using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ClinicBusinessLogic.ViewModels
{
    public class SymptomaticViewModel
    {
        [DisplayName("Номер выдачи")]
        public int Id { get; set; }

        public int DoctorId { get; set; }

        public int? ReceiptId { get; set; }


        [DisplayName("Название симптома")]
        public string SymptomName { get; set; }

        [DisplayName("Дата постановки")]
        public DateTime IssueDate { get; set; }

        [DisplayName("Легкие")]
        public string Lungs { get; set; }

        [DisplayName("Печень")]
        public string Liver { get; set; }

        [DisplayName("Сердце")]
        public string Heart { get; set; }

        public Dictionary<int, (string, int)> SymptomaticDiseases { get; set; }
    }
}