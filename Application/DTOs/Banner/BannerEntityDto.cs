namespace Adros.Application.DTOs.Banner
{
    //public class BannerEntityDto
    //{
    //    public Guid Id { get; set; }
    //    public string ImageUrl { get; set; } = default!;
    //    public int? Order { get; set; }
    //    public DateTime CreatedAt { get; set; }
    //    public DateTime UpdatedAt { get; set; }
    //    public Guid CreatedBy { get; set; }
    //    public Guid UpdatedBy { get; set; }
    //}
    public class BannerEntityDto
    {
        public Guid Id { get; set; }
        //public string Title { get; set; }
        public string? ImageUrl { get; set; }
<<<<<<< HEAD
        public int? Order { get; set; }
=======
        public int Order { get; set; }
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
    }
}
