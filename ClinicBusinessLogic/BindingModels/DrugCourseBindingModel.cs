using System;
using System.Collections.Generic;

namespace ClinicBusinessLogic.BindingModels
{
    public class DrugCourseBindingModel
    {
        public int? Id { get; set; }

        public int? DoctorId { get; set; }

        public int? MedicineId { get; set; }

        public decimal Length { get; set; }

        public DateTime FormedDate { get; set; }

        public Dictionary<int, (string, int)> DrugCourseDiseases { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }
    }
}