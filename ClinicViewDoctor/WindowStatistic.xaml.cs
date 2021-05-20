using ClinicBusinessLogic.BindingModels;
using ClinicBusinessLogic.BusinessLogics;
using System.Windows;
using System.Windows.Controls.DataVisualization.Charting;
using Unity;

namespace ClinicViewDoctor
{
    /// <summary>
    /// Interaction logic for WindowStatistic.xaml
    /// </summary>
    public partial class WindowStatistic : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        private readonly StatisticLogic logic;

        public int Id { set { id = value; } }

        private int? id;

        private DoctorLogic doctorLogic;

        public WindowStatistic(StatisticLogic logic, DoctorLogic logicE)
        {
            InitializeComponent();
            this.logic = logic;
            doctorLogic = logicE;
        }

        private void LoadData()
        {
            ((ColumnSeries)mcChart.Series[0]).ItemsSource = logic.GetSymptomaticStatistic(new ReportBindingModel
            {
                DateFrom = datePickerFrom.SelectedDate,
                DateTo = datePickerTo.SelectedDate,
                DoctorId = id
            });
            ((ColumnSeries)mcChart.Series[1]).ItemsSource = logic.GetDrugCourseStatistic(new ReportBindingModel
            {
                DateFrom = datePickerFrom.SelectedDate,
                DateTo = datePickerTo.SelectedDate,
                DoctorId = id
            });
            ((ColumnSeries)mcChartAll.Series[0]).ItemsSource = logic.GetSymptomaticStatistic(new ReportBindingModel
            {
                DateFrom = datePickerFrom.SelectedDate,
                DateTo = datePickerTo.SelectedDate,
                DoctorId = 0
            });
            ((ColumnSeries)mcChartAll.Series[1]).ItemsSource = logic.GetDrugCourseStatistic(new ReportBindingModel
            {
                DateFrom = datePickerFrom.SelectedDate,
                DateTo = datePickerTo.SelectedDate,
                DoctorId = 0
            });
        }

        private void buttonMake_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void WindowStatistic_Loaded(object sender, RoutedEventArgs e)
        {
            var doctor = doctorLogic.Read(new DoctorBindingModel { Id = id })?[0];
            lbl_Doctor.Content = "Заболевания, сформированные доктором: " + doctor.F_Name + " " + doctor.L_Name;
        }
    }
}
