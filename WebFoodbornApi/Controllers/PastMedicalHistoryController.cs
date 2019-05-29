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
    [Route("api/v1/PastMedicalHistories")]
    [Authorize]
    public class PastMedicalHistoryController : Controller
    {
        private readonly ApiContext dbContext;
        private readonly IMapper mapper;

        public PastMedicalHistoryController(ApiContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        #region 基本操作

        #region 获得既往病史信息
        /// <summary>
        /// 获得既往病史信息
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetPastMedicalHistory")]
        [ProducesResponseType(typeof(PastMedicalHistoryOutput), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<IActionResult> GetPastMedicalHistory([FromRoute]int id)
        {
            PastMedicalHistory pastMedicalHistory = await dbContext.PastMedicalHistories
               .FirstOrDefaultAsync(p => p.Id == id);

            if (pastMedicalHistory == null)
            {
                return NotFound(Json(new { Error = "该既往病史不存在" }));
            }

            PastMedicalHistoryOutput output = mapper.Map<PastMedicalHistoryOutput>(pastMedicalHistory);

            return new ObjectResult(output);
        }

        /// <summary>
        /// 获得既往病史
        /// </summary>
        /// <param name="patientId">patientId</param>
        /// <returns></returns>
        [HttpGet("~/api/v1/PastMedicalHistories/PatientId/{patientId}")]
        [ProducesResponseType(typeof(PastMedicalHistoryOutput), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<IActionResult> GetPastMedicalHistoryByPatientId([FromRoute]int patientId)
        {
            PastMedicalHistory pastMedicalHistory = await dbContext.PastMedicalHistories
               .FirstOrDefaultAsync(p => p.PatientId == patientId);

            if (pastMedicalHistory == null)
            {
                return NotFound(Json(new { Error = "该患者未填写既往病史" }));
            }

            PastMedicalHistoryOutput output = mapper.Map<PastMedicalHistoryOutput>(pastMedicalHistory);

            return new ObjectResult(output);
        }
        #endregion

        #region 创建既往病史
        /// <summary>
        /// 创建既往病史
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        [ProducesResponseType(typeof(PastMedicalHistoryOutput), 201)]
        [ProducesResponseType(typeof(ValidationError), 422)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<IActionResult> CreatePastMedicalHistory([FromBody]PastMedicalHistoryCreateInput input)
        {
            if (dbContext.PastMedicalHistories.Count(p => p.PatientId == input.PatientId) > 0)
            {
                return BadRequest(Json(new { Error = "患者已填写既往病史" }));
            }

            var pastMedicalHistory = mapper.Map<PastMedicalHistory>(input);
            dbContext.PastMedicalHistories.Add(pastMedicalHistory);
            await dbContext.SaveChangesAsync();

            return CreatedAtRoute("GetPastMedicalHistory", new { id = pastMedicalHistory.Id }, mapper.Map<PastMedicalHistoryOutput>(pastMedicalHistory));
        }
        #endregion

        #region 修改既往病史
        /// <summary>
        /// 修改既往病史
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
        public async Task<IActionResult> UpdatePastMedicalHistory([FromRoute]int id, [FromBody]PastMedicalHistoryUpdateInput input)
        {
            if (input.Id != id)
            {
                return BadRequest(Json(new { Error = "请求参数错误" }));
            }
            var pastMedicalHistory = await dbContext.PastMedicalHistories.FirstOrDefaultAsync(p => p.Id == id);
            if (pastMedicalHistory == null)
            {
                return NotFound(Json(new { Error = "该既往病史信息不存在" }));
            }

            dbContext.Entry(pastMedicalHistory).CurrentValues.SetValues(input);
            await dbContext.SaveChangesAsync();

            return new NoContentResult();
        }
        #endregion

        #endregion
    }
}