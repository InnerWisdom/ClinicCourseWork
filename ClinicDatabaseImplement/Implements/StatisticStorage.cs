using ClinicBusinessLogic.BindingModels;
using ClinicBusinessLogic.Interfaces;
using ClinicBusinessLogic.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace ClinicDatabaseImplement.Implements
{
    public class StatisticStorage : IStatisticStorage
    {
        public List<ReportDiseasesViewModel> GetSymptomatics(ReportBindingModel model)
        {
            using (var context = new ClinicDataBase())
            {
                return (
                from dis in context.Diseases
                join sympD in context.SymptomaticDiseases on dis.Id equals sympD.DiseaseId
                join s in context.Symptomatics on sympD.SymptomaticId equals s.Id
                where s.DoctorId == model.DoctorId
                where s.IssueDate >= model.DateFrom
                where s.IssueDate <= model.DateTo
                select new ReportDiseasesViewModel
                {
                    Type = "Выписка о симптоме ",
                    DiseaseName = dis.DiseaseName,
                    DateFormed = s.IssueDate,
                    Length = sympD.Count
                })
                .ToList();
            }
        }

        public List<ReportDiseasesViewModel> GetSymptomaticsForAll(ReportBindingModel model)
        {
            using (var context = new ClinicDataBase())
            {
                return (
                from dis in context.Diseases
                join sympD in context.SymptomaticDiseases on dis.Id equals sympD.DiseaseId
                join s in context.Symptomatics on sympD.SymptomaticId equals s.Id
                where s.IssueDate >= model.DateFrom
                where s.IssueDate <= model.DateTo
                select new ReportDiseasesViewModel
                {
                    Type = "Выписка о симптоме ",
                    DiseaseName = dis.DiseaseName,
                    DateFormed = s.IssueDate,
                    Length = sympD.Count
                })
                .ToList();
            }
        }

        public List<ReportDiseasesViewModel> GetDrugCourses(ReportBindingModel model)
        {
            using (var context = new ClinicDataBase())
            {
                return (
                from dis in context.Diseases
                join drC in context.DrugCoursesDiseases on dis.Id equals drC.DiseaseId
                join c in context.DrugCourses on drC.DrugCourseId equals c.Id
                where c.DoctorId == model.DoctorId
                where c.FormedDate >= model.DateFrom
                where c.FormedDate <= model.DateTo
                select new ReportDiseasesViewModel
                {
                    Type = "Курс препаратов",
                    DateFormed = c.FormedDate,
                    DiseaseName = dis.DiseaseName,
                    Length = drC.Count
                })
                .ToList();
            }
        }

        public List<ReportDiseasesViewModel> GetDrugCoursesForAll(ReportBindingModel model)
        {
            using (var context = new ClinicDataBase())
            {
                return (
                from dis in context.Diseases
                join drC in context.DrugCoursesDiseases on dis.Id equals drC.DiseaseId
                join c in context.DrugCourses on drC.DrugCourseId equals c.Id
                where c.FormedDate >= model.DateFrom
                where c.FormedDate <= model.DateTo
                select new ReportDiseasesViewModel
                {
                    Type = "Курс препаратов",
                    DateFormed = c.FormedDate,
                    DiseaseName = dis.DiseaseName,
                    Length = drC.Count
                })
                .ToList();
            }
        }
    }
}
