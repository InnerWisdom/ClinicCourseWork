using ClinicBusinessLogic.BindingModels;
using ClinicBusinessLogic.ViewModels;
using System.Collections.Generic;

namespace ClinicBusinessLogic.Interfaces
{
    public interface IReceiptStorage
    {

        List<ReceiptViewModel> GetFullList();

        List<ReceiptViewModel> GetFilteredList(ReceiptBindingModel model);

        ReceiptViewModel GetElement(ReceiptBindingModel model);

        void Generate();

        void Insert(ReceiptBindingModel model);

        void Update(ReceiptBindingModel model);

        void Delete(ReceiptBindingModel model);

    }
}
