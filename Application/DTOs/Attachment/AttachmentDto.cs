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

namespace Adros.Application.DTOs.Attachment
{
    public class AttachmentDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
<<<<<<< HEAD
        //public IFormFile FormFile { get; set; }
=======
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
    }
}
