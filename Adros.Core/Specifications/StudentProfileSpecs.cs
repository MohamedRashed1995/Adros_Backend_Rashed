using Adros.Core.Entities.Users;
using Adros.Shared.Specifications;

namespace Adros.Core.Specifications
{
    /// <summary>
    /// Specification to load a full student profile, including videos
    /// </summary>
    public class StudentProfileSpecs : BaseSpecification<Student>
    {
        public StudentProfileSpecs(Guid studentId)
            : base(s => s.ApplicationUserId == studentId)
        {
            // Load the related ApplicationUser
            Includes.Add(s => s.ApplicationUser);

            // Load the Level
            Includes.Add(s => s.Level);

            // Load the VideoDownloads
<<<<<<< HEAD
            Includes.Add(s => s.WatchLater);
=======
            Includes.Add(s => s.VideoDownloads);
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a

            // Load the VideoViews
            Includes.Add(s => s.VideoViews);

            // Also load the Video for each VideoView
            AddInclude("VideoViews.Video");
        }
    }
}
