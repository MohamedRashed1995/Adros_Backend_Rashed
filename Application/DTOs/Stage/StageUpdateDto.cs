using Adros.Core.Enums;
using Adros.Shared;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adros.Application.DTOs.Stage
{
    public class StageUpdateDto 
    {
        //public Guid Id { get; set; }
        public string? Title { get; set; }
        public IFormFile? Image { get; set; }
        public int? Order { get; set; }
        public StageType? Type { get; set; }
    }
}
