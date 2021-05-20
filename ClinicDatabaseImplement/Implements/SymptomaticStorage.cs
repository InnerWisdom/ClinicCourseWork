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
    public class SymptomaticStorage : ISymptomaticStorage
    {
        public List<SymptomaticViewModel> GetFullList()
        {
            using (var context = new ClinicDataBase())
            {
                return context.Symptomatics
                .Include(rec => rec.Doctor)
                .Include(rec => rec.Receipt)
                .Include(rec => rec.SymptomaticDiseases)
                .ThenInclude(rec => rec.Disease)
                .ToList()
                .Select(rec => new SymptomaticViewModel
                {
                    Id = rec.Id,
                    IssueDate = rec.IssueDate,
                    SymptomName = rec.SymptomName,
                    Heart = rec.Heart,
                    Liver = rec.Liver,
                    Lungs = rec.Lungs,
                    SymptomaticDiseases = rec.SymptomaticDiseases.ToDictionary(recDC => recDC.DiseaseId, recDC => (recDC.Disease?.DiseaseName, recDC.Count)),
                    DoctorId = rec.DoctorId,
                    ReceiptId = rec.ReceiptId
                })
                .ToList();
            }
        }

        public List<SymptomaticViewModel> GetFilteredList(SymptomaticsBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new ClinicDataBase())
            {
                return context.Symptomatics
                .Include(rec => rec.Doctor)
                .Include(rec => rec.Receipt)
                .Include(rec => rec.SymptomaticDiseases)
                .ThenInclude(rec => rec.Disease)
                .Where(rec => (!model.DateFrom.HasValue && !model.DateTo.HasValue && rec.DoctorId == model.DoctorId || rec.IssueDate == model.IssueDate) ||
                (model.DateFrom.HasValue && model.DateTo.HasValue && (rec.DoctorId == model.DoctorId || rec.IssueDate.Date >= model.DateFrom.Value.Date && rec.IssueDate.Date <= model.DateTo.Value.Date)))
                .ToList()
                .Select(rec => new SymptomaticViewModel
                {
                    Id = rec.Id,
                    IssueDate = rec.IssueDate,
                    SymptomName = rec.SymptomName,
                    Heart = rec.Heart,
                    Liver = rec.Liver,
                    Lungs = rec.Lungs,
                    SymptomaticDiseases = rec.SymptomaticDiseases.ToDictionary(recDC => recDC.DiseaseId, recDC => (recDC.Disease?.DiseaseName, recDC.Count)),
                    DoctorId = rec.DoctorId,
                    ReceiptId = rec.ReceiptId
                })
                .ToList();
            }
        }

        public SymptomaticViewModel GetElement(SymptomaticsBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new ClinicDataBase())
            {
                Symptomatic symptomatic = context.Symptomatics
                .Include(rec => rec.Doctor)
                .Include(rec => rec.Receipt)
                .Include(rec => rec.SymptomaticDiseases)
                .ThenInclude(rec => rec.Disease)
                .FirstOrDefault(rec => rec.IssueDate == model.IssueDate || rec.Id == model.Id);
                return symptomatic != null ? new SymptomaticViewModel
                {
                    Id = symptomatic.Id,
                    IssueDate = symptomatic.IssueDate,
                    SymptomName = symptomatic.SymptomName,
                    Heart = symptomatic.Heart,
                    Liver = symptomatic.Liver,
                    Lungs = symptomatic.Lungs,
                    SymptomaticDiseases = symptomatic.SymptomaticDiseases.ToDictionary(recDC => recDC.DiseaseId, recDC => (recDC.Disease?.DiseaseName, recDC.Count)),
                    DoctorId = symptomatic.DoctorId,
                    ReceiptId = symptomatic.ReceiptId
                } : null;
            }
        }

        public void Insert(SymptomaticsBindingModel model)
        {
            using (var context = new ClinicDataBase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        CreateModel(model, new Symptomatic(), context);
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

        public void Update(SymptomaticsBindingModel model)
        {
            using (var context = new ClinicDataBase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Symptomatic element = context.Symptomatics.FirstOrDefault(rec => rec.Id == model.Id);

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

        public void Delete(SymptomaticsBindingModel model)
        {
            using (var context = new ClinicDataBase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {

                    try
                    {
                        Symptomatic element = context.Symptomatics.FirstOrDefault(rec => rec.Id == model.Id);
                        if (element != null)
                        {
                            context.Symptomatics.Remove(element);
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

        private Symptomatic CreateModel(SymptomaticsBindingModel model, Symptomatic symptomatic, ClinicDataBase context)
        {
            symptomatic.IssueDate = model.IssueDate;
            symptomatic.SymptomName = model.SymptomName;
            symptomatic.Liver = model.Liver;
            symptomatic.Lungs = model.Lungs;
            symptomatic.Heart = model.Heart;
            symptomatic.Heart = model.Heart;
            symptomatic.Liver = model.Liver;
            symptomatic.Lungs = model.Lungs;
            symptomatic.DoctorId = (int)model.DoctorId;
            symptomatic.ReceiptId = model.ReceiptId;

            if (symptomatic.Id == 0)
            {
                context.Symptomatics.Add(symptomatic);
                context.SaveChanges();
            }

            if (model.Id.HasValue)
            {
                var symptomaticDiseases = context.SymptomaticDiseases.Where(rec => rec.SymptomaticId == model.Id.Value).ToList();
                context.SymptomaticDiseases.RemoveRange(symptomaticDiseases.Where(rec => !model.SymptomaticDiseases.ContainsKey(rec.DiseaseId)).ToList());
                context.SaveChanges();

                foreach (var updateDisease in symptomaticDiseases)
                {
                    updateDisease.Count = model.SymptomaticDiseases[updateDisease.DiseaseId].Item2;
                    model.SymptomaticDiseases.Remove(updateDisease.DiseaseId);
                }
                context.SaveChanges();
            }
            foreach (var dc in model.SymptomaticDiseases)
            {
                context.SymptomaticDiseases.Add(new SymptomaticDisease
                {
                    SymptomaticId = symptomatic.Id,
                    DiseaseId = dc.Key,
                    Count = dc.Value.Item2
                });
                context.SaveChanges();
            }
            return symptomatic;
        }
    }
}