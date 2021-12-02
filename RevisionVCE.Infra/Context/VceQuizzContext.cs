using Microsoft.EntityFrameworkCore;
using RevisionVCE.Infra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace RevisionVCE.Infra.Context
{
    public class VceQuizzContext : DbContext
    {
        public VceQuizzContext(DbContextOptions<VceQuizzContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        //entities
        public DbSet<Question> Questions { get; set; }
        public DbSet<Choice> Choices { get; set; }
        public DbSet<Questionnaire> Questionnaires { get; set; }
    }
}
