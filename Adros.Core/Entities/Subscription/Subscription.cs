using Adros.Core.Entities.Course;
using Adros.Core.Entities.Users;
using Adros.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adros.Core.Entities.Subscription  
{
    public class Subscription : BaseEntity
    {
        [Required]
        public string Name { get; set; } // Monthly, Annual, أو أي اسم
        [Required]
        public string Price { get; set; } // ممكن نخليه string عشان يظهر بالـ UI
        [Required]
        public string Duration { get; set; } // Monthly, Annually

        public List<string> Benefits { get; set; } = new(); // List of benefits كـ string مفصولة بفواصل
    }

}
