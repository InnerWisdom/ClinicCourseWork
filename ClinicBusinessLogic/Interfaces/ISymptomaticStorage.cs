using ClinicBusinessLogic.BindingModels;
using ClinicBusinessLogic.ViewModels;
using System.Collections.Generic;

namespace ClinicBusinessLogic.Interfaces
{
    public interface ISymptomaticStorage
    {
        List<SymptomaticViewModel> GetFullList();

        List<SymptomaticViewModel> GetFilteredList(SymptomaticsBindingModel model);

        SymptomaticViewModel GetElement(SymptomaticsBindingModel model);

        void Insert(SymptomaticsBindingModel model);

        void Update(SymptomaticsBindingModel model);

        void Delete(SymptomaticsBindingModel model);
    }
}