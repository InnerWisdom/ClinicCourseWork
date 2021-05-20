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
    public class MedicineStorage : IMedicineStorage
    {
        public List<MedicineViewModel> GetFullList()
        {
            using (var context = new ClinicDataBase())
            {
                return context.Medicines
                .Select(rec => new MedicineViewModel
                {
                    Id = rec.Id,
                    Name = rec.Name,
                    Class = rec.Class,
                    DrugCourseId = rec.DrugCourseId
                })
                .ToList();
            }
        }

        public List<MedicineViewModel> GetFilteredList(MedicineBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new ClinicDataBase())
            {
                return context.Medicines
                .Select(rec => new MedicineViewModel
                {
                    Id = rec.Id,
                    Name = rec.Name,
                    Class = rec.Class,
                    DrugCourseId = rec.DrugCourseId
                })
                .ToList();
            }
        }

        public MedicineViewModel GetElement(MedicineBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new ClinicDataBase())
            {
                var medicine = context.Medicines
                .FirstOrDefault(rec => rec.Name == model.Name || rec.Id == model.Id);
                return medicine != null ?
                new MedicineViewModel
                {
                    Id = medicine.Id,
                    Name = medicine.Name,
                    Class = medicine.Class,
                    DrugCourseId = medicine.DrugCourseId
                } :
                null;
            }
        }

        public void Generate()
        {
            using (var context = new ClinicDataBase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    string[] Classes = new string[] { "Болеутоляющее", "Обезболивающее", "A-48", "B-37", "K-18", "AB-12", "ARS", "GARS" };
                    string[] Names = new string[] { "Пурен", "Вамин", "Квазиб", "Анахрон", "Пурпур", "Моерон", "Амоерон", "Грязерон" };
                    try
                    {
                        for (int i = 0; i < Classes.Length; i++)
                        {
                            Insert(new MedicineBindingModel
                            {
                                Class = Classes[i],
                                Name = Names[i]
                            });
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

        public void Insert(MedicineBindingModel model)
        {
            using (var context = new ClinicDataBase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {

                    try
                    {
                        context.Medicines.Add(CreateModel(model, new Medicine()));
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

        public void Update(MedicineBindingModel model)
        {
            using (var context = new ClinicDataBase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {

                    try
                    {
                        var element = context.Medicines.FirstOrDefault(rec => rec.Id == model.Id);
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

        public void Delete(MedicineBindingModel model)
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

        private Medicine CreateModel(MedicineBindingModel model, Medicine medicine)
        {
            medicine.Name = model.Name;
            medicine.Class = model.Class;
            medicine.DrugCourseId = model.DrugCourseId;
            return medicine;
        }
    }
}