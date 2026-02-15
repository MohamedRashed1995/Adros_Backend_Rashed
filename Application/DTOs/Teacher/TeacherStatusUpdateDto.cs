using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adros.Application.DTOs.Teacher
{
    public class TeacherStatusUpdateDto
    {
        [Required]
        public bool IsActive { get; set; }
    }
}
