using ClinicBusinessLogic.BindingModels;
using ClinicBusinessLogic.ViewModels;
using System.Collections.Generic;

namespace ClinicBusinessLogic.Interfaces
{
    public interface IMedicineStorage
    {
        List<MedicineViewModel> GetFullList();

        List<MedicineViewModel> GetFilteredList(MedicineBindingModel model);

        MedicineViewModel GetElement(MedicineBindingModel model);

        void Generate();

        void Insert(MedicineBindingModel model);

        void Update(MedicineBindingModel model);

        void Delete(MedicineBindingModel model);
    }
}
