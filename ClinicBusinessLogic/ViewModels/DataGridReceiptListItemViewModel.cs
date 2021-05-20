using System.ComponentModel;

namespace ClinicBusinessLogic.ViewModels
{
    public class DataGridReceiptListItemViewModel
    {
        public int Id { get; set; }

        [DisplayName("Болезнь")]
        public string DiseaseName { get; set; }
    }
}
