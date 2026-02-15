using Adros.Core.Entities.Users;
using Adros.Shared.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adros.Core.Specifications
{
    public class StudentByApplicationUserIdSpecification : BaseSpecification<Student>
    {
        public StudentByApplicationUserIdSpecification(Guid applicationUserId)
            : base(s => s.ApplicationUserId == applicationUserId) // هنا الرابط الصح
        {
            AddInclude(s => s.Level); // لو حابب تجيب Level مع الطالب
        }
    }

}
