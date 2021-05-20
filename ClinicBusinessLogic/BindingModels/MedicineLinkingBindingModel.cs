using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicBusinessLogic.BindingModels
{
    public class MedicineLinkingBindingModel
    {
        public int MedicineId { get; set; }

        public int ReceiptId { get; set; }
    }
}
