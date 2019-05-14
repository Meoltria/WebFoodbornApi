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
    [Route("api/v1/Dictionary")]
    [Authorize]
    public class DictionaryController : Controller
    {
        private readonly ApiContext dbContext;
        private readonly IMapper mapper;

        public DictionaryController(ApiContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        #region 基本操作
        /// <summary>
        /// 获得字典列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<DictionaryOutput>), 200)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<IEnumerable<DictionaryOutput>> GetDictionaries(DictionaryQueryInput input)
        {
            int pageIndex = input.Page - 1;
            int Per_Page = input.Per_Page;
            string sortBy = input.SortBy;

            IQueryable<Dictionary> query = dbContext.Dictionaries.AsQueryable<Dictionary>();

            query = query.Where(q => string.IsNullOrEmpty(input.TypeCode) || q.TypeCode == input.TypeCode);
            query = query.Where(q => string.IsNullOrEmpty(input.TypeName) || q.TypeName.Contains(input.TypeName));
            query = query.OrderBy(sortBy);

            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / Per_Page);

            HttpContext.Response.Headers.Add("X-TotalCount", JsonConvert.SerializeObject(totalCount));
            HttpContext.Response.Headers.Add("X-TotalPage", JsonConvert.SerializeObject(totalPages));

            query = query.Skip(pageIndex * Per_Page).Take(Per_Page);

            List<Dictionary> dictionaries = await query.ToListAsync();
            List<DictionaryOutput> list = mapper.Map<List<DictionaryOutput>>(dictionaries);

            return list;
        }

        /// <summary>
        /// 获得字典信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetDictionary")]
        [ProducesResponseType(typeof(DictionaryOutput), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<IActionResult> GetDictionary([FromRoute]int id)
        {
            Dictionary dictionary = await dbContext.Dictionaries.FirstOrDefaultAsync(d => d.Id == id);
            if (dictionary == null)
            {
                return NotFound(Json(new { Error = "该字典不存在" }));
            }

            DictionaryOutput dictionaryOutput = mapper.Map<DictionaryOutput>(dictionary);

            return new ObjectResult(dictionaryOutput);
        }

        /// <summary>
        /// 获得字典信息
        /// </summary>
        /// <param name="typeCode"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("{typeCode}/{code}/Dictionary")]
        [ProducesResponseType(typeof(DictionaryOutput), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<IActionResult> GetDictionary([FromRoute]string typeCode, [FromRoute]string code)
        {
            Dictionary dictionary = await dbContext.Dictionaries.FirstOrDefaultAsync(d => d.TypeCode.Equals(typeCode) && d.Code.Equals(code));
            if (dictionary == null)
            {
                return NotFound(Json(new { Error = "该字典不存在" }));
            }

            DictionaryOutput dictionaryOutput = mapper.Map<DictionaryOutput>(dictionary);

            return new ObjectResult(dictionaryOutput);
        }

        /// <summary>
        /// 创建字典
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        [ProducesResponseType(typeof(DictionaryOutput), 201)]
        [ProducesResponseType(typeof(ValidationError), 422)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<IActionResult> CreateDictionary([FromBody]DictionaryCreateInput input)
        {
            Dictionary dictionary = mapper.Map<Dictionary>(input);

            dbContext.Dictionaries.Add(dictionary);
            await dbContext.SaveChangesAsync();

            return CreatedAtRoute("GetOrg", new { id = dictionary.Id }, mapper.Map<DictionaryOutput>(dictionary));
        }

        /// <summary>
        /// 修改字典
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
        public async Task<IActionResult> UpdateDictionary([FromRoute]int id, [FromBody]DictonaryUpdateInput input)
        {
            if (input.Id != id)
            {
                return BadRequest(Json(new { Error = "请求参数错误" }));
            }

            var dictionary = await dbContext.Dictionaries.FirstOrDefaultAsync(d => d.Id == id);
            if (dictionary == null)
            {
                return NotFound(Json(new { Error = "该字典不存在" }));
            }

            dbContext.Entry(dictionary).CurrentValues.SetValues(input);
            await dbContext.SaveChangesAsync();

            return new NoContentResult();
        }

        /// <summary>
        /// 更新字典
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patchDoc"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(DictionaryOutput), 201)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(ValidationError), 422)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<IActionResult> PatchDictionary([FromRoute]int id, [FromBody]JsonPatchDocument<DictonaryUpdateInput> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest(Json(new { Error = "请求参数错误" }));
            }

            var dictionary = await dbContext.Dictionaries.FirstOrDefaultAsync(d => d.Id == id);
            if (dictionary == null)
            {
                return NotFound(Json(new { Error = "该字典不存在" }));
            }

            var input = mapper.Map<DictonaryUpdateInput>(dictionary);
            patchDoc.ApplyTo(input);

            TryValidateModel(input);
            if (!ModelState.IsValid)
            {
                return new ValidationFailedResult(ModelState);
            }

            dbContext.Entry(dictionary).CurrentValues.SetValues(input);
            await dbContext.SaveChangesAsync();

            return CreatedAtRoute("GetOrg", new { id = dictionary.Id }, mapper.Map<DictionaryOutput>(dictionary));
        }

        /// <summary>
        /// 删除字典
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(void), 400)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<IActionResult> DeleteDictionary([FromRoute]int id)
        {
            var dictionary = await dbContext.Dictionaries.FirstOrDefaultAsync(d => d.Id == id);
            if (dictionary == null)
            {
                return NotFound(Json(new { Error = "该字典不存在" }));
            }

            dbContext.Dictionaries.Remove(dictionary);
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
                var dictionary = await dbContext.Dictionaries.FirstOrDefaultAsync(d => d.Id == ids[i]);
                if (dictionary != null)
                {
                    dbContext.Dictionaries.Remove(dictionary);
                }
            }
            await dbContext.SaveChangesAsync();

            return new NoContentResult();
        }
        #endregion

        #region 提供选项
        /// <summary>
        /// 字典选项
        /// </summary>
        /// <param name="typeCode"></param>
        /// <returns></returns>
        [HttpGet("WithSelect")]
        [ProducesResponseType(typeof(List<DictionaryOutput>), 200)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<IEnumerable<DictionarySelectOutput>> GetDictionarySelect(string typeCode)
        {
            IQueryable<Dictionary> query = dbContext.Dictionaries.AsQueryable<Dictionary>();

            query = query.Where(q => string.IsNullOrEmpty(typeCode) || q.TypeCode == typeCode);
            query = query.OrderBy(q => q.Code);

            List<Dictionary> dictionaries = await query.ToListAsync();
            List<DictionarySelectOutput> list = mapper.Map<List<DictionarySelectOutput>>(dictionaries);

            return list;
        }
        #endregion
    }
}