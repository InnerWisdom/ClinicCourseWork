using ClinicBusinessLogic.BindingModels;
using ClinicBusinessLogic.HelperModels;
using ClinicBusinessLogic.Interfaces;
using ClinicBusinessLogic.ViewModels;
using System.Collections.Generic;
using System;

namespace ClinicBusinessLogic.BusinessLogics
{
    public class ReportLogicDoctor
    {
        private readonly IDrugCourseStorage _drugCourseStorage;

        private readonly ISymptomaticStorage _symptomaticStorage;

        private readonly IDoctorStorage _doctorStorage;

        private readonly IReceiptStorage _receiptStorage;

        private readonly IMedicineStorage _medicineStorage;

        public ReportLogicDoctor(IReceiptStorage receiptStorage, IDrugCourseStorage drugCourseStorage, ISymptomaticStorage symptomaticStorage, IDoctorStorage doctorStorage, IMedicineStorage medicineStorage)
        {
            _medicineStorage = medicineStorage;
            _receiptStorage = receiptStorage;
            _drugCourseStorage = drugCourseStorage;
            _symptomaticStorage = symptomaticStorage;
            _doctorStorage = doctorStorage;
        }

        public List<ReportDiseasesViewModel> GetDiseases(ReportBindingModel model)
        {
            var drugCourseList = _drugCourseStorage.GetFilteredList(new DrugCourseBindingModel { DateFrom = model.DateFrom, DateTo = model.DateTo, Id = model.DoctorId });
            var listAll = new List<ReportDiseasesViewModel>();
            foreach (var drugCourse in drugCourseList)
            {
                foreach (var dc in drugCourse.DrugCourseDiseases)
                {
                    var doctor = _doctorStorage.GetElement(new DoctorBindingModel { Id = drugCourse.DoctorId });
                    listAll.Add(new ReportDiseasesViewModel
                    {
                        Type = "Выписка о приёме препарата " + _medicineStorage.GetElement(new MedicineBindingModel { Id = drugCourse.MedicineId }).Name,
                        DiseaseName = dc.Value.Item1,
                        DateFormed = drugCourse.FormedDate,
                        Length = dc.Value.Item2,
                        DoctorName = doctor.F_Name + " " + doctor.L_Name
                    });
                }
            }
            var listSymptomatics = _symptomaticStorage.GetFilteredList(new SymptomaticsBindingModel { DateFrom = model.DateFrom, DateTo = model.DateTo });
            foreach (var sympt in listSymptomatics)
            {
                foreach (var dc in sympt.SymptomaticDiseases)
                {
                    var doctor = _doctorStorage.GetElement(new DoctorBindingModel { Id = sympt.DoctorId });
                    listAll.Add(new ReportDiseasesViewModel
                    {
                        Type = "Выписка о симптоме " + sympt.SymptomName,
                        DiseaseName = dc.Value.Item1,
                        DateFormed = sympt.IssueDate,
                        Length = dc.Value.Item2,
                        DoctorName = doctor.F_Name + " " + doctor.L_Name
                    });
                }
            }
            return listAll;
        }

        public List<ReportReceiptDiseaseViewModel> GetReceiptList(ReportBindingModel model)
        {
            var listSymptomatics = _symptomaticStorage.GetFullList();
            var list = new List<ReportReceiptDiseaseViewModel>();
            foreach (var diseaseName in model.receiptDiseases)
            {
                foreach (var sympt in listSymptomatics)
                {
                    foreach (var rc in sympt.SymptomaticDiseases)
                    {
                        if (rc.Value.Item1 == diseaseName)
                        {
                            var listReceipts = _receiptStorage.GetFilteredList(new ReceiptBindingModel { SymptomaticsId = sympt.Id });
                        if (listReceipts.Count > 0 && listReceipts != null)
                        {
                            foreach (var receipt in listReceipts)
                            {
                                list.Add(new ReportReceiptDiseaseViewModel
                                {
                                    DiseaseName = rc.Value.Item1,
                                    Dose = receipt.Dose,
                                    PerDose = receipt.PerDose,
                                    DoctorId = sympt.DoctorId,
                                    MedicineName = _medicineStorage.GetElement(new MedicineBindingModel { Id = receipt.MedicineId }).Name
                                });
                            }
                        }
                        }
                    }
                }
            }
                

            return list;

        }

        public void SaveReceiptListToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDoc(new WordInfoDoctor
            {
                FileName = model.FileName,
                Title = "Сведения по рецептам",
                Receipts = GetReceiptList(model)
            });
        }

        public void SaveReceiptListToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateDoc(new ExcelInfoDoctor
            {
                FileName = model.FileName,
                Title = "Сведения по рецептам",
                Receipts = GetReceiptList(model)
            });
        }

        public void SaveDiseasesToPdfFile(ReportBindingModel model)
        {
            SaveToPdfDoctor.CreateDoc(new PdfInfoDoctor
            {
                FileName = model.FileName,
                Title = "Список заболеваний",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                Diseases = GetDiseases(model)
            });
        }
    }
}