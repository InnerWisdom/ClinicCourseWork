using ClinicBusinessLogic.BindingModels;
using ClinicBusinessLogic.ViewModels;
using System.Collections.Generic;

namespace ClinicBusinessLogic.Interfaces
{
    public interface IDrugCourseStorage
    {
        List<DrugCourseViewModel> GetFullList();

        List<DrugCourseViewModel> GetFilteredList(DrugCourseBindingModel model);

        DrugCourseViewModel GetElement(DrugCourseBindingModel model);

        void Insert(DrugCourseBindingModel model);

        void Update(DrugCourseBindingModel model);

        void Delete(DrugCourseBindingModel model);
    }
}