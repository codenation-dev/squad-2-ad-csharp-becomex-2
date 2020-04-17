using AutoMapper;
using AwesomePotato.DTOs;
using AwesomePotato.Models;
using AwesomePotato.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace AwesomePotato.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Usuario")]
    public class ErrorLogDataController : ControllerBase
    {
        private readonly IErrorLogDataService errorLogDataService;
        private readonly IMapper mapper;

        public ErrorLogDataController(IErrorLogDataService service, IMapper mapper)
        {
            errorLogDataService = service;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("listar-erros")]
        public ActionResult<IEnumerable<ErrorLogDataDTO>> GetFilteredData(
            [FromQuery] string aplicacao,
            [FromQuery] string token,
            [FromQuery] int? nivel,
            [FromQuery] string dataInicio,
            [FromQuery] string dataFim)
        {
            var result = errorLogDataService.FilterByApplicationTokenLevelDate(aplicacao, token, nivel, dataInicio, dataFim);

            if (result.Count > 0)
                return Ok(mapper.Map<ErrorLogDataDTO>(result));
            else
                return NoContent();
        }


        [HttpGet]
        [Route("listar")]
        public ActionResult<ErrorLogDataDTO> GetData([FromQuery] int id)
        {
            var result = errorLogDataService.FindById(id);

            if (result != null)
                return Ok(mapper.Map<ErrorLogDataDTO>(result));
            else
                return NoContent();
        }

        [HttpPost]
        public ActionResult<ErrorLogDataDTO> Post([FromBody] ErrorLogDataDTO value)
        {
            if (!ModelState.IsValid || value == null)
                return BadRequest(ModelState);
            else
            {
                value.TimeStamp = DateTime.Now;
                ErrorLogData data = mapper.Map<ErrorLogData>(value);
                errorLogDataService.Save(data);
                ErrorLogDataDTO userDTO = mapper.Map<ErrorLogDataDTO>(data);
                return Ok(userDTO);
            }
        }
    }
}
