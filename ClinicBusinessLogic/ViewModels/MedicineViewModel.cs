using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ClinicBusinessLogic.ViewModels
{
    public class MedicineViewModel
    {
        public int Id { get; set; }


        [DisplayName("Название лекарства")]
        public string Name { get; set; }

        [DisplayName("Класс лекарства")]
        public string Class { get; set; }

        public int DrugCourseId { get; set; }
    }
}