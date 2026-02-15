using Adros.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Adros.Application.DTOs.Calender
{
    public class CalenderCreateDto
    {
        public DateTime Date { get; set; }
        public string Notes { get; set; } = default!;

        [EnumDataType(typeof(CalenderColor), ErrorMessage = "Invalid color value")]
        public string Color { get; set; } = default!;
    }
}
