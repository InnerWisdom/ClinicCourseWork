using ClinicBusinessLogic.ViewModels;
using System.Collections.Generic;
using System.Text;

namespace ClinicBusinessLogic.HelperModels
{
    class ExcelInfoDoctor
    {
        public string FileName { get; set; }

        public string Title { get; set; }
        public List<ReportReceiptDiseaseViewModel> Receipts { get; set; }
    }
}
