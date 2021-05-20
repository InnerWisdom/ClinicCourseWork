using ClinicBusinessLogic.BindingModels;
using ClinicBusinessLogic.BusinessLogics;
using ClinicBusinessLogic.ViewModels;
using System.Windows;
using Unity;

namespace ClinicViewDoctor
{
    /// <summary>
    /// Interaction logic for WindowSelectionDiseasesForReceiptList.xaml
    /// </summary>
    public partial class WindowSelectionDiseasesForReceiptList : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public int Id { get { return (ComboBoxDiseaseName.SelectedItem as DiseaseViewModel).Id; } set { id = value; } }

        public string DiseaseName { get { return (ComboBoxDiseaseName.SelectedItem as DiseaseViewModel).DiseaseName; } }

        public int DoctorId { set { doctorId = value; } }

        private int? id;

        private int? doctorId;

        private readonly DiseaseLogic logic;

        public WindowSelectionDiseasesForReceiptList(DiseaseLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void WindowSelectionDiseasesForReceiptList_Loaded(object sender, RoutedEventArgs e)
        {
            var list = logic.Read(new DiseaseBindingModel { DoctorId = doctorId });
            if (list != null)
            {
                ComboBoxDiseaseName.ItemsSource = list;
            }
            if (id != null)
            {
                ComboBoxDiseaseName.SelectedItem = SetValue(id.Value);
            }
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBoxDiseaseName.SelectedValue == null)
            {
                MessageBox.Show("Выберите косметику", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            DialogResult = true;
            Close();
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private DiseaseViewModel SetValue(int value)
        {
            foreach (var item in ComboBoxDiseaseName.Items)
            {
                if ((item as DiseaseViewModel).Id == value)
                {
                    return item as DiseaseViewModel;
                }
            }
            return null;
        }
    }
}
