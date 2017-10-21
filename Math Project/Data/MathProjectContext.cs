using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MathProject.Models;

namespace MathProject.Models
{
    public class MathProjectContext : DbContext
    {
        public MathProjectContext (DbContextOptions<MathProjectContext> options)
            : base(options)
        {
        }

        public DbSet<MathProject.Models.Question> Question { get; set; }

        public DbSet<MathProject.Models.Hint> Hint { get; set; }
    }
}
