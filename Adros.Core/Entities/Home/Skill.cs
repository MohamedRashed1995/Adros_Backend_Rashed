using Adros.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adros.Core.Entities.Home
{
    public class Skill : BaseEntity
    {
        public string VideoURL { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int ViewsCount { get; set; } = 0;
<<<<<<< HEAD
        
=======
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
    }
}
