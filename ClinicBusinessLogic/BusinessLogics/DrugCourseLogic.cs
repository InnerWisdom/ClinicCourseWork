using ClinicBusinessLogic.BindingModels;
using ClinicBusinessLogic.Interfaces;
using ClinicBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;

namespace ClinicBusinessLogic.BusinessLogics
{
    public class DrugCourseLogic
    {
        private readonly IDrugCourseStorage _drugCourseStorage;
        private readonly IMedicineStorage _medicineStorage;

        public DrugCourseLogic(IDrugCourseStorage drugCourseStorage, IMedicineStorage medicineStorage)
        {
            _drugCourseStorage = drugCourseStorage;
            _medicineStorage = medicineStorage;
        }

        public List<DrugCourseViewModel> Read(DrugCourseBindingModel model)
        {
            if (model == null)
            {
                return _drugCourseStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<DrugCourseViewModel> { _drugCourseStorage.GetElement(model) };
            }
            return _drugCourseStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(DrugCourseBindingModel model)
        {
            var element = _drugCourseStorage.GetElement(new DrugCourseBindingModel { FormedDate = model.FormedDate });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Для данного времени уже существует запись");
            }
            if (model.Id.HasValue)
            {
                _drugCourseStorage.Update(model);
            }
            else
            {
                _drugCourseStorage.Insert(model);
            }
        }

        public void Delete(DrugCourseBindingModel model)
        {
            var element = _drugCourseStorage.GetElement(new DrugCourseBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Запись не найдена");
            }
            _drugCourseStorage.Delete(model);
        }

        public void Linking(DrugCourseLinkingBindingModel model)
        {
            var drugCourse = _drugCourseStorage.GetElement(new DrugCourseBindingModel
            {
                Id = model.DrugCourseId
            });

            var medicine = _medicineStorage.GetElement(new MedicineBindingModel
            {
                Id = model.MedicineId
            });

            if (drugCourse == null)
            {
                throw new Exception("Не найдена выдача");
            }

            if (medicine == null)
            {
                throw new Exception("Не найдено выписка");
            }

            if (drugCourse.MedicineId.HasValue)
            {
                throw new Exception("Данное лекарство уже привязано к симптоматике");
            }

            _drugCourseStorage.Update(new DrugCourseBindingModel
            {
                Id = drugCourse.Id,
                Length = drugCourse.Length,
                FormedDate = drugCourse.FormedDate,
                DrugCourseDiseases = drugCourse.DrugCourseDiseases,
                DoctorId = drugCourse.DoctorId,
                MedicineId = model.MedicineId
            });

            _medicineStorage.Update(new MedicineBindingModel
            {
                Id = medicine.Id,
                Class = medicine.Class,
                Name = medicine.Name,
                DrugCourseId = model.DrugCourseId
            });
        }

    }
}