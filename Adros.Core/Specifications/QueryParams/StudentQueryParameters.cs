using System.ComponentModel.DataAnnotations;

namespace Adros.Core.Specifications.QueryParams
{

    /// <summary>
    /// Represents parameters for querying students
    /// </summary>
    /// <remarks>
    /// Example request:
    /// GET /students?Take=10&amp;Skip=0&amp;Sort=name_desc
    /// </remarks>
    public class StudentQueryParameters
    {
        [StringLength(50)]
        public string? Name { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [StringLength(50)]
        public string? Government { get; set; }

        [StringLength(50)]
        public string? City { get; set; }

        [StringLength(50)]
        public string? Level { get; set; }

        [StringLength(50)]
        public string? Stage { get; set; }

        public bool? IsActive { get; set; }

        [StringLength(100)]
        public string? Search { get; set; }

        [RegularExpression(@"^[\w_]+(_desc)?(,[\w_]+(_desc)?)*$")]
        public string? Sort { get; set; }

        [Range(0, int.MaxValue)]
        public int? Skip { get; set; }

        [Range(1, 1000)]
        public int? Take { get; set; }
    }
}
