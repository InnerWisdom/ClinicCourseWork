using ClinicBusinessLogic.BindingModels;
using ClinicBusinessLogic.BusinessLogics;
using ClinicBusinessLogic.ViewModels;
using System;
using System.Windows;
using Unity;

namespace ClinicViewDoctor
{
    /// <summary>
    /// Interaction logic for WindowLinkingMed.xaml
    /// </summary>
    public partial class WindowLinkingMed : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public int ReceiptId
        {
            get { return (int)(ComboBoxReceipt.SelectedItem as ReceiptViewModel).Id; }
            set { ComboBoxReceipt.SelectedItem = SetValueReceipt(value); }
        }

        public int MedicineId
        {
            get { return (ComboBoxMed.SelectedItem as MedicineViewModel).Id; }
            set { ComboBoxMed.SelectedItem = SetValueMedicine(value); }
        }

        public int DoctorId { set { doctorId = value; } }

        private int? doctorId;

        private readonly ReceiptLogic logicReceipt;

        private readonly MedicineLogic logicMedicine;

        private readonly SymptomaticLogic logicSymptomatic;

        public WindowLinkingMed(ReceiptLogic logicReceipt, MedicineLogic logicMedicine, SymptomaticLogic logicSymptomatic)
        {
            InitializeComponent();
            this.logicSymptomatic = logicSymptomatic;
            this.logicMedicine = logicMedicine;
            this.logicReceipt = logicReceipt;
        }

        private void WindowLinkingMed_Loaded(object sender, RoutedEventArgs e)
        {
            var listReciept = logicReceipt.Read(null);
            if (listReciept != null)
            {
                ComboBoxReceipt.ItemsSource = listReciept;
            }
            var listMedicine = logicMedicine.Read(null);
            if (listMedicine != null)
            {
                ComboBoxMed.ItemsSource = listMedicine;
            }
        }

        private void buttonLinking_Click(object sender, RoutedEventArgs e)
        {

            if (ComboBoxReceipt.SelectedValue == null)
            {
                MessageBox.Show("Выберите выписку", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (ComboBoxMed.SelectedValue == null)
            {
                MessageBox.Show("Выберите мед", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                logicSymptomatic.LinkingMedicine(new MedicineLinkingBindingModel
                {
                    ReceiptId = (int)(ComboBoxReceipt.SelectedItem as ReceiptViewModel).Id,
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

        private ReceiptViewModel SetValueReceipt(int value)
        {
            foreach (var item in ComboBoxReceipt.Items)
            {
                if ((item as ReceiptViewModel).Id == value)
                {
                    return item as ReceiptViewModel;
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
