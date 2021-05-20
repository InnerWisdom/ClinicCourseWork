using System;

namespace ClinicBusinessLogic.ViewModels
{
    public class ReportReceiptDiseaseViewModel
    {
        public string DiseaseName { get; set; }

        public string Dose { get; set; }

        public int PerDose { get; set; }

        public int DoctorId { get; set; }

        public string MedicineName { get; set; }
    }
}
