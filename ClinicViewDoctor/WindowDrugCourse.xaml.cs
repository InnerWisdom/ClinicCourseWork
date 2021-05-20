using ClinicBusinessLogic.BindingModels;
using ClinicBusinessLogic.BusinessLogics;
using ClinicBusinessLogic.ViewModels;
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
    /// Логика взаимодействия для WindowDrugCourse.xaml
    /// </summary>
    public partial class WindowDrugCourse : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        public int DoctorId { set { doctorId = value; } }

        private readonly DrugCourseLogic logicR;

        private readonly DiseaseLogic logicC;

        private int? id;

        private int? doctorId;

        private Dictionary<int, (string, int)> drugCourseDiseases;

        public WindowDrugCourse(DrugCourseLogic logicR, DiseaseLogic logicC)
        {
            InitializeComponent();
            this.logicR = logicR;
            this.logicC = logicC;
        }

        private void WindowDrugCourse_Loaded(object sender, RoutedEventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    DrugCourseViewModel view = logicR.Read(new DrugCourseBindingModel { Id = id.Value })?[0];
                    if (view != null)
                    {
                        TextBoxTotalLength.Text = view.Length.ToString();
                        TextBoxFormDate.Text = view.FormedDate.ToString();
                        drugCourseDiseases = view.DrugCourseDiseases;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                drugCourseDiseases = new Dictionary<int, (string, int)>();
            }
        }

        private void LoadData()
        {
            try
            {
                if (drugCourseDiseases != null)
                {
                    dataGrid.Columns.Clear();
                    var list = new List<DataGridDrugCourseItemViewModel>();
                    foreach (var rc in drugCourseDiseases)
                    {
                        list.Add(new DataGridDrugCourseItemViewModel()
                        {
                            Id = rc.Key,
                            DiseaseName = rc.Value.Item1,
                            Length = logicC.Read(new DiseaseBindingModel { Id = rc.Key })?[0].Difficulty,
                            Count = rc.Value.Item2
                        });
                    }
                    dataGrid.ItemsSource = list;
                    dataGrid.Columns[0].Visibility = Visibility.Hidden;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            TextBoxFormDate.Text = (DateTime.Now).ToString();
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<WindowSelectionDiseases>();
            window.DoctorId = (int)doctorId;
            if (window.ShowDialog().Value)
            {
                if (drugCourseDiseases.ContainsKey(window.Id))
                {
                    drugCourseDiseases[window.Id] = (window.DiseaseName, window.Mod);
                }
                else
                {
                    drugCourseDiseases.Add(window.Id, (window.DiseaseName, window.Mod));
                }
                LoadData();
                CalcTotalCost();
            }
        }

        private void CalcTotalCost()
        {
            try
            {
                int totalCost = 0;
                foreach (var rc in drugCourseDiseases)
                {
                    totalCost += rc.Value.Item2 * (int)logicC.Read(new DiseaseBindingModel { Id = rc.Key })?[0].Difficulty;
                }
                TextBoxTotalLength.Text = totalCost.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonUpd_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedCells.Count != 0)
            {
                var window = Container.Resolve<WindowSelectionDiseases>();
                window.Id = ((DataGridDrugCourseItemViewModel)dataGrid.SelectedCells[0].Item).Id;
                window.Mod = ((DataGridDrugCourseItemViewModel)dataGrid.SelectedCells[0].Item).Count;
                window.DoctorId = (int)doctorId;

                if (window.ShowDialog().Value)
                {
                    drugCourseDiseases[window.Id] = (window.DiseaseName, window.Mod);
                    LoadData();
                    CalcTotalCost();
                }
            }
        }

        private void buttonDel_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedCells.Count != 0)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    int id = ((DrugCourseViewModel)dataGrid.SelectedCells[0].Item).Id;
                    try
                    {
                        logicR.Delete(new DrugCourseBindingModel { Id = id });
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

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (drugCourseDiseases == null || drugCourseDiseases.Count == 0)
            {
                MessageBox.Show("Заполните косметику", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                logicR.CreateOrUpdate(new DrugCourseBindingModel
                {
                    Id = id,
                    FormedDate = DateTime.Now,
                    Length = Convert.ToInt32(TextBoxTotalLength.Text),
                    DrugCourseDiseases = drugCourseDiseases,
                    DoctorId = doctorId
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
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