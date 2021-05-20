using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicDatabaseImplement.Models
{
    public class Symptomatic
    {
        public int Id { get; set; }

        public int DoctorId { get; set; }

        public virtual Doctor Doctor { get; set; }

        [Required]
        public string SymptomName { get; set; }

        [Required]
        public DateTime IssueDate { get; set; }

        [Required]
        public string Heart  { get; set; }

        [Required]
        public string Liver  { get; set; }

        [Required]
        public string Lungs  { get; set; }

        [ForeignKey("SymptomaticId")]
        public virtual List<SymptomaticDisease> SymptomaticDiseases { get; set; }

        public int? ReceiptId { get; set; }

        public virtual Receipt Receipt { get; set; }
    }
}