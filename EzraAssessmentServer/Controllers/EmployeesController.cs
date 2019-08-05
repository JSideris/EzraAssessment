using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EzraAssessmentServer.Models.DTOs.Create;
using EzraAssessmentServer.Models.DTOs.Get;
using EzraAssessmentServer.Models.DTOs.Update;
using EzraAssessmentServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private IEmployeeService _service;

        public EmployeesController([FromServices] IEmployeeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<GetEmployeeDTO>> Get()
        {
            return await _service.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<GetEmployeeDTO> Get([FromRoute] int id)
        {
            return await _service.GetById(id);
        }

        [HttpPost]
        public async Task<GetEmployeeDTO> Post([FromBody] CreateEmployeeDTO value)
        {
            return await _service.Create(value);
        }

        [HttpPut("{id}")]
        public async Task<GetEmployeeDTO> Put([FromRoute] int id, [FromBody] UpdateEmployeeDTO value)
        {
            return await _service.Update(id, value);
        }

        [HttpDelete("{id}")]
        public async Task Delete([FromRoute] int id)
        {
            // await _service.HardDelete(id);
            await _service.SoftDelete(id);
        }
    }
}
