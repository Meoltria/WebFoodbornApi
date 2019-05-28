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
    [Route("api/v1/InitialDiagnoses")]
    [Authorize]
    public class InitialDiagnosisController : Controller
    {
        private readonly ApiContext dbContext;
        private readonly IMapper mapper;

        public InitialDiagnosisController(ApiContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        #region 基本操作

        #region 获得初步诊断信息
        /// <summary>
        /// 获得初步诊断信息
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetInitialDiagnosis")]
        [ProducesResponseType(typeof(InitialDiagnosisOutput), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<IActionResult> GetInitialDiagnosis([FromRoute]int id)
        {
            InitialDiagnosis initialDiagnosis = await dbContext.InitialDiagnoses
               .FirstOrDefaultAsync(i => i.Id == id);

            if (initialDiagnosis == null)
            {
                return NotFound(Json(new { Error = "该初步诊断不存在" }));
            }

            InitialDiagnosisOutput output = mapper.Map<InitialDiagnosisOutput>(initialDiagnosis);

            return new ObjectResult(output);
        }

        /// <summary>
        /// 获得初步诊断
        /// </summary>
        /// <param name="patientId">patientId</param>
        /// <returns></returns>
        [HttpGet("~/api/v1/InitialDiagnoses/PatientId/{patientId}")]
        [ProducesResponseType(typeof(PatientOutput), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<IActionResult> GetInitialDiagnosisByPatientId([FromRoute]int patientId)
        {
            InitialDiagnosis initialDiagnosis = await dbContext.InitialDiagnoses
               .FirstOrDefaultAsync(i => i.PatientId == patientId);

            if (initialDiagnosis == null)
            {
                return NotFound(Json(new { Error = "该患者未填写初步诊断" }));
            }

            InitialDiagnosisOutput output = mapper.Map<InitialDiagnosisOutput>(initialDiagnosis);

            return new ObjectResult(output);
        }
        #endregion

        #region 创建初步诊断
        /// <summary>
        /// 创建初步诊断
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        [ProducesResponseType(typeof(InitialDiagnosisOutput), 201)]
        [ProducesResponseType(typeof(ValidationError), 422)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<IActionResult> CreateInitialDiagnosis([FromBody]InitialDiagnosisCreateInput input)
        {
            if (dbContext.InitialDiagnoses.Count(i => i.PatientId == input.PatientId) > 0)
            {
                return BadRequest(Json(new { Error = "患者已填写初步诊断" }));
            }

            var initialDiagnosis = mapper.Map<InitialDiagnosis>(input);
            dbContext.InitialDiagnoses.Add(initialDiagnosis);
            await dbContext.SaveChangesAsync();

            return CreatedAtRoute("GetInitialDiagnosis", new { id = initialDiagnosis.Id }, mapper.Map<InitialDiagnosisOutput>(initialDiagnosis));
        }
        #endregion

        #region 修改初步诊断信息
        /// <summary>
        /// 修改初步诊断信息
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
        public async Task<IActionResult> UpdateInitialDiagnosis([FromRoute]int id, [FromBody]InitialDiagnosisUpdateInput input)
        {
            if (input.Id != id)
            {
                return BadRequest(Json(new { Error = "请求参数错误" }));
            }
            var initialDiagnosis = await dbContext.InitialDiagnoses.FirstOrDefaultAsync(p => p.Id == id);
            if (initialDiagnosis == null)
            {
                return NotFound(Json(new { Error = "该初步诊断信息不存在" }));
            }

            dbContext.Entry(initialDiagnosis).CurrentValues.SetValues(input);
            await dbContext.SaveChangesAsync();

            return new NoContentResult();
        }
        #endregion

        #endregion
    }
}