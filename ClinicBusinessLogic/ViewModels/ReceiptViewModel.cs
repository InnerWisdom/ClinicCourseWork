using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ClinicBusinessLogic.ViewModels
{
    public class ReceiptViewModel
    {
        public int? Id { get; set; }

        public string Dose { get; set; }

        public int PerDose { get; set; }

        public int MedicineId { get; set; }
        public int? SymptomaticsId { get; set; }

    }
}