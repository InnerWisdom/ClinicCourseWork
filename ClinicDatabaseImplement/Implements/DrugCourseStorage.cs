using ClinicBusinessLogic.BindingModels;
using ClinicBusinessLogic.Interfaces;
using ClinicBusinessLogic.ViewModels;
using ClinicDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClinicDatabaseImplement.Implements
{
    public class DrugCourseStorage : IDrugCourseStorage
    {
        public List<DrugCourseViewModel> GetFullList()
        {
            using (var context = new ClinicDataBase())
            {
                return context.DrugCourses
                .Include(rec => rec.Doctor)
                .Include(rec => rec.DrugCourseDiseases)
                .ThenInclude(rec => rec.Disease)
                .ToList()
                .Select(rec => new DrugCourseViewModel
                {
                    Id = rec.Id,
                    Length = rec.Length,
                    FormedDate = rec.FormedDate,
                    DrugCourseDiseases = rec.DrugCourseDiseases.ToDictionary(recRC => recRC.DiseaseId, recRC => (recRC.Disease?.DiseaseName, recRC.Count)),
                    DoctorId = rec.DoctorId,
                    MedicineId=rec.MedicineId
                })
                .ToList();
            }
        }

        public List<DrugCourseViewModel> GetFilteredList(DrugCourseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new ClinicDataBase())
            {
                return context.DrugCourses
                .Include(rec => rec.Doctor)
                .Include(rec => rec.DrugCourseDiseases)
                .ThenInclude(rec => rec.Disease)
                .Where(rec => (!model.DateFrom.HasValue && !model.DateTo.HasValue && rec.DoctorId == model.DoctorId || rec.FormedDate == model.FormedDate) ||
                (model.DateFrom.HasValue && model.DateTo.HasValue && (rec.DoctorId == model.DoctorId || rec.FormedDate.Date >= model.DateFrom.Value.Date && rec.FormedDate.Date <= model.DateTo.Value.Date)))
                .ToList()
                .Select(rec => new DrugCourseViewModel
                {
                    Id = rec.Id,
                    Length = rec.Length,
                    FormedDate = rec.FormedDate,
                    DrugCourseDiseases = rec.DrugCourseDiseases.ToDictionary(recRC => recRC.DiseaseId, recRC => (recRC.Disease?.DiseaseName, recRC.Count)),
                    DoctorId = rec.DoctorId,
                    MedicineId = rec.MedicineId
                })
                .ToList();
            }
        }

        public DrugCourseViewModel GetElement(DrugCourseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new ClinicDataBase())
            {
                DrugCourse drugCourse = context.DrugCourses
                .Include(rec => rec.Doctor)
                .Include(rec => rec.DrugCourseDiseases)
                .ThenInclude(rec => rec.Disease)
                .FirstOrDefault(rec => rec.FormedDate == model.FormedDate || rec.Id == model.Id);
                return drugCourse != null ? new DrugCourseViewModel
                {
                    Id = drugCourse.Id,
                    Length = drugCourse.Length,
                    FormedDate = drugCourse.FormedDate,
                    DrugCourseDiseases = drugCourse.DrugCourseDiseases.ToDictionary(recRC => recRC.DiseaseId, recRC => (recRC.Disease?.DiseaseName, recRC.Count)),
                    DoctorId = drugCourse.DoctorId,
                    MedicineId = drugCourse.MedicineId
                } : null;
            }
        }

        public void Insert(DrugCourseBindingModel model)
        {
            using (var context = new ClinicDataBase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        CreateModel(model, new DrugCourse(), context);
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void Update(DrugCourseBindingModel model)
        {
            using (var context = new ClinicDataBase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        DrugCourse element = context.DrugCourses.FirstOrDefault(rec => rec.Id == model.Id);

                        if (element == null)
                        {
                            throw new Exception("Элемент не найден");
                        }

                        CreateModel(model, element, context);
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void Delete(DrugCourseBindingModel model)
        {
            using (var context = new ClinicDataBase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {

                    try
                    {
                        DrugCourse element = context.DrugCourses.FirstOrDefault(rec => rec.Id == model.Id);
                        if (element != null)
                        {
                            context.DrugCourses.Remove(element);
                            context.SaveChanges();
                        }
                        else
                        {
                            throw new Exception("Элемент не найден");
                        }
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }

            }

        }

        private DrugCourse CreateModel(DrugCourseBindingModel model, DrugCourse drugCourse, ClinicDataBase context)
        {
            drugCourse.Length = model.Length;
            drugCourse.FormedDate = model.FormedDate;
            drugCourse.DoctorId = (int)model.DoctorId;
            drugCourse.MedicineId = model.MedicineId;

            if (drugCourse.Id == 0)
            {
                context.DrugCourses.Add(drugCourse);
                context.SaveChanges();
            }

            if (model.Id.HasValue)
            {
                var drugCourseDiseases = context.DrugCoursesDiseases.Where(rec => rec.DrugCourseId == model.Id.Value).ToList();
                context.DrugCoursesDiseases.RemoveRange(drugCourseDiseases.Where(rec => !model.DrugCourseDiseases.ContainsKey(rec.DiseaseId)).ToList());
                context.SaveChanges();

                foreach (var updateDisease in drugCourseDiseases)
                {
                    updateDisease.Count = model.DrugCourseDiseases[updateDisease.DiseaseId].Item2;
                    model.DrugCourseDiseases.Remove(updateDisease.DiseaseId);
                }
                context.SaveChanges();
            }
            foreach (var rc in model.DrugCourseDiseases)
            {
                context.DrugCoursesDiseases.Add(new DrugCourseDisease
                {
                    DrugCourseId = drugCourse.Id,
                    DiseaseId = rc.Key,
                    Count = rc.Value.Item2
                });
                context.SaveChanges();
            }
            return drugCourse;
        }
    }
}