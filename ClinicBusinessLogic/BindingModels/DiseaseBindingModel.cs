using System;
using System.Collections.Generic;
namespace ClinicBusinessLogic.BindingModels
{
    public class DiseaseBindingModel
    {
        public int? Id { get; set; }

        public int? DoctorId { get; set; }

        public string DiseaseName { get; set; }

        public string Class { get; set; }

        public decimal Difficulty { get; set; }
    }
}
