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
    public class DiseaseStorage : IDiseaseStorage
    {
        public List<DiseaseViewModel> GetFullList()
        {
            using (var context = new ClinicDataBase())
            {
                return context.Diseases
                .Include(rec => rec.Doctor)
                .ToList()
                .Select(rec => new DiseaseViewModel
                {
                    Id = rec.Id,
                    DiseaseName = rec.DiseaseName,
                    Class = rec.Class,
                    Difficulty = rec.Difficulty,
                    DoctorId = rec.DoctorId
                })
                .ToList();
            }
        }

        public List<DiseaseViewModel> GetFilteredList(DiseaseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new ClinicDataBase())
            {
                return context.Diseases
                .Include(rec => rec.Doctor)
                .Where(rec => rec.DoctorId == model.DoctorId || rec.DiseaseName.Contains(model.DiseaseName))
                .ToList()
                .Select(rec => new DiseaseViewModel
                {
                    Id = rec.Id,
                    DiseaseName = rec.DiseaseName,
                    Class = rec.Class,
                    Difficulty = rec.Difficulty,
                    DoctorId = rec.DoctorId
                })
                .ToList();
            }
        }

        public DiseaseViewModel GetElement(DiseaseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new ClinicDataBase())
            {
                var tmp = model.DoctorId;
                var disease = context.Diseases
                .Include(rec => rec.Doctor)
                .FirstOrDefault(rec => rec.DiseaseName == model.DiseaseName || rec.Id == model.Id);
                return disease != null ?
                new DiseaseViewModel
                {
                    Id = disease.Id,
                    DiseaseName = disease.DiseaseName,
                    Difficulty = disease.Difficulty,
                    Class = disease.Class,
                    DoctorId = disease.DoctorId
                } :
                null;
            }
        }

        public void Insert(DiseaseBindingModel model)
        {
            using (var context = new ClinicDataBase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {

                    try
                    {
                        context.Diseases.Add(CreateModel(model, new Disease()));
                        context.SaveChanges();
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

        public void Update(DiseaseBindingModel model)
        {
            using (var context = new ClinicDataBase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {

                    try
                    {
                        var element = context.Diseases.FirstOrDefault(rec => rec.Id == model.Id);
                        if (element == null)
                        {
                            throw new Exception("Элемент не найден");
                        }
                        CreateModel(model, element);
                        context.SaveChanges();
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

        public void Delete(DiseaseBindingModel model)
        {
            using (var context = new ClinicDataBase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {

                    try
                    {
                        Disease element = context.Diseases.FirstOrDefault(rec => rec.Id == model.Id);
                        if (element != null)
                        {
                            context.Diseases.Remove(element);
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

        private Disease CreateModel(DiseaseBindingModel model, Disease disease)
        {
            disease.DiseaseName = model.DiseaseName;
            disease.Difficulty = model.Difficulty;
            disease.Class = model.Class;
            disease.DoctorId = (int)model.DoctorId;
            return disease;
        }
    }
}