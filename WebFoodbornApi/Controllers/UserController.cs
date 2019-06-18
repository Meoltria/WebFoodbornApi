using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using WebFoodbornApi.Data;
using WebFoodbornApi.Dtos;
using WebFoodbornApi.Common;
using WebFoodbornApi.Filters;
using WebFoodbornApi.Models;
using System.Security.Claims;

namespace WebFoodbornApi.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/Users")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly ApiContext dbContext;
        private readonly IMapper mapper;

        public UserController(ApiContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        #region 用户基本操作
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="input">传入参数</param>
        /// <returns>用户列表</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<UserOutput>), 200)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<IEnumerable<UserOutput>> GetUsers(UserQueryInput input)
        {
            int pageIndex = input.Page - 1;
            int Per_Page = input.Per_Page;
            string sortBy = input.SortBy;

            IQueryable<User> query = dbContext.Users
                 .Include(q => q.Organazition)
                 .Include(q => q.Role)
                 .AsQueryable<User>();

            query = query.Where(q => string.IsNullOrEmpty(input.Name) || q.Name.Contains(input.Name));
            query = query.OrderBy(sortBy);

            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / Per_Page);

            HttpContext.Response.Headers.Add("X-TotalCount", JsonConvert.SerializeObject(totalCount));
            HttpContext.Response.Headers.Add("X-TotalPage", JsonConvert.SerializeObject(totalPages));

            query = query.Skip(pageIndex * Per_Page).Take(Per_Page);

            List<User> users = await query.ToListAsync();
            List<UserOutput> list = mapper.Map<List<UserOutput>>(users);

            return list;
        }

        /// <summary>
        /// 获得用户信息
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetUser")]
        [ProducesResponseType(typeof(UserOutput), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<IActionResult> GetUser([FromRoute]int id)
        {
            User user = await dbContext.Users
               .Include(q => q.Organazition)
               .Include(q => q.Role)
               .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound(Json(new { Error = "该用户不存在" }));
            }

            UserOutput output = mapper.Map<UserOutput>(user);

            return new ObjectResult(output);
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        [ProducesResponseType(typeof(UserOutput), 201)]
        [ProducesResponseType(typeof(ValidationError), 422)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<IActionResult> CreateUser([FromBody]UserCreateInput input)
        {
            if (dbContext.Users.Count(u => u.Name == input.Name) > 0)
            {
                return BadRequest(Json(new { Error = "用户名已存在" }));
            }
            if (await dbContext.Orgnazitions.CountAsync(o => o.Id == input.OrganazitionId) == 0)
            {
                return BadRequest(Json(new { Error = "不存在该部门" }));
            }
            if (await dbContext.Roles.CountAsync(r => r.Id == input.RoleId) == 0)
            {
                return BadRequest(Json(new { Error = "不存在该角色" }));
            }

            var user = mapper.Map<User>(input);
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();

            return CreatedAtRoute("GetUser", new { id = user.Id }, mapper.Map<UserOutput>(user));
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ValidateModel]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(ValidationError), 422)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<IActionResult> UpdateUser([FromRoute]int id, [FromBody]UserUpdateInput input)
        {
            if (input.Id != id)
            {
                return BadRequest(Json(new { Error = "请求参数错误" }));
            }
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound(Json(new { Error = "该用户不存在" }));
            }
            if (await dbContext.Orgnazitions.CountAsync(o => o.Id == input.OrganazitionId) == 0)
            {
                return BadRequest(Json(new { Error = "不存在该部门" }));
            }
            if (await dbContext.Roles.CountAsync(r => r.Id == input.RoleId) == 0)
            {
                return BadRequest(Json(new { Error = "不存在该角色" }));
            }

            dbContext.Entry(user).CurrentValues.SetValues(input);
            await dbContext.SaveChangesAsync();

            return new NoContentResult();
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patchDoc"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(UserOutput), 201)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(ValidationError), 422)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<IActionResult> PatchUserAsync([FromRoute]int id, [FromBody]JsonPatchDocument<UserUpdateInput> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest(Json(new { Error = "请求参数错误" }));
            }

            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound(Json(new { Error = "该用户不存在" }));
            }

            var input = mapper.Map<UserUpdateInput>(user);
            patchDoc.ApplyTo(input);

            TryValidateModel(input);
            if (!ModelState.IsValid)
            {
                return new ValidationFailedResult(ModelState);
            }

            if (await dbContext.Orgnazitions.CountAsync(o => o.Id == input.OrganazitionId) == 0)
            {
                return BadRequest(Json(new { Error = "不存在该部门" }));
            }
            if (await dbContext.Roles.CountAsync(r => r.Id == input.RoleId) == 0)
            {
                return BadRequest(Json(new { Error = "不存在该角色" }));
            }

            dbContext.Entry(user).CurrentValues.SetValues(input);
            await dbContext.SaveChangesAsync();

            return CreatedAtRoute("GetUser", new { id = user.Id }, mapper.Map<UserOutput>(user));
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<IActionResult> DeleteUser([FromRoute]int id)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound(Json(new { Error = "该用户不存在" }));
            }

            user.Status = "删除";
            dbContext.Users.Update(user);
            await dbContext.SaveChangesAsync();

            return new NoContentResult();
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids">ID数组</param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<IActionResult> BatchDelete([FromBody]int[] ids)
        {
            for (int i = 0; i < ids.Length; i++)
            {
                var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == ids[i]);
                if (user != null)
                {
                    user.Status = "删除";
                    dbContext.Users.Update(user);
                }
            }

            await dbContext.SaveChangesAsync();

            return new NoContentResult();
        }
        #endregion

        #region 修改、重置密码
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("/api/v1/Users/PassWord")]
        [Authorize]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<IActionResult> UpdatePassWord([FromBody]PassWordInput input)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            int id = Convert.ToInt32(claimsIdentity.Claims.FirstOrDefault(c => c.Type == "userId").Value);

            User user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound(Json(new { Error = "该用户不存在" }));
            }

            if (!user.PassWord.Equals(Encrypt.Md5Encrypt(input.OldPassWord)))
            {
                return BadRequest(Json(new { Error = "原密码错误" }));
            }

            user.PassWord = Encrypt.Md5Encrypt(input.NewPassWord);

            dbContext.Update(user);
            await dbContext.SaveChangesAsync();

            return new NoContentResult();
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        [HttpPut("/api/v1/Users/{userId}/WithReset/PassWord")]
        [Authorize(Roles = "admin,data")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<IActionResult> ResetPassWord([FromRoute]int userId, [FromBody]string passWord)
        {
            User user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return NotFound(Json(new { Error = "该用户不存在" }));
            }

            user.PassWord = Encrypt.Md5Encrypt(passWord);

            dbContext.Update(user);
            await dbContext.SaveChangesAsync();

            return new NoContentResult();
        }
        #endregion

        #region 部门-用户操作
        /// <summary>
        /// 部门-用户列表
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("/api/v1/Orgs/{orgId}/Users")]
        [ProducesResponseType(typeof(List<UserOutput>), 200)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<IEnumerable<UserOutput>> GetOrgUsers([FromRoute]int orgId, UserQueryInput input)
        {
            int pageIndex = input.Page - 1;
            int Per_Page = input.Per_Page;
            string sortBy = input.SortBy;

            IQueryable<User> query = dbContext.Users
                 .Include(q => q.Organazition)
                 .Include(q => q.Role)
                 .AsQueryable<User>();

            query = query.Where(q => q.OrganazitionId == orgId);
            query = query.Where(q => string.IsNullOrEmpty(input.Name) || q.Name.Contains(input.Name));
            query = query.OrderBy(sortBy);

            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / Per_Page);

            HttpContext.Response.Headers.Add("X-TotalCount", JsonConvert.SerializeObject(totalCount));
            HttpContext.Response.Headers.Add("X-TotalPage", JsonConvert.SerializeObject(totalPages));

            query = query.Skip(pageIndex * Per_Page).Take(Per_Page);

            List<User> users = await query.ToListAsync();
            List<UserOutput> list = mapper.Map<List<UserOutput>>(users);

            return list;
        }

        /// <summary>
        /// 部门-用户选项
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="userTypeCode"></param>
        /// <returns></returns>
        [HttpGet("/api/v1/Orgs/{orgId}/{userTypeCode}/Users")]
        [ProducesResponseType(typeof(List<UserSelectOutput>), 200)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<IEnumerable<UserSelectOutput>> GetOrgUserSelect([FromRoute]int orgId, [FromRoute]string userTypeCode)
        {
            IQueryable<User> query = dbContext.Users.AsQueryable<User>();

            query = query.Where(q => q.OrganazitionId == orgId);
            query = query.Where(q => q.UserTypeCode.Equals(userTypeCode));

            List<User> users = await query.ToListAsync();
            List<UserSelectOutput> list = mapper.Map<List<UserSelectOutput>>(users);

            return list;
        }
        #endregion
    }
}