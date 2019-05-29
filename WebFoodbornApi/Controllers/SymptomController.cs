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
    [Route("api/v1/Symptoms")]
    [Authorize]
    public class SymptomController : Controller
    {
        private readonly ApiContext dbContext;
        private readonly IMapper mapper;

        public SymptomController(ApiContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        #region 基本操作

        #region 获得症状体征信息
        /// <summary>
        /// 获得症状体征信息
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetSymptom")]
        [ProducesResponseType(typeof(SymptomOutput), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<IActionResult> GetSymptom([FromRoute]int id)
        {
            Symptom symptom = await dbContext.Symptoms
               .FirstOrDefaultAsync(s => s.Id == id);

            if (symptom == null)
            {
                return NotFound(Json(new { Error = "该症状体征信息不存在" }));
            }

            SymptomOutput output = mapper.Map<SymptomOutput>(symptom);

            return new ObjectResult(output);
        }

        /// <summary>
        /// 获得症状体征信息
        /// </summary>
        /// <param name="patientId">patientId</param>
        /// <returns></returns>
        [HttpGet("~/api/v1/Symptoms/PatientId/{patientId}")]
        [ProducesResponseType(typeof(SymptomOutput), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<IActionResult> GetSymptomByPatientId([FromRoute]int patientId)
        {
            Symptom symptom = await dbContext.Symptoms
               .FirstOrDefaultAsync(s => s.PatientId == patientId);

            if (symptom == null)
            {
                return NotFound(Json(new { Error = "该患者未填写症状体征" }));
            }

            SymptomOutput output = mapper.Map<SymptomOutput>(symptom);

            return new ObjectResult(output);
        }
        #endregion

        #region 创建症状体征
        /// <summary>
        /// 创建症状体征
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        [ProducesResponseType(typeof(SymptomCreateInput), 201)]
        [ProducesResponseType(typeof(ValidationError), 422)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<IActionResult> CreateSymptom([FromBody]SymptomCreateInput input)
        {
            if (dbContext.Symptoms.Count(s => s.PatientId == input.PatientId) > 0)
            {
                return BadRequest(Json(new { Error = "患者已填写症状体征" }));
            }

            var symptom = mapper.Map<Symptom>(input);
            dbContext.Symptoms.Add(symptom);
            await dbContext.SaveChangesAsync();

            return CreatedAtRoute("GetSymptom", new { id = symptom.Id }, mapper.Map<SymptomOutput>(symptom));
        }
        #endregion

        #region 修改症状体征
        /// <summary>
        /// 修改症状体征
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
        public async Task<IActionResult> UpdateSymptom([FromRoute]int id, [FromBody]SymptomUpdateInput input)
        {
            if (input.Id != id)
            {
                return BadRequest(Json(new { Error = "请求参数错误" }));
            }
            var symptom = await dbContext.Symptoms.FirstOrDefaultAsync(s => s.Id == id);
            if (symptom == null)
            {
                return NotFound(Json(new { Error = "该症状体征信息不存在" }));
            }

            dbContext.Entry(symptom).CurrentValues.SetValues(input);
            await dbContext.SaveChangesAsync();

            return new NoContentResult();
        }
        #endregion

        #endregion
    }
}