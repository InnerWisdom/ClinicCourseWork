using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ClinicDatabaseImplement.Models
{
    public class Receipt
    {

        public int Id { get; set; }

        [Required]
        public string Dose { get; set; }
        [Required]
        public int PerDose { get; set; }

        public int? MedicineId { get; set; }

        public int? SymptomaticsId { get; set; }

    }
}
