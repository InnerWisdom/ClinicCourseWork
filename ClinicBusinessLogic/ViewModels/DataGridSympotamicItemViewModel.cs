using System.ComponentModel;

namespace ClinicBusinessLogic.ViewModels
{
    public class DataGridSympotamicItemViewModel
    {
        public int Id { get; set; }

        [DisplayName("Болезнь")]
        public string DiseaseName { get; set; }

        [DisplayName("Модификатор")]
        public int Modifier { get; set; }
    }
}
