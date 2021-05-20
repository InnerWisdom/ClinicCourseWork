using ClinicBusinessLogic.BindingModels;
using ClinicBusinessLogic.BusinessLogics;
using ClinicBusinessLogic.ViewModels;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Unity;

namespace ClinicViewDoctor
{
    /// <summary>
    /// Interaction logic for WindowSimulateForms.xaml
    /// </summary>
    public partial class WindowSimulateForms : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        private readonly ReceiptLogic _rlogic;

        private readonly MedicineLogic _mlogic;
        public WindowSimulateForms(ReceiptLogic _rlogic, MedicineLogic _mlogic)
        {
            InitializeComponent();
            this._rlogic = _rlogic;
            this._mlogic = _mlogic;
        }
        private void WindowSimulateForms_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                var listMedicine = _mlogic.Read(null);
                if (listMedicine != null)
                {
                    dataGridMedicine.ItemsSource = listMedicine;
                }
                var listReceipt = _rlogic.Read(null);
                if (listReceipt != null)
                {
                    dataGridReceipt.ItemsSource = listReceipt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Generate_Click(object sender, RoutedEventArgs e)
        {
            _rlogic.Generate();
            _mlogic.Generate();
            LoadData();
        }
    }
}
