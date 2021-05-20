using ClinicBusinessLogic.BindingModels;
using ClinicBusinessLogic.BusinessLogics;
using ClinicBusinessLogic.ViewModels;
using System;
using System.Windows;
using Unity;

namespace ClinicViewDoctor
{
    /// <summary>
    /// Логика взаимодействия для WindowSelectionDiseases.xaml
    /// </summary>
    public partial class WindowSelectionDiseases : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public int Id { get { return (ComboBoxDiseaseName.SelectedItem as DiseaseViewModel).Id; } set { ComboBoxDiseaseName.SelectedItem = SetValue(value); ; } }

        public string DiseaseName { get { return (ComboBoxDiseaseName.SelectedItem as DiseaseViewModel).DiseaseName; } }

        public int Mod { get { return Convert.ToInt32(TextBoxMod.Text); } set { TextBoxMod.Text = value.ToString(); } }

        public int DoctorId { set { doctorId = value; } }

        private int? doctorId;

        private readonly DiseaseLogic logic;

        public WindowSelectionDiseases(DiseaseLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void WindowSelectionDiseases_Loaded(object sender, RoutedEventArgs e)
        {
            var list = logic.Read(new DiseaseBindingModel { DoctorId = doctorId });
            if (list != null)
            {
                ComboBoxDiseaseName.ItemsSource = list;
            }
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxMod.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (ComboBoxDiseaseName.SelectedValue == null)
            {
                MessageBox.Show("Выберите заболевание", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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