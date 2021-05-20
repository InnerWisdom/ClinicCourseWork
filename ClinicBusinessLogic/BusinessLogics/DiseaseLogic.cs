using ClinicBusinessLogic.BindingModels;
using ClinicBusinessLogic.Interfaces;
using ClinicBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;

namespace ClinicBusinessLogic.BusinessLogics
{
    public class DiseaseLogic
    {
        private readonly IDiseaseStorage _diseaseStorage;

        public DiseaseLogic(IDiseaseStorage diseaseStorage)
        {
            _diseaseStorage = diseaseStorage;
        }

        public List<DiseaseViewModel> Read(DiseaseBindingModel model)
        {
            if (model == null)
            {
                return _diseaseStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<DiseaseViewModel> { _diseaseStorage.GetElement(model) };
            }
            return _diseaseStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(DiseaseBindingModel model)
        {
            var element = _diseaseStorage.GetElement(new DiseaseBindingModel { DiseaseName = model.DiseaseName });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть болезнь с таким названием");
            }
            if (model.Id.HasValue)
            {
                _diseaseStorage.Update(model);
            }
            else
            {
                _diseaseStorage.Insert(model);
            }
        }

        public void Delete(DiseaseBindingModel model)
        {
            var element = _diseaseStorage.GetElement(new DiseaseBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Косметика не найдена");
            }
            _diseaseStorage.Delete(model);
        }
    }
}