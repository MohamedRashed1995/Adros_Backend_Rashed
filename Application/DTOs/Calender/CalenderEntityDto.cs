namespace Adros.Application.DTOs.Calender
{
    public class CalenderEntityDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; } = default!;
        public string Color { get; set; } = default!;
    }
}
