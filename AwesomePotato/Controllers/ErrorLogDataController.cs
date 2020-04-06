using AutoMapper;
using AwesomePotato.DTOs;
using AwesomePotato.Models;
using AwesomePotato.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomePotato.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        [Route("listar-aplicacao/{aplicacao?}")]
        public ActionResult<IEnumerable<ErrorLogDataDTO>> GetDataByApplication(string aplicacao)
        {
            var result = errorLogDataService.FindByAplicationName(aplicacao);

            if (result.Count > 0)
                return Ok(mapper.Map<ErrorLogDataDTO>(result));
            else
                return NoContent();
        }

        [HttpGet]
        [Route("{aplicacao}/{nivel}")]
        public ActionResult<IEnumerable<ErrorLogDataDTO>> GetDataByApplicationAndLevel(string application, int level)
        {
            var result = errorLogDataService.FindByAplicationNameAndLevel(application, level);

            if (result.Count > 0)
                return Ok(mapper.Map<ErrorLogDataDTO>(result));
            else
                return NoContent();
        }

        [HttpGet]
        [Route("{aplicacao}/{token}")]
        public ActionResult<IEnumerable<ErrorLogDataDTO>> GetDataByApplicationAndToken(string application, Guid token)
        {
            var result = errorLogDataService.FindByAplicationNameAndToken(application, token);

            if (result.Count > 0)
                return Ok(mapper.Map<ErrorLogDataDTO>(result));
            else
                return NoContent();
        }

        [HttpGet]
        [Route("{aplicacao}/{token}/{nivel}")]
        public ActionResult<IEnumerable<ErrorLogDataDTO>> GetDataByApplicationAndTokenAndLevel(string application, Guid token, int level)
        {
            var result = errorLogDataService.FindByAplicationNameAndTokenAndLevel(application, token, level);

            if (result.Count > 0)
                return Ok(mapper.Map<ErrorLogDataDTO>(result));
            else
                return NoContent();
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<ErrorLogDataDTO> GetData(int id)
        {
            var result = errorLogDataService.FindById(id);

            if (result != null)
                return Ok(mapper.Map<ErrorLogDataDTO>(result));
            else
                return NoContent();
        }

        [HttpGet]
        [Route("{nivel}")]
        public ActionResult<IEnumerable<ErrorLogDataDTO>> GetDataByLevel(int level)
        {
            var result = errorLogDataService.FindByLevel(level);

            if (result.Count>0)
                return Ok(mapper.Map<ErrorLogDataDTO>(result));
            else
                return NoContent();
        }

        [HttpGet]
        [Route("{token}")]
        public ActionResult<IEnumerable<ErrorLogDataDTO>> GetDataByToken(Guid token)
        {
            var result = errorLogDataService.FindByToken(token);

            if (result.Count > 0)
                return Ok(mapper.Map<ErrorLogDataDTO>(result));
            else
                return NoContent();
        }

        [HttpGet]
        [Route("{token}/{nivel}")]
        public ActionResult<IEnumerable<ErrorLogDataDTO>> GetDataByTokenAndLevel(Guid token, int level)
        {
            var result = errorLogDataService.FindByTokenAndLevel(token, level);

            if (result.Count > 0)
                return Ok(mapper.Map<ErrorLogDataDTO>(result));
            else
                return NoContent();
        }

        [HttpGet]
        [Route("{data_inicio}/{data_fim}")]
        public ActionResult<IEnumerable<ErrorLogDataDTO>> GetDataByDateTime(DateTime startDate, DateTime endDate)
        {
            var result = errorLogDataService.FindByDateTime(startDate, endDate);

            if (result.Count > 0)
                return Ok(mapper.Map<ErrorLogDataDTO>(result));
            else
                return NoContent();
        }

        [HttpPost]
        public ActionResult<ErrorLogDataDTO> Post([FromBody] ErrorLogDataDTO value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            else
            {
                ErrorLogData data = mapper.Map<ErrorLogData>(value);
                errorLogDataService.Save(data);
                ErrorLogDataDTO userDTO = mapper.Map<ErrorLogDataDTO>(data);
                return Ok(userDTO);
            }
        }
    }
}
