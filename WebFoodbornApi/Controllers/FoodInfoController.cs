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

namespace WebFoodbornApi.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/FoodInfos")]
    [Authorize]
    public class FoodInfoController : Controller
    {
        private readonly ApiContext dbContext;
        private readonly IMapper mapper;

        public FoodInfoController(ApiContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        #region 暴露信息基本操作

        #region 获得暴露信息列表
        /// <summary>
        /// 获得暴露信息列表
        /// </summary>
        /// <param name="input">传入参数</param>
        /// <returns>暴露信息列表</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<FoodInfoOutput>), 200)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<IEnumerable<FoodInfoOutput>> GetFoodInfos(FoodInfoQueryInput input)
        {
            List<FoodInfo> foodInfos = await dbContext.FoodInfos.Where(f => f.PatientId == input.PatientId).ToListAsync();
            List<FoodInfoOutput> list = mapper.Map<List<FoodInfoOutput>>(foodInfos);

            return list;
        }
        #endregion

        #region 获得暴露信息
        /// <summary>
        /// 获得暴露信息
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetFoodInfo")]
        [ProducesResponseType(typeof(FoodInfoOutput), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<IActionResult> GetFoodInfo([FromRoute]int id)
        {
            FoodInfo foodInfo = await dbContext.FoodInfos
               .FirstOrDefaultAsync(f => f.Id == id);

            if (foodInfo == null)
            {
                return NotFound(Json(new { Error = "该暴露信息不存在" }));
            }

            FoodInfoOutput output = mapper.Map<FoodInfoOutput>(foodInfo);

            return new ObjectResult(output);
        }
        #endregion

        #region 创建暴露信息
        /// <summary>
        /// 创建暴露信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        [ProducesResponseType(typeof(FoodInfoOutput), 201)]
        [ProducesResponseType(typeof(ValidationError), 422)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<IActionResult> CreateFoodInfo([FromBody]FoodInfoCreateInput input)
        {
            if (dbContext.FoodInfos.Count(f => f.PatientId == input.PatientId && f.FoodName.Equals(input.FoodName)) > 0)
            {
                return BadRequest(Json(new { Error = "该患者已登记过同名食物" }));
            }

            var foodInfo = mapper.Map<FoodInfo>(input);
            dbContext.FoodInfos.Add(foodInfo);
            await dbContext.SaveChangesAsync();

            return CreatedAtRoute("GetFoodInfo", new { id = foodInfo.Id }, mapper.Map<FoodInfoOutput>(foodInfo));
        }
        #endregion

        #region 修改暴露信息
        /// <summary>
        /// 修改暴露信息
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
        public async Task<IActionResult> UpdateFoodInfo([FromRoute]int id, [FromBody]FoodInfoUpdateInput input)
        {
            if (input.Id != id)
            {
                return BadRequest(Json(new { Error = "请求参数错误" }));
            }
            var foodInfo = await dbContext.FoodInfos.FirstOrDefaultAsync(f => f.Id == id);
            if (foodInfo == null)
            {
                return NotFound(Json(new { Error = "该暴露信息不存在" }));
            }

            dbContext.Entry(foodInfo).CurrentValues.SetValues(input);
            await dbContext.SaveChangesAsync();

            return new NoContentResult();
        }
        #endregion

        #region 更新暴露信息
        /// <summary>
        /// 更新暴露信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patchDoc"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(FoodInfoOutput), 201)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(ValidationError), 422)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<IActionResult> PatchFoodInfoAsync([FromRoute]int id, [FromBody]JsonPatchDocument<FoodInfoUpdateInput> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest(Json(new { Error = "请求参数错误" }));
            }

            var foodInfo = await dbContext.FoodInfos.FirstOrDefaultAsync(f => f.Id == id);
            if (foodInfo == null)
            {
                return NotFound(Json(new { Error = "该暴露信息不存在" }));
            }

            var input = mapper.Map<FoodInfoUpdateInput>(foodInfo);
            patchDoc.ApplyTo(input);

            TryValidateModel(input);
            if (!ModelState.IsValid)
            {
                return new ValidationFailedResult(ModelState);
            }

            dbContext.Entry(foodInfo).CurrentValues.SetValues(input);
            await dbContext.SaveChangesAsync();

            return CreatedAtRoute("GetFoodInfo", new { id = foodInfo.Id }, mapper.Map<FoodInfoOutput>(foodInfo));
        }
        #endregion

        #region 删除暴露信息
        /// <summary>
        /// 删除暴露信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<IActionResult> DeleteFoodInfo([FromRoute]int id)
        {
            var foodInfo = await dbContext.FoodInfos.FirstOrDefaultAsync(f => f.Id == id);
            if (foodInfo == null)
            {
                return NotFound(Json(new { Error = "该暴露信息不存在" }));
            }

            foodInfo.Status = "删除";
            dbContext.FoodInfos.Update(foodInfo);
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
                var foodInfo = await dbContext.FoodInfos.FirstOrDefaultAsync(f => f.Id == ids[i]);
                if (foodInfo != null)
                {
                    foodInfo.Status = "删除";
                    dbContext.FoodInfos.Update(foodInfo);
                }
            }

            await dbContext.SaveChangesAsync();

            return new NoContentResult();
        }
        #endregion

        #endregion
    }
}