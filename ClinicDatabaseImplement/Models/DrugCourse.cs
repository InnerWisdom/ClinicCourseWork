using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicDatabaseImplement.Models
{
    public class DrugCourse
    {
        public int Id { get; set; }

        public int DoctorId { get; set; }

        public virtual Doctor Doctor { get; set; }

        [Required]
        public decimal Length { get; set; }

        [Required]
        public DateTime FormedDate { get; set; }

        [ForeignKey("DrugCourseId")]
        public virtual List<DrugCourseDisease> DrugCourseDiseases { get; set; }
        public int? MedicineId { get; set; }

        public virtual Medicine Medicine { get; set; }
    }
}