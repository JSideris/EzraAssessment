using EzraAssessmentServer.Models.DTOs.Create;
using EzraAssessmentServer.Models.DTOs.Get;
using EzraAssessmentServer.Models.DTOs.Update;
using EzraAssessmentServer.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EzraAssessmentServer.Configuration
{
    public static class AutoMapperConfig
    {
        public static void Init() {
            // Initialize the static automapper
#pragma warning disable CS0618 // Type or member is obsolete
            AutoMapper.Mapper.Initialize(cfg => {
                cfg.CreateMap<Employee, GetEmployeeDTO>().ReverseMap();
                cfg.CreateMap<CreateEmployeeDTO, Employee>();
                cfg.CreateMap<UpdateEmployeeDTO, Employee>();
            });
#pragma warning restore CS0618 // Type or member is obsolete
        }
    }
}
