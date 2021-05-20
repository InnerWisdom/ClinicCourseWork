using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicDatabaseImplement.Models
{
    public class Doctor
    {
        public int Id { get; set; }

        [Required]
        public string F_Name { get; set; }

        [Required]
        public string L_Name { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string EMail { get; set; }

        [ForeignKey("DoctorId")]
        public virtual List<Disease> Disease { get; set; }

        [ForeignKey("DoctorId")]
        public virtual List<Symptomatic> Symptomatic { get; set; }

        [ForeignKey("DoctorId")]
        public virtual List<DrugCourse> DrugCourse { get; set; }
    }
}