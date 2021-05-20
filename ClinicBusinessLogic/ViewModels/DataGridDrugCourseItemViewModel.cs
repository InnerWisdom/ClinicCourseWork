using System.ComponentModel;

namespace ClinicBusinessLogic.ViewModels
{
    public class DataGridDrugCourseItemViewModel
    {
        public int Id { get; set; }

        [DisplayName("Болезни")]
        public string DiseaseName { get; set; }

        [DisplayName("Длина курса")]
        public decimal? Length { get; set; }

        [DisplayName("Модификатор")]
        public int Count { get; set; }
    }
}
