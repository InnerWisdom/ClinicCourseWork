using System.ComponentModel;

namespace ClinicBusinessLogic.ViewModels
{
    public class DoctorViewModel
    {
        public int Id { get; set; }

        [DisplayName("Фамилия")]
        public string F_Name { get; set; }

        [DisplayName("Имя")]
        public string L_Name { get; set; }

        [DisplayName("Пароль")]
        public string Password { get; set; }

        [DisplayName("Логин")]
        public string Login { get; set; }

        [DisplayName("Почта")]
        public string EMail { get; set; }
    }
}