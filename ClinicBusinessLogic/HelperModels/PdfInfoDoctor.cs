using ClinicBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;

namespace ClinicBusinessLogic.HelperModels
{
    public class PdfInfoDoctor
    {
        public string FileName { get; set; }

        public string Title { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public List<ReportDiseasesViewModel> Diseases { get; set; }
    }
}