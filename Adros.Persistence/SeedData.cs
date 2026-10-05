using Adros.Core.Entities;
using Adros.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adros.Persistence
{
    public static class SeedData
    {
        public static void Initialize(AppDbContext _context)
        {
            
            //var Role = _context.Roles.ToList();
            _context.Database.EnsureCreated();
            if (!_context.Roles.Any())
            {
                var roles = new []
                {
                    new IdentityRole<Guid> { Id=Guid.NewGuid(),Name="Admin",NormalizedName="ADMIN"},
                    new IdentityRole<Guid> { Id=Guid.NewGuid(),Name="Student",NormalizedName="STUDENT"},
                    new IdentityRole<Guid> { Id=Guid.NewGuid(),Name="Teacher",NormalizedName="TEACHER"}
                };
                _context.Roles.AddRange(roles);
                _context.SaveChanges();
            }
        }
    }
}
