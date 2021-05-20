using System;

namespace ClinicBusinessLogic.ViewModels
{
    public class ReportDiseasesViewModel
    {
        public string Type { get; set; }
        public string DiseaseName { get; set; }

        public DateTime DateFormed { get; set; }

        public decimal Length { get; set; }

        public string DoctorName { get; set; }
    }
}