using ClinicBusinessLogic.BindingModels;
using ClinicBusinessLogic.Interfaces;
using ClinicBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;

namespace ClinicBusinessLogic.BusinessLogics
{
    public class SymptomaticLogic
    {
        private readonly ISymptomaticStorage _symptomaticStorage;

        private readonly IReceiptStorage _receiptStorage;

        private readonly IMedicineStorage _medicineStorage;

        public SymptomaticLogic(ISymptomaticStorage symptomaticStorage, IReceiptStorage receiptStorage, IMedicineStorage medicineStorage)
        {
            _symptomaticStorage = symptomaticStorage;
            _receiptStorage = receiptStorage;
            _medicineStorage = medicineStorage;
        }

        public List<SymptomaticViewModel> Read(SymptomaticsBindingModel model)
        {
            if (model == null)
            {
                return _symptomaticStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<SymptomaticViewModel> { _symptomaticStorage.GetElement(model) };
            }
            return _symptomaticStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(SymptomaticsBindingModel model)
        {
            var element = _symptomaticStorage.GetElement(new SymptomaticsBindingModel { IssueDate = model.IssueDate });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже произведена выдача в данное время");
            }
            if (model.Id.HasValue)
            {
                _symptomaticStorage.Update(model);
            }
            else
            {
                _symptomaticStorage.Insert(model);
            }
        }

        public void Delete(SymptomaticsBindingModel model)
        {
            var element = _symptomaticStorage.GetElement(new SymptomaticsBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Выдача не найдена");
            }
            _symptomaticStorage.Delete(model);
        }

        public void Linking(SymptomaticLinkingBindingModel model)
        {
            var symptomatic = _symptomaticStorage.GetElement(new SymptomaticsBindingModel
            {
                Id = model.SymptomaticId
            });

            var receipt = _receiptStorage.GetElement(new ReceiptBindingModel
            {
                Id = model.ReceiptId
            });

            if (symptomatic == null)
            {
                throw new Exception("Не найдена выдача");
            }

            if (receipt == null)
            {
                throw new Exception("Не найдено выписка");
            }
               
            _symptomaticStorage.Update(new SymptomaticsBindingModel
            {
                Id = symptomatic.Id,
                IssueDate = symptomatic.IssueDate,
                Lungs = symptomatic.Lungs,
                Liver = symptomatic.Liver,
                Heart = symptomatic.Heart,
                SymptomName = symptomatic.SymptomName,
                SymptomaticDiseases = symptomatic.SymptomaticDiseases,
                DoctorId = symptomatic.DoctorId,
                ReceiptId = model.ReceiptId
            });
            _receiptStorage.Update(new ReceiptBindingModel
            {
                Id = receipt.Id,
                Dose = receipt.Dose,
                PerDose = receipt.PerDose,
                SymptomaticsId = model.SymptomaticId,
                MedicineId = receipt.MedicineId
            });
        }

        public void LinkingMedicine(MedicineLinkingBindingModel model)
        {
            var medicine = _medicineStorage.GetElement(new MedicineBindingModel
            {
                Id = model.MedicineId
            });

            var receipt = _receiptStorage.GetElement(new ReceiptBindingModel
            {
                Id = model.ReceiptId
            });

            if (medicine == null)
            {
                throw new Exception("Не найдено лекарство");
            }

            if (receipt == null)
            {
                throw new Exception("Не найдено выписка");
            }

            _receiptStorage.Update(new ReceiptBindingModel
            {
                Id = receipt.Id,
                Dose = receipt.Dose,
                PerDose = receipt.PerDose,
                MedicineId = model.MedicineId,
                SymptomaticsId = receipt.SymptomaticsId
            });
        }

    }
}