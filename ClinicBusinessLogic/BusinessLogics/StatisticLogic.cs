using ClinicBusinessLogic.BindingModels;
using ClinicBusinessLogic.Interfaces;
using ClinicBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClinicBusinessLogic.BusinessLogics
{
    public class StatisticLogic
    {
        private readonly IStatisticStorage _statisticStorage;

        public StatisticLogic(IStatisticStorage statisticStorage)
        {
            _statisticStorage = statisticStorage;
        }

        public List<Tuple<string, decimal>> GetDrugCourseStatistic(ReportBindingModel model)
        {
            var list = new List<ReportDiseasesViewModel>();
            if (model.DoctorId != 0)
            {
                list = _statisticStorage.GetDrugCourses(model);
            }
            else
            {
                list = _statisticStorage.GetDrugCoursesForAll(model);
            }
            return list.OrderBy(rec => rec.DiseaseName).GroupBy(rec => new { rec.DiseaseName, rec.Length }).Select(rec => new Tuple<string, decimal>(rec.Key.DiseaseName, rec.Key.Length)).ToList();
        }

        public List<Tuple<string, decimal>> GetSymptomaticStatistic(ReportBindingModel model)
        {
            var list = new List<ReportDiseasesViewModel>();
            if (model.DoctorId != 0)
            {
                list = _statisticStorage.GetSymptomatics(model);
            }
            else
            {
                list = _statisticStorage.GetSymptomaticsForAll(model);
            }
            return list.OrderBy(rec => rec.DiseaseName).GroupBy(rec => new { rec.DiseaseName, rec.Length }).Select(rec => new Tuple<string, decimal>(rec.Key.DiseaseName, rec.Key.Length)).ToList();
        }
    }
}
