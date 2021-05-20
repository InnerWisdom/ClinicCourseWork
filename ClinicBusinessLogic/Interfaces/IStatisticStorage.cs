using ClinicBusinessLogic.BindingModels;
using ClinicBusinessLogic.ViewModels;
using System.Collections.Generic;
namespace ClinicBusinessLogic.Interfaces
{
    public interface IStatisticStorage
    {
        List<ReportDiseasesViewModel> GetDrugCourses(ReportBindingModel model);

        List<ReportDiseasesViewModel> GetSymptomatics(ReportBindingModel model);

        List<ReportDiseasesViewModel> GetDrugCoursesForAll(ReportBindingModel model);

        List<ReportDiseasesViewModel> GetSymptomaticsForAll(ReportBindingModel model);
    }
}
