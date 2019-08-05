
using EzraAssessmentServer.Models.DTOs.Create;
using EzraAssessmentServer.Models.DTOs.Get;
using EzraAssessmentServer.Models.DTOs.Update;
using EzraAssessmentServer.Models.Entities;

public interface IEmployeeService : IGenericService<CreateEmployeeDTO, GetEmployeeDTO, UpdateEmployeeDTO, Employee>{}