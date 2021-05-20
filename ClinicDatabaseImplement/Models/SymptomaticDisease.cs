using System.ComponentModel.DataAnnotations;

namespace ClinicDatabaseImplement.Models
{
    public class SymptomaticDisease
    {
        public int Id { get; set; }

        public int SymptomaticId { get; set; }

        public int DiseaseId { get; set; }

        [Required]
        public int Count { get; set; }

        public virtual Disease Disease { get; set; }
    }
}