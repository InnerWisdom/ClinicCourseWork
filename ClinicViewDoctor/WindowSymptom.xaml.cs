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
    /// Логика взаимодействия для WindowSymptom.xaml
    /// </summary>
    public partial class WindowSymptom : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        public int DoctorId { set { doctorId = value; } }

        private readonly SymptomaticLogic logic;

        private int? id;


        private int? doctorId;

        private Dictionary<int, (string, int)> symptomDiseases;

        public WindowSymptom(SymptomaticLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void WindowSymptom_Loaded(object sender, RoutedEventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    SymptomaticViewModel view = logic.Read(new SymptomaticsBindingModel { Id = id.Value })?[0];
                    if (view != null)
                    {
                        TextBoxIssueDate.Text = view.IssueDate.ToString();
                        TextBoxLiver.Text = view.Liver;
                        TextBoxHeart.Text = view.Heart;
                        TextBoxLungs.Text = view.Lungs;
                        TextBoxName.Text = view.SymptomName;
                        symptomDiseases = view.SymptomaticDiseases;
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
                symptomDiseases = new Dictionary<int, (string, int)>();
            }
        }

        private void LoadData()
        {
            try
            {
                if (symptomDiseases != null)
                {
                    dataGrid.Columns.Clear();
                    var list = new List<DataGridSympotamicItemViewModel>();
                    foreach (var dc in symptomDiseases)
                    {
                        list.Add(new DataGridSympotamicItemViewModel()
                        {
                            Id = dc.Key,
                            DiseaseName = dc.Value.Item1,
                            Modifier = dc.Value.Item2
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
            TextBoxIssueDate.Text = (DateTime.Now).ToString();
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<WindowSelectionDiseases>();
            window.DoctorId = (int)doctorId;
            if (window.ShowDialog().Value)
            {
                if (symptomDiseases.ContainsKey(window.Id))
                {
                    symptomDiseases[window.Id] = (window.DiseaseName, window.Mod);
                }
                else
                {
                    symptomDiseases.Add(window.Id, (window.DiseaseName, window.Mod));
                }
                LoadData();
            }
        }

        private void buttonUpd_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedCells.Count != 0)
            {
                var window = Container.Resolve<WindowSelectionDiseases>();
                window.Id =((DataGridSympotamicItemViewModel)dataGrid.SelectedCells[0].Item).Id;
                window.Mod = ((DataGridSympotamicItemViewModel)dataGrid.SelectedCells[0].Item).Modifier;
                window.DoctorId = (int)doctorId;

                if (window.ShowDialog().Value)
                {
                    symptomDiseases[window.Id] = (window.DiseaseName, window.Mod);
                    LoadData();
                }
            }
        }

        private void buttonDel_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedCells.Count != 0)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    int id = ((SymptomaticViewModel)dataGrid.SelectedCells[0].Item).Id;
                    try
                    {
                        logic.Delete(new SymptomaticsBindingModel { Id = id });
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
            if (symptomDiseases == null || symptomDiseases.Count == 0)
            {
                MessageBox.Show("Заполните заболевания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(TextBoxLungs.Text))
            {
                MessageBox.Show("Заполните состояние легких", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(TextBoxLiver.Text))
            {
                MessageBox.Show("Заполните состояние печени", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(TextBoxHeart.Text))
            {
                MessageBox.Show("Заполните состояние сердца", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(TextBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                logic.CreateOrUpdate(new SymptomaticsBindingModel
                {
                    Id = id,
                    IssueDate = DateTime.Now,
                    Liver=TextBoxLiver.Text.ToString(),
                    Lungs= TextBoxLungs.Text.ToString(),
                    Heart = TextBoxHeart.Text.ToString(),
                    SymptomName = TextBoxName.Text.ToString(),
                    SymptomaticDiseases = symptomDiseases,
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

        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string displayName = GetPropertyDisplayName(e.PropertyDescriptor);
            if (!string.IsNullOrEmpty(displayName))
            {
                e.Column.Header = displayName;
            }
        }

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