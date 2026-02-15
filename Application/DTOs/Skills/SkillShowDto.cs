using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adros.Application.DTOs.Skills
{
    public class SkillShowDto
    {
        public Guid id { get; set; }
        public string title { get; set; } = default!;
        public string description { get; set; } = default!;
        public string videoURL { get; set; } = default!;
        public int viewsCount { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime? updatedAt { get; set; }
    }
}
