using Adros.Core.Entities.Users;
using Adros.Shared.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adros.Core.Specifications
{
    public class TeacherSpecifications : BaseSpecification<Teacher>
    {
        public TeacherSpecifications(bool? isActive, string? sort, int page, int pageSize, Guid? teacherId = null)
            : base(t => !t.Deleted && (teacherId.HasValue ? t.Id == teacherId.Value : true))
        {
            // Filtering by activation status
            if (isActive.HasValue)
            {
                AddCriteria(t => t.ApplicationUser.IsActive == isActive.Value);
            }

            // Sorting
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort.ToLower())
                {
                    case "name":
                        ApplyOrderBy(t => t.ApplicationUser.UserName);
                        break;
                    case "email":
                        ApplyOrderBy(t => t.ApplicationUser.Email);
                        break;
                    case "lessoncount":
                        ApplyOrderByDescending(t => t.Lessons.Count);
                        break;
                    default:
                        ApplyOrderBy(t => t.ApplicationUser.UserName);
                        break;
                }
            }
            else
            {
                ApplyOrderBy(t => t.ApplicationUser.UserName);
            }

            // Pagination
            ApplyPaging((page - 1) * pageSize, pageSize);

            // Includes
            AddInclude(t => t.ApplicationUser);
            AddInclude(t => t.Lessons);
        }
      
            public TeacherSpecifications()
                : base(t => !t.Deleted && t.ApplicationUser.IsActive)
            {
                AddInclude(t => t.ApplicationUser);
                AddInclude(t => t.Lessons);
                ApplyOrderBy(t => t.CreatedAt); // Or any desired ordering
            }
        }
    }

