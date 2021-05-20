using ClinicBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;

namespace ClinicBusinessLogic.BindingModels
{
    public class ReportBindingModel
    {
        public string FileName { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public int? DoctorId { get; set; }
        public List<string> receiptDiseases { get; set; }
    }
}