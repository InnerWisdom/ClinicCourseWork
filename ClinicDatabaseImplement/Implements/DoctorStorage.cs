using ClinicBusinessLogic.BindingModels;
using ClinicBusinessLogic.Interfaces;
using ClinicBusinessLogic.ViewModels;
using ClinicDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClinicDatabaseImplement.Implements
{
    public class DoctorStorage : IDoctorStorage
    {
        public List<DoctorViewModel> GetFullList()
        {
            using (var context = new ClinicDataBase())
            {
                return context.Doctors
                .Select(rec => new DoctorViewModel
                {
                    Id = rec.Id,
                    F_Name = rec.F_Name,
                    L_Name = rec.L_Name,
                    Login = rec.Login,
                    Password = rec.Password,
                    EMail = rec.EMail
                })
                .ToList();
            }
        }

        public List<DoctorViewModel> GetFilteredList(DoctorBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new ClinicDataBase())
            {
                return context.Doctors
                .Where(rec => rec.Login == model.Login && rec.Password == model.Password)
                .Select(rec => new DoctorViewModel
                {
                    Id = rec.Id,
                    F_Name = rec.F_Name,
                    L_Name = rec.L_Name,
                    Login = rec.Login,
                    Password = rec.Password,
                    EMail = rec.EMail
                })
                .ToList();
            }
        }

        public DoctorViewModel GetElement(DoctorBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new ClinicDataBase())
            {
                var doctor = context.Doctors
                .FirstOrDefault(rec => rec.Login == model.Login || rec.Id == model.Id);
                return doctor != null ?
                new DoctorViewModel
                {
                    Id = doctor.Id,
                    F_Name = doctor.F_Name,
                    L_Name = doctor.L_Name,
                    Login = doctor.Login,
                    Password = doctor.Password,
                    EMail = doctor.EMail
                } :
                null;
            }
        }

        public void Insert(DoctorBindingModel model)
        {
            using (var context = new ClinicDataBase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {

                    try
                    {
                        context.Doctors.Add(CreateModel(model, new Doctor()));
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

        public void Update(DoctorBindingModel model)
        {
            using (var context = new ClinicDataBase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {

                    try
                    {
                        var element = context.Doctors.FirstOrDefault(rec => rec.Id == model.Id);
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

        public void Delete(DoctorBindingModel model)
        {
            using (var context = new ClinicDataBase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {

                    try
                    {
                        Doctor element = context.Doctors.FirstOrDefault(rec => rec.Id == model.Id);
                        if (element != null)
                        {
                            context.Doctors.Remove(element);
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

        private Doctor CreateModel(DoctorBindingModel model, Doctor doctor)
        {
            doctor.F_Name = model.F_Name;
            doctor.L_Name = model.L_Name;
            doctor.Login = model.Login;
            doctor.Password = model.Password;
            doctor.EMail = model.EMail;
            return doctor;
        }
    }
}