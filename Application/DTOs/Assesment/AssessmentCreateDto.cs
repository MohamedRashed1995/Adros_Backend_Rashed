using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adros.Application.DTOs.Assesment
{
    public class AssessmentCreateDto
    {
        public Guid TopicId { get; set; }
        public string Title { get; set; }
        public int Score { get; set; }
    }
}
