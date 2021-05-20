using ClinicBusinessLogic.BindingModels;
using ClinicBusinessLogic.BusinessLogics;
using System;
using System.Windows;
using Unity;

namespace ClinicViewDoctor
{
    /// <summary>
    /// Логика взаимодействия для WindowDisease.xaml
    /// </summary>
    public partial class WindowDisease : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        public int DoctorId { set { doctorId = value; } }

        private readonly DiseaseLogic logic;

        private int? id;

        private int? doctorId;

        public WindowDisease(DiseaseLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void WindowDisease_Loaded(object sender, RoutedEventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var view = logic.Read(new DiseaseBindingModel { Id = id })?[0];
                    if (view != null)
                    {
                        TextBoxName.Text = view.DiseaseName;
                        TextBoxDifficulty.Text = view.Difficulty.ToString();
                        TextBoxClass.Text = view.Class;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(TextBoxDifficulty.Text))
            {
                MessageBox.Show("Заполните сложность", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(TextBoxClass.Text))
            {
                MessageBox.Show("Заполните класс", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                logic.CreateOrUpdate(new DiseaseBindingModel
                {
                    Id = id,
                    DiseaseName = TextBoxName.Text,
                    Difficulty = Convert.ToDecimal(TextBoxDifficulty.Text),
                    Class = TextBoxClass.Text,
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
    }
}