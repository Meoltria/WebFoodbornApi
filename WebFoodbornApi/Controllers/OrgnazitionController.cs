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
using WebFoodbornApi.Filters;
using WebFoodbornApi.Models;

namespace WebFoodbornApi.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/Orgs")]
    [Authorize]
    public class OrgnazitionController : Controller
    {
        private readonly ApiContext dbContext;
        private readonly IMapper mapper;

        public OrgnazitionController(ApiContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        #region 基本操作
        /// <summary>
        /// 获得部门列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<OrgOutput>), 200)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<IEnumerable<OrgOutput>> GetOrgs(OrgQueryInput input)
        {
            int pageIndex = input.Page - 1;
            int Per_Page = input.Per_Page;
            string sortBy = input.SortBy;

            IQueryable<Orgnazition> query = dbContext.Orgnazitions.AsQueryable<Orgnazition>();

            query = query.Where(q => string.IsNullOrEmpty(input.Name) || q.Name.Contains(input.Name));
            query = query.Where(q => string.IsNullOrEmpty(input.OrgTypeCode) || q.OrgTypeCode == input.OrgTypeCode);
            query = query.OrderBy(sortBy);

            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / Per_Page);

            HttpContext.Response.Headers.Add("X-TotalCount", JsonConvert.SerializeObject(totalCount));
            HttpContext.Response.Headers.Add("X-TotalPage", JsonConvert.SerializeObject(totalPages));

            query = query.Skip(pageIndex * Per_Page).Take(Per_Page);

            List<Orgnazition> orgs = await query.ToListAsync();
            List<OrgOutput> list = mapper.Map<List<OrgOutput>>(orgs);

            return list;
        }

        /// <summary>
        /// 获得部门列表
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("{orgId}/Orgs")]
        [ProducesResponseType(typeof(List<OrgOutput>), 200)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<IEnumerable<OrgOutput>> GetOrgs([FromRoute]int orgId, OrgQueryInput input)
        {
            int pageIndex = input.Page - 1;
            int Per_Page = input.Per_Page;
            string sortBy = input.SortBy;

            IQueryable<Orgnazition> query = dbContext.Orgnazitions.AsQueryable<Orgnazition>();

            query = query.Where(q => q.Parent == orgId);
            query = query.Where(q => string.IsNullOrEmpty(input.Name) || q.Name.Contains(input.Name));
            query = query.Where(q => string.IsNullOrEmpty(input.OrgTypeCode) || q.OrgTypeCode == input.OrgTypeCode);
            query = query.OrderBy(sortBy);

            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / Per_Page);

            HttpContext.Response.Headers.Add("X-TotalCount", JsonConvert.SerializeObject(totalCount));
            HttpContext.Response.Headers.Add("X-TotalPage", JsonConvert.SerializeObject(totalPages));

            query = query.Skip(pageIndex * Per_Page).Take(Per_Page);

            List<Orgnazition> orgs = await query.ToListAsync();
            List<OrgOutput> list = mapper.Map<List<OrgOutput>>(orgs);

            return list;
        }

        /// <summary>
        /// 获得部门信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetOrg")]
        [ProducesResponseType(typeof(OrgOutput), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<IActionResult> GetOrg([FromRoute]int id)
        {
            Orgnazition org = await dbContext.Orgnazitions.FirstOrDefaultAsync(o => o.Id == id);
            if (org == null)
            {
                return NotFound(Json(new { Error = "该部门不存在" }));
            }

            OrgOutput orgOutput = mapper.Map<OrgOutput>(org);

            return new ObjectResult(orgOutput);
        }

        /// <summary>
        /// 创建部门
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        [ProducesResponseType(typeof(OrgOutput), 201)]
        [ProducesResponseType(typeof(ValidationError), 422)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<IActionResult> CreateOrg([FromBody]OrgCreateInput input)
        {
            Orgnazition org = mapper.Map<Orgnazition>(input);

            dbContext.Orgnazitions.Add(org);
            await dbContext.SaveChangesAsync();

            return CreatedAtRoute("GetOrg", new { id = org.Id }, mapper.Map<OrgOutput>(org));
        }

        /// <summary>
        /// 修改部门
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
        public async Task<IActionResult> UpdateOrg([FromRoute]int id, [FromBody]OrgUpdateInput input)
        {
            if (input.Id != id)
            {
                return BadRequest(Json(new { Error = "请求参数错误" }));
            }

            var org = await dbContext.Orgnazitions.FirstOrDefaultAsync(o => o.Id == id);
            if (org == null)
            {
                return NotFound(Json(new { Error = "该部门不存在" }));
            }

            dbContext.Entry(org).CurrentValues.SetValues(input);
            await dbContext.SaveChangesAsync();

            return new NoContentResult();
        }

        /// <summary>
        /// 更新部门
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patchDoc"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(OrgOutput), 201)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(ValidationError), 422)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<IActionResult> PatchOrg([FromRoute]int id, [FromBody]JsonPatchDocument<OrgUpdateInput> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest(Json(new { Error = "请求参数错误" }));
            }

            var org = await dbContext.Orgnazitions.FirstOrDefaultAsync(o => o.Id == id);
            if (org == null)
            {
                return NotFound(Json(new { Error = "该部门不存在" }));
            }

            var input = mapper.Map<OrgUpdateInput>(org);
            patchDoc.ApplyTo(input);

            TryValidateModel(input);
            if (!ModelState.IsValid)
            {
                return new ValidationFailedResult(ModelState);
            }

            dbContext.Entry(org).CurrentValues.SetValues(input);
            await dbContext.SaveChangesAsync();

            return CreatedAtRoute("GetOrg", new { id = org.Id }, mapper.Map<OrgOutput>(org));
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(void), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<IActionResult> DeleteOrg([FromRoute]int id)
        {
            var org = await dbContext.Orgnazitions.FirstOrDefaultAsync(o => o.Id == id);
            if (org == null)
            {
                return NotFound(Json(new { Error = "该部门不存在" }));
            }

            int childCount = dbContext.Orgnazitions.Count(o => o.Parent == id);
            int userCount = dbContext.Users.Count(u => u.OrganazitionId == id);
            if (childCount != 0 || userCount != 0)
            {
                return BadRequest(Json(new { Error = "该部门存在下级或引用，不可删除" }));
            }

            org.Status = "删除";
            dbContext.Orgnazitions.Update(org);
            await dbContext.SaveChangesAsync();

            return new NoContentResult();
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<IActionResult> BatchDelete([FromBody]int[] ids)
        {
            for (int i = 0; i < ids.Length; i++)
            {
                var org = await dbContext.Orgnazitions.FirstOrDefaultAsync(o => o.Id == ids[i]);
                int childCount = dbContext.Orgnazitions.Count(o => o.Parent == ids[i]);
                int userCount = dbContext.Users.Count(u => u.OrganazitionId == ids[i]);
                if (org != null && childCount == 0 && userCount == 0)
                {
                    org.Status = "删除";
                    dbContext.Orgnazitions.Update(org);
                }
            }
            await dbContext.SaveChangesAsync();

            return new NoContentResult();
        }
        #endregion

        #region 树形菜单

        /// <summary>
        /// 获得树形菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/WithTree/Orgs")]
        [ProducesResponseType(typeof(OrgTreeOutput), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public IActionResult GetOrgTree([FromRoute]int id)
        {
            var org = dbContext.Orgnazitions.FirstOrDefault(o => o.Id == id);
            if (org == null)
            {
                return NotFound(Json(new { Error = "该部门不存在" }));
            }
            var orgTreeOut = mapper.Map<OrgTreeOutput>(org);
            GenerateTree(orgTreeOut.Id, ref orgTreeOut);

            return Ok(orgTreeOut);
        }

        /// <summary>
        /// 获得树形菜单
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="orgTypeCode"></param>
        /// <returns></returns>
        [HttpGet("{parentId}/{orgTypeCode}/WithTree/Orgs")]
        [ProducesResponseType(typeof(List<OrgTreeOutput>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public IActionResult GetOrgTree([FromRoute]int parentId, [FromRoute]string orgTypeCode)
        {
            var list = dbContext.Orgnazitions.Where(o => o.Parent == parentId && o.OrgTypeCode.Equals(orgTypeCode)).ToList();
            List<OrgTreeOutput> treeList = new List<OrgTreeOutput>();

            foreach (var item in list)
            {
                var orgTreeOut = mapper.Map<OrgTreeOutput>(item);
                GenerateTree(orgTreeOut.Id, ref orgTreeOut);
                treeList.Add(orgTreeOut);
            }

            return Ok(treeList);
        }


        private void GenerateTree(int parentId, ref OrgTreeOutput orgTreeOutput)
        {
            var list = dbContext.Orgnazitions.Where(o => o.Parent == parentId).ToList();
            List<OrgTreeOutput> childList = new List<OrgTreeOutput>();
            foreach (var item in list)
            {
                var childNode = mapper.Map<OrgTreeOutput>(item);
                GenerateTree(childNode.Id, ref childNode);
                childList.Add(childNode);
            }
            orgTreeOutput.Children = childList;
        }


        #endregion
    }
}