using ClinicBusinessLogic.BindingModels;
using ClinicBusinessLogic.Interfaces;
using ClinicBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;

namespace ClinicBusinessLogic.BusinessLogics
{
    public class MedicineLogic
    {
        private readonly IMedicineStorage _medicineStorage;

        public MedicineLogic(IMedicineStorage medicineStorage)
        {
            _medicineStorage = medicineStorage;
        }
        public void Generate() { _medicineStorage.Generate(); }
        public List<MedicineViewModel> Read(MedicineBindingModel model)
        {
            if (model == null)
            {
                return _medicineStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<MedicineViewModel> { _medicineStorage.GetElement(model) };
            }
            return _medicineStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(MedicineBindingModel model)
        {
            var element = _medicineStorage.GetElement(new MedicineBindingModel { Name = model.Name });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть лекарство с таким названием");
            }
            if (model.Id.HasValue)
            {
                _medicineStorage.Update(model);
            }
            else
            {
                _medicineStorage.Insert(model);
            }
        }

        public void Delete(MedicineBindingModel model)
        {
            var element = _medicineStorage.GetElement(new MedicineBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Лекарство не найдено");
            }
            _medicineStorage.Delete(model);
        }
    }
}