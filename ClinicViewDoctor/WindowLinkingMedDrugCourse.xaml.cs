using ClinicBusinessLogic.BindingModels;
using ClinicBusinessLogic.BusinessLogics;
using ClinicBusinessLogic.ViewModels;
using System;
using System.Windows;
using Unity;

namespace ClinicViewDoctor
{
    /// <summary>
    /// Interaction logic for WindowLinkingMedDrugCourse.xaml
    /// </summary>
    public partial class WindowLinkingMedDrugCourse : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public int CourseId
        {
            get { return (int)(ComboBoxCourse.SelectedItem as DrugCourseViewModel).Id; }
            set { ComboBoxCourse.SelectedItem = SetValueCourse(value); }
        }

        public int MedicineId
        {
            get { return (ComboBoxMed.SelectedItem as MedicineViewModel).Id; }
            set { ComboBoxMed.SelectedItem = SetValueMedicine(value); }
        }

        public int DoctorId { set { doctorId = value; } }

        private int? doctorId;

        private readonly DrugCourseLogic logicCourse;

        private readonly MedicineLogic logicMedicine;

        public WindowLinkingMedDrugCourse(DrugCourseLogic logicCourse, MedicineLogic logicMedicine)
        {
            InitializeComponent();
            this.logicMedicine = logicMedicine;
            this.logicCourse = logicCourse;
        }

        private void WindowLinkingMedDrugCourse_Loaded(object sender, RoutedEventArgs e)
        {
            var listCourse = logicCourse.Read(new DrugCourseBindingModel { DoctorId= doctorId });
            if (listCourse != null)
            {
                ComboBoxCourse.ItemsSource = listCourse;
            }
            var listMedicine = logicMedicine.Read(null);
            if (listMedicine != null)
            {
                ComboBoxMed.ItemsSource = listMedicine;
            }
        }

        private void buttonLinking_Click(object sender, RoutedEventArgs e)
        {

            if (ComboBoxCourse.SelectedValue == null)
            {
                MessageBox.Show("Выберите курс", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (ComboBoxMed.SelectedValue == null)
            {
                MessageBox.Show("Выберите мед", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                logicCourse.Linking(new DrugCourseLinkingBindingModel
                {
                    DrugCourseId = (ComboBoxCourse.SelectedItem as DrugCourseViewModel).Id,
                    MedicineId = (ComboBoxMed.SelectedItem as MedicineViewModel).Id
                });
                MessageBox.Show("Привязка прошла успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
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

        private DrugCourseViewModel SetValueCourse(int value)
        {
            foreach (var item in ComboBoxCourse.Items)
            {
                if ((item as DrugCourseViewModel).Id == value)
                {
                    return item as DrugCourseViewModel;
                }
            }
            return null;
        }

        private MedicineViewModel SetValueMedicine(int value)
        {
            foreach (var item in ComboBoxMed.Items)
            {
                if ((item as MedicineViewModel).Id == value)
                {
                    return item as MedicineViewModel;
                }
            }
            return null;
        }
    }
}
