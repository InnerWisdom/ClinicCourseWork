using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicBusinessLogic.BindingModels
{
    public class DrugCourseLinkingBindingModel
    {
        public int DrugCourseId { get; set; }

        public int MedicineId { get; set; }
    }
}
