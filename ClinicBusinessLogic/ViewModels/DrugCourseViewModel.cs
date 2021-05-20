using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ClinicBusinessLogic.ViewModels
{
    public class DrugCourseViewModel
    {
        [DisplayName("Номер курса")]
        public int Id { get; set; }

        public int DoctorId { get; set; }
        public int? MedicineId { get; set; }

        [DisplayName("Длина курса")]
        public decimal Length { get; set; }

        [DisplayName("Дата формирования")]
        public DateTime FormedDate { get; set; }

        public Dictionary<int, (string, int)> DrugCourseDiseases { get; set; }

    }
}