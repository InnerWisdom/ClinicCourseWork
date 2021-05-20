using ClinicDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicDatabaseImplement
{
    public class ClinicDataBase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=(LocalDb)\v11.0;Initial Catalog=ClinicDatabaseInitialFinalFixed;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }

        public virtual DbSet<Disease> Diseases { set; get; }

        public virtual DbSet<Symptomatic> Symptomatics { set; get; }

        public virtual DbSet<SymptomaticDisease> SymptomaticDiseases { set; get; }

        public virtual DbSet<Doctor> Doctors { set; get; }

        public virtual DbSet<DrugCourse> DrugCourses { set; get; }

        public virtual DbSet<DrugCourseDisease> DrugCoursesDiseases { set; get; }

        public virtual DbSet<Medicine> Medicines { set; get; }

        public virtual DbSet<Receipt> Receipts { set; get; }
    }
}