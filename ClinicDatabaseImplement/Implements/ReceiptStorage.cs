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
    public class ReceiptStorage : IReceiptStorage
    {
        int number = 1;

        public List<ReceiptViewModel> GetFullList()
        {
            using (var context = new ClinicDataBase())
            {
                return context.Receipts
                .Select(rec => new ReceiptViewModel
                {
                    Id = rec.Id,
                    Dose = rec.Dose,
                    PerDose = rec.PerDose,
                    MedicineId = (int)rec.MedicineId,
                    SymptomaticsId = rec.SymptomaticsId
                    })
                .ToList();
            }
        }

        public List<ReceiptViewModel> GetFilteredList(ReceiptBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new ClinicDataBase())
            {
                return context.Receipts
                .Where(rec => rec.SymptomaticsId==model.SymptomaticsId)
                .Select(rec => new ReceiptViewModel
                {
                    Id = rec.Id,
                    Dose = rec.Dose,
                    PerDose = rec.PerDose,
                    MedicineId = (int)rec.MedicineId,
                    SymptomaticsId = rec.SymptomaticsId
                })
                .ToList();
            }
        }
        public void Generate()
        {
            using (var context = new ClinicDataBase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {

                    string[] Dose = new string[] { "12-S", "15-K", "1-D", "7-N", "8-G", "25-AR", "32-S", "11-G" };
                    int[] PerDose = new int[] { 55, 6, 32, 18, 48, 1, 5, 4};
                    try
                    {

                        for (int i = 0; i < Dose.Length; i++)
                        {
                            Insert(new ReceiptBindingModel
                            {
                                Dose = Dose[i],
                                PerDose = PerDose[i],
                                MedicineId = number
                            });
                            number++;
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

        public ReceiptViewModel GetElement(ReceiptBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new ClinicDataBase())
            {
                var medicine = context.Receipts
                           .FirstOrDefault(rec => rec.Id == model.Id);
                return medicine != null ?
                new ReceiptViewModel
                {
                    Id = medicine.Id,
                    Dose = medicine.Dose,
                    PerDose = medicine.PerDose,
                    MedicineId = (int)medicine.MedicineId,
                    SymptomaticsId = medicine.SymptomaticsId
                } :
                null;
            }
        }

        public void Insert(ReceiptBindingModel model)
        {
            using (var context = new ClinicDataBase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {

                    try
                    {
                        context.Receipts.Add(CreateModel(model, new Receipt()));
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

        public void Update(ReceiptBindingModel model)
        {
            using (var context = new ClinicDataBase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {

                    try
                    {
                        var element = context.Receipts.FirstOrDefault(rec => rec.Id == model.Id);
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

        public void Delete(ReceiptBindingModel model)
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

        private Receipt CreateModel(ReceiptBindingModel model, Receipt medicine)
        {
            medicine.Dose = model.Dose;
            medicine.PerDose = model.PerDose;
            medicine.MedicineId = model.MedicineId;
            medicine.SymptomaticsId = model.SymptomaticsId;
            return medicine;
        }

    }
}
