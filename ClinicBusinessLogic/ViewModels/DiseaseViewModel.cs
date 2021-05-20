using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ClinicBusinessLogic.ViewModels
{
    public class DiseaseViewModel
    {
        public int Id { get; set; }

        public int DoctorId { get; set; }

        [DisplayName("Название болезни")]
        public string DiseaseName { get; set; }

        [DisplayName("Класс болезни")]
        public string Class { get; set; }
        [DisplayName("Сложность")]
        public decimal Difficulty { get; set; }
    }
}
