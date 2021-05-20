using System.Windows;
using Unity;

namespace ClinicViewDoctor
{
    /// <summary>
    /// Логика взаимодействия для WindowInitial.xaml
    /// </summary>
    public partial class WindowInitial : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public WindowInitial()
        {
            InitializeComponent();
        }

        private void buttonRegister_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<WindowRegistration>();
            window.ShowDialog();
        }

        private void buttonAuthorization_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<WindowAuthorization>();
            window.ShowDialog();
        }
    }
}