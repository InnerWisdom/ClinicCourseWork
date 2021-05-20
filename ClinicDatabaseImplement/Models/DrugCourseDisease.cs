using System.ComponentModel.DataAnnotations;

namespace ClinicDatabaseImplement.Models
{
    public class DrugCourseDisease
    {
        public int Id { get; set; }

        public int DrugCourseId { get; set; }

        public int DiseaseId { get; set; }

        [Required]
        public int Count { get; set; }

        public virtual DrugCourse DrugCourse { get; set; }

        public virtual Disease Disease { get; set; }
    }
}