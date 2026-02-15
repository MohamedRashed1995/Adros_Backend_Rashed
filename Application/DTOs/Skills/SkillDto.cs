using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adros.Application.DTOs.Skills
{
    public class SkillDto
    {
   
        public string title { get; set; } = default!;
        public string description { get; set; } = default!;
        public string videoURL { get; set; } = default!;
        
        
    }


}
