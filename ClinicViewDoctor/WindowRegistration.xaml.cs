using ClinicBusinessLogic.BindingModels;
using ClinicBusinessLogic.BusinessLogics;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using Unity;

namespace ClinicViewDoctor
{
    /// <summary>
    /// Логика взаимодействия для WindowRegistration.xaml
    /// </summary>
    public partial class WindowRegistration : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly DoctorLogic logic;

        private int? id;

        public WindowRegistration(DoctorLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void WindowRegistration_Loaded(object sender, RoutedEventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var view = logic.Read(new DoctorBindingModel { Id = id })?[0];
                    if (view != null)
                    {
                        TextBoxF_Name.Text = view.F_Name;
                        TextBoxL_Name.Text = view.L_Name;
                        TextBoxLogin.Text = view.Login;
                        TextBoxPassword.Text = view.Password;
                        TextBoxEmail.Text = view.EMail;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void buttonRegister_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxF_Name.Text))
            {
                MessageBox.Show("Заполните фамилию", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(TextBoxL_Name.Text))
            {
                MessageBox.Show("Заполните имя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(TextBoxLogin.Text))
            {
                MessageBox.Show("Заполните логин", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(TextBoxPassword.Text))
            {
                MessageBox.Show("Заполните пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(TextBoxEmail.Text))
            {
                MessageBox.Show("Заполните адрес электронной почты", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                if (!Regex.IsMatch(TextBoxEmail.Text, @"^[A-Za-z0-9]+(?:[._%+-])?[A-Za-z0-9._-]+[A-Za-z0-9]@[A-Za-z0-9]+(?:[.-])?[A-Za-z0-9._-]+\.[A-Za-z]{2,6}$"))
                {
                    throw new Exception("Почта указана некорректно");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            try
            {
                logic.CreateOrUpdate(new DoctorBindingModel
                {
                    Id = id,
                    F_Name = TextBoxF_Name.Text,
                    L_Name = TextBoxL_Name.Text,
                    Login = TextBoxLogin.Text,
                    Password = TextBoxPassword.Text,
                    EMail = TextBoxEmail.Text
                });
                MessageBox.Show("Решистрация прошла успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        private void simulate_work()
        {

        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}