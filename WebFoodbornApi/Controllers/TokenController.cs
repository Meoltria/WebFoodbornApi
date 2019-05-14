using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebFoodbornApi.Data;
using WebFoodbornApi.Common;
using WebFoodbornApi.Models;
using WebFoodbornApi.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace WebFoodbornApi.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/Token")]
    public class TokenController : Controller
    {
        private readonly JWTTokenOptions tokenOptions;
        private readonly ApiContext dbContext;

        public TokenController(JWTTokenOptions tokenOptions, ApiContext dbContext)
        {
            this.tokenOptions = tokenOptions;
            this.dbContext = dbContext;
        }

        private string CreatToken(User user)
        {
            var handler = new JwtSecurityTokenHandler();

            string jti = Guid.NewGuid().ToString();

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti,jti),
                new Claim("userId",user.Id.ToString()),
                new Claim("role",user.Role.Code)
            };
            ClaimsIdentity identity = new ClaimsIdentity(new GenericIdentity(user.Code, "TokenAuth"), claims);

            var token = handler.CreateEncodedJwt(new SecurityTokenDescriptor
            {
                Issuer = tokenOptions.Issuer,
                Audience = tokenOptions.Audience,
                SigningCredentials = new SigningCredentials(tokenOptions.SecretKey, SecurityAlgorithms.HmacSha256),
                Expires = DateTime.Now.Add(tokenOptions.Expiration),
                Subject = identity
            });
            return token;
        }

        /// <summary>
        /// 用户登录，返回一个Token
        /// </summary>
        /// <param name="loginUser">登录信息</param>
        /// <returns>Token</returns>
        // Post: api/Token
        [HttpPost]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<IActionResult> GetToken([FromBody]LoginInputDto loginUser)
        {
            var user = await dbContext.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Code == loginUser.UserCode && u.PassWord == Encrypt.Md5Encrypt(loginUser.PassWord));

            if (user == null)
            {
                return NotFound(Json(new { Error = "用户名或密码错误！" }));
            }
            return Json(new { Token = CreatToken(user) });
        }

        /// <summary>
        /// 刷新Token
        /// </summary>
        /// <returns></returns>
        [HttpGet("Refresh")]
        [Authorize]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<IActionResult> RefreshToken()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            int id = Convert.ToInt32(claimsIdentity.Claims.FirstOrDefault(c => c.Type == "userId").Value);

            User user = await dbContext.Users
             .Include(q => q.Role)
             .FirstOrDefaultAsync(u => u.Id == id);

            return Json(new { Token = CreatToken(user) });
        }
    }
}