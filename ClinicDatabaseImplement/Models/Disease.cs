using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicDatabaseImplement.Models
{
    public class Disease
    {
        public int Id { get; set; }

        public int DoctorId { get; set; }

        public virtual Doctor Doctor { get; set; }

        [Required]
        public string DiseaseName { get; set; }

        [Required]
        public string Class { get; set; }

        [Required]
        public decimal Difficulty { get; set; }

        

    }
}