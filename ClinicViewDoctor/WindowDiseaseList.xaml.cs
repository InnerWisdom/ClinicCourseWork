using ClinicBusinessLogic.BindingModels;
using ClinicBusinessLogic.BusinessLogics;
using ClinicBusinessLogic.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Unity;

namespace ClinicViewDoctor
{
    /// <summary>
    /// Interaction logic for WindowDiseaseList.xaml
    /// </summary>
    public partial class WindowDiseaseList : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        private readonly ReportLogicDoctor report;

        public int Id { set { id = value; } }

        private int? id;

        private Dictionary<int, string> receiptDiseases;

        public WindowDiseaseList(ReportLogicDoctor logicDoctor)
        {
            InitializeComponent();
            report = logicDoctor;
        }

        private void LoadData()
        {
            try
            {
                if (receiptDiseases != null)
                {
                    dataGridDisease.Columns.Clear();
                    var list = new List<DataGridReceiptListItemViewModel>();
                    foreach (var pc in receiptDiseases)
                    {
                        list.Add(new DataGridReceiptListItemViewModel()
                        {
                            Id = pc.Key,
                            DiseaseName = pc.Value
                        });
                    }
                    dataGridDisease.ItemsSource = list;
                    dataGridDisease.Columns[0].Visibility = Visibility.Hidden;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void WindowDiseaseList_Loaded(object sender, RoutedEventArgs e)
        {
            receiptDiseases = new Dictionary<int, string>();
        }

        private void buttonWord_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.Filter = "docx|*.docx";
            if ((bool)dialog.ShowDialog())
            {
                try
                {
                    report.SaveReceiptListToWordFile(new ReportBindingModel { FileName = dialog.FileName, receiptDiseases = new List<string>(receiptDiseases.Values), DoctorId = id });
                    MessageBox.Show("Выполнено", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void buttonExcel_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.Filter = "xlsx|*.xlsx";
            if ((bool)dialog.ShowDialog())
            {
                try
                {
                    report.SaveReceiptListToExcelFile(new ReportBindingModel { FileName = dialog.FileName, receiptDiseases = new List<string>(receiptDiseases.Values), DoctorId = id });
                    MessageBox.Show("Выполнено", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<WindowSelectionDiseasesForReceiptList>();
            window.DoctorId = (int)id;
            if (window.ShowDialog().Value)
            {
                if (receiptDiseases.ContainsKey(window.Id))
                {
                    receiptDiseases[window.Id] = window.DiseaseName;
                }
                else
                {
                    receiptDiseases.Add(window.Id, window.DiseaseName);
                }
                LoadData();
            }
        }

        private void buttonUpd_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridDisease.SelectedCells.Count != 0)
            {
                var window = Container.Resolve<WindowSelectionDiseasesForReceiptList>();
                window.Id = ((DataGridReceiptListItemViewModel)dataGridDisease.SelectedCells[0].Item).Id;
                window.DoctorId = (int)id;
                if (window.ShowDialog().Value)
                {
                    receiptDiseases[window.Id] = window.DiseaseName;
                    LoadData();
                }
            }
        }

        private void buttonDel_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridDisease.SelectedCells.Count != 0)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        receiptDiseases.Remove(((DataGridReceiptListItemViewModel)dataGridDisease.SelectedCells[0].Item).Id);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    LoadData();
                }
            }
        }

        private void buttonRef_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        /// <summary>
        /// Данные для привязки DisplayName к названиям столбцов
        /// </summary>
        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string displayName = GetPropertyDisplayName(e.PropertyDescriptor);
            if (!string.IsNullOrEmpty(displayName))
            {
                e.Column.Header = displayName;
            }
        }

        /// <summary>
        /// метод привязки DisplayName к названию столбца
        /// </summary>
        public static string GetPropertyDisplayName(object descriptor)
        {
            PropertyDescriptor pd = descriptor as PropertyDescriptor;
            if (pd != null)
            {
                // Check for DisplayName attribute and set the column header accordingly
                DisplayNameAttribute displayName = pd.Attributes[typeof(DisplayNameAttribute)] as DisplayNameAttribute;
                if (displayName != null && displayName != DisplayNameAttribute.Default)
                {
                    return displayName.DisplayName;
                }
            }
            else
            {
                PropertyInfo pi = descriptor as PropertyInfo;
                if (pi != null)
                {
                    // Check for DisplayName attribute and set the column header accordingly
                    Object[] attributes = pi.GetCustomAttributes(typeof(DisplayNameAttribute), true);
                    for (int i = 0; i < attributes.Length; ++i)
                    {
                        DisplayNameAttribute displayName = attributes[i] as DisplayNameAttribute;
                        if (displayName != null && displayName != DisplayNameAttribute.Default)
                        {
                            return displayName.DisplayName;
                        }
                    }
                }
            }
            return null;
        }
    }
}
