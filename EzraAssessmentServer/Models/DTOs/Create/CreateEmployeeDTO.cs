using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EzraAssessmentServer.Models.DTOs.Create
{
    public class CreateEmployeeDTO
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone number is required.")]
        public string PhoneNumber { get; set; }
    }
}
