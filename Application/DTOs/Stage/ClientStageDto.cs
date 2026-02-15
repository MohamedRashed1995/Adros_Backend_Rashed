using Adros.Core.Enums;

namespace Adros.Application.DTOs.Stage
{
    public class ClientStageDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string ImageName { get; set; } = default!;
        public StageType? Type { get; set; }
    }
}
