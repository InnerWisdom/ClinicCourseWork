using ClinicBusinessLogic.BindingModels;
using ClinicBusinessLogic.Interfaces;
using ClinicBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;

namespace ClinicBusinessLogic.BusinessLogics
{
    public class ReceiptLogic
    {
        private readonly IReceiptStorage _receiptStorage;

        public ReceiptLogic(IReceiptStorage receiptStorage)
        {
            _receiptStorage = receiptStorage;
        }
        public void Generate() { _receiptStorage.Generate(); }
        public List<ReceiptViewModel> Read(ReceiptBindingModel model)
        {
            if (model == null)
            {
                return _receiptStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<ReceiptViewModel> { _receiptStorage.GetElement(model) };
            }
            return _receiptStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(ReceiptBindingModel model)
        {
            var element = _receiptStorage.GetElement(new ReceiptBindingModel { Id = model.Id });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть выписка с таким названием");
            }
            if (model.Id.HasValue)
            {
                _receiptStorage.Update(model);
            }
            else
            {
                _receiptStorage.Insert(model);
            }
        }

        public void Delete(ReceiptBindingModel model)
        {
            var element = _receiptStorage.GetElement(new ReceiptBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Лекарство не найдено");
            }
            _receiptStorage.Delete(model);
        }

    }
}
