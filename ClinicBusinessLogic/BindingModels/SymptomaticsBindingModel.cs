using System;
using System.Collections.Generic;

namespace ClinicBusinessLogic.BindingModels
{
    public class SymptomaticsBindingModel
    {
        public int? Id { get; set; }

        public int? DoctorId { get; set; }

        public int? ReceiptId { get; set; }

        public string SymptomName { get; set; }

        public DateTime IssueDate { get; set; }

        public string Lungs { get; set; }

        public string Liver { get; set; }

        public string Heart { get; set; }

        public Dictionary<int, (string, int)> SymptomaticDiseases { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }
    }
}