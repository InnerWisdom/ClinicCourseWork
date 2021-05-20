using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicBusinessLogic.BindingModels
{
    public class ReceiptBindingModel
    {
        public int? Id { get; set; }

        public string Dose { get; set; }

        public int PerDose { get; set; }

        public int? SymptomaticsId { get; set; }
        public int MedicineId { get; set; }

    }
}
