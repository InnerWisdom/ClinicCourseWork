using ClinicBusinessLogic.BindingModels;
using ClinicBusinessLogic.Interfaces;
using ClinicBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;

namespace ClinicBusinessLogic.BusinessLogics
{
    public class DoctorLogic
    {
        private readonly IDoctorStorage _doctorStorage;

        public DoctorLogic(IDoctorStorage doctorStorage)
        {
            _doctorStorage = doctorStorage;
        }

        public List<DoctorViewModel> Read(DoctorBindingModel model)
        {
            if (model == null)
            {
                return _doctorStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<DoctorViewModel> { _doctorStorage.GetElement(model) };
            }
            return _doctorStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(DoctorBindingModel model)
        {
            var element = _doctorStorage.GetElement(new DoctorBindingModel { Login = model.Login, EMail = model.EMail});
            if (element != null && element.Id != model.Id)
            {
                throw new Exception(" Доктор с такими данными уже существует");
            }
            if (model.Id.HasValue)
            {
                _doctorStorage.Update(model);
            }
            else
            {
                _doctorStorage.Insert(model);
            }
        }

        public void Delete(DoctorBindingModel model)
        {
            var element = _doctorStorage.GetElement(new DoctorBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Пользователь не найден");
            }
            _doctorStorage.Delete(model);
        }
    }
}