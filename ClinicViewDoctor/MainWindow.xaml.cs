using ClinicBusinessLogic.BindingModels;
using ClinicBusinessLogic.BusinessLogics;

using System.IO;
using System.Windows;
using Microsoft.Win32;
using Unity;

namespace ClinicViewDoctor
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private int? id;

        private DoctorLogic logic;

        private MedicineLogic mLogic;

        private ReceiptLogic rLogic;

        private ReportLogicDoctor rrLogic;

        public MainWindow(DoctorLogic logic, MedicineLogic mLogic, ReceiptLogic rLogic, ReportLogicDoctor rrLogic)
        {
            InitializeComponent();
            this.logic = logic;
            this.mLogic = mLogic;
            this.rLogic = rLogic;
            this.rrLogic = rrLogic;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            var doctor = logic.Read(new DoctorBindingModel { Id = id })?[0];
            lbl_Doctor.Content = "Доктор: " + doctor.F_Name + " " + doctor.L_Name;
        }

        private void DiseaseMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<WindowDiseases>();
            window.Id = (int)id;
            window.ShowDialog();
        }

        private void SymptomaticsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<WindowDrugCourses>();
            window.Id = (int)id;
            window.ShowDialog();
        }

        private void SymptomsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<WindowSymptoms>();
            window.Id = (int)id;
            window.ShowDialog();
        }

        private void ReportMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<WindowReportDiseases>();
            window.Id = (int)id;
            window.ShowDialog();
        }

        private void DiseaseListMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<WindowDiseaseList>();
            window.Id = (int)id;
            window.ShowDialog();
        }

        private void Generate_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<WindowSimulateForms>();
            window.ShowDialog();
        }

        private void StatisticsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<WindowStatistic>();
            window.Id = (int)id;
            window.Show();
        }
    }
}