using EzraAssessmentServer.Db;
using EzraAssessmentServer.Models.DTOs.Create;
using EzraAssessmentServer.Models.DTOs.Get;
using EzraAssessmentServer.Models.DTOs.Update;
using EzraAssessmentServer.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EzraAssessmentServer.Services
{
    /// <summary>
    /// Basic CRUD for employees.
    /// </summary>
    public class EmployeeService : GenericService<CreateEmployeeDTO, GetEmployeeDTO, UpdateEmployeeDTO, Employee>, IEmployeeService
    {
        public EmployeeService([FromServices] EzraAssessmentDbContext context) : base(context)
        {
        }
    }
}
