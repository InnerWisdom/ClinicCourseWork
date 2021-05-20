using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicBusinessLogic.BindingModels
{
    public class MedicineBindingModel
    {

        public int? Id { get; set; }

        public string Name { get; set; }

        public string Class { get; set; }

        public int DrugCourseId { get; set; }

    }
}
