using ClinicBusinessLogic.BindingModels;
using ClinicBusinessLogic.ViewModels;
using System.Collections.Generic;

namespace ClinicBusinessLogic.Interfaces
{
    public interface IDiseaseStorage
    {
        List<DiseaseViewModel> GetFullList();

        List<DiseaseViewModel> GetFilteredList(DiseaseBindingModel model);

        DiseaseViewModel GetElement(DiseaseBindingModel model);

        void Insert(DiseaseBindingModel model);

        void Update(DiseaseBindingModel model);

        void Delete(DiseaseBindingModel model);
    }
}