using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Security.Claims;
using System.Threading.Tasks;
using WebFoodbornApi.Data;
using WebFoodbornApi.Dtos;
using WebFoodbornApi.Models;

namespace WebFoodbornApi.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/Login")]
    [Authorize]
    public class LoginController : Controller
    {
        private readonly ApiContext dbContext;
        private readonly IMapper mapper;

        public LoginController(ApiContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        /// <summary>
        /// 根据Token,得到登录用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(UserOutput), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<IActionResult> GetUser()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            int id = Convert.ToInt32(claimsIdentity.Claims.FirstOrDefault(c => c.Type == "userId").Value);

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
    }
}