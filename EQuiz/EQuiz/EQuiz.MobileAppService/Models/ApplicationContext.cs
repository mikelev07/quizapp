using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EQuiz.MobileAppService.Models
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Question> Questions { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
       : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
