<<<<<<< HEAD
﻿using Microsoft.AspNetCore.Http;
using System;
=======
﻿using System;
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adros.Application.DTOs.Skills
{
    public class SkillDto
    {
<<<<<<< HEAD
   
        public string title { get; set; } = default!;
        public string description { get; set; } = default!;
        public string videoURL { get; set; } = default!;
        
        
=======
        public Guid id { get; set; }
        public string title { get; set; } = default!;
        public string description { get; set; } = default!;
        public string videoURL { get; set; } = default!;
        public int viewsCount { get; set; }

        public DateTime createdAt { get; set; }
        public DateTime? updatedAt { get; set; }
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
    }


}
