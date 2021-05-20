using ClinicBusinessLogic.BindingModels;
using ClinicBusinessLogic.BusinessLogics;
using ClinicBusinessLogic.ViewModels;
using System;
using System.Windows;
using Unity;

namespace ClinicViewDoctor
{
    public partial class WindowLinkingSymptomatic : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public int ReceiptId
        {
            get { return (int)(ComboBoxReceipt.SelectedItem as ReceiptViewModel).Id; }
            set { ComboBoxReceipt.SelectedItem = SetValueReceipt(value); }
        }

        public int SymptomId
        {
            get { return (ComboBoxSymptomatic.SelectedItem as SymptomaticViewModel).Id; }
            set { ComboBoxSymptomatic.SelectedItem = SetValueSymptomatic(value); }
        }

        public int DoctorId { set { doctorId = value; } }

        private int? doctorId;

        private readonly ReceiptLogic logicReceipt;

        private readonly SymptomaticLogic logicSymptomatic;
        
        public WindowLinkingSymptomatic(ReceiptLogic logicReceipt, SymptomaticLogic logicSymptomatic)
        {
            InitializeComponent();
            this.logicSymptomatic = logicSymptomatic;
            this.logicReceipt = logicReceipt;
        }

        private void WindowLinkingSymptomatic_Loaded(object sender, RoutedEventArgs e)
        {
            var listReciept = logicReceipt.Read(null);
            if (listReciept != null)
            {
                ComboBoxReceipt.ItemsSource = listReciept;
            }
            var lisSymptomatic = logicSymptomatic.Read(new SymptomaticsBindingModel { DoctorId = doctorId });
            if (lisSymptomatic != null)
            {
                ComboBoxSymptomatic.ItemsSource = lisSymptomatic;
            }
        }

        private void buttonLinking_Click(object sender, RoutedEventArgs e)
        {

            if (ComboBoxReceipt.SelectedValue == null)
            {
                MessageBox.Show("Выберите выписку", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (ComboBoxSymptomatic.SelectedValue == null)
            {
                MessageBox.Show("Выберите симптоматику", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                logicSymptomatic.Linking(new SymptomaticLinkingBindingModel
                {
                    ReceiptId = (int)(ComboBoxReceipt.SelectedItem as ReceiptViewModel).Id,
                    SymptomaticId = (ComboBoxSymptomatic.SelectedItem as SymptomaticViewModel).Id
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

        private SymptomaticViewModel SetValueSymptomatic(int value)
        {
            foreach (var item in ComboBoxSymptomatic.Items)
            {
                if ((item as SymptomaticViewModel).Id == value)
                {
                    return item as SymptomaticViewModel;
                }
            }
            return null;
        }

    }
}
