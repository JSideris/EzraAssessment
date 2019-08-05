using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EzraAssessmentServer.Models.DTOs.Update
{
    public class UpdateEmployeeDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
