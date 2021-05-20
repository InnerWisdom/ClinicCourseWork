using ClinicBusinessLogic.BindingModels;
using ClinicBusinessLogic.BusinessLogics;
using ClinicBusinessLogic.HelperModels;
using ClinicBusinessLogic.Interfaces;
using Microsoft.Reporting.WinForms;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Windows;
using Unity;

namespace ClinicViewDoctor
{
    /// <summary>
    /// Логика взаимодействия для WindowReportDiseases.xaml
    /// </summary>
    public partial class WindowReportDiseases : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        private readonly ReportLogicDoctor logic;

        private readonly IDoctorStorage _doctorStorage;

        public int Id { set { id = value; } }

        private int? id;

        public WindowReportDiseases(ReportLogicDoctor logic, IDoctorStorage doctorStorage)
        {
            InitializeComponent();
            this.logic = logic;
            _doctorStorage = doctorStorage;
        }

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            reportViewer.LocalReport.ReportPath = "../../ReportDiseases.rdlc";
        }

        private void buttonMake_Click(object sender, RoutedEventArgs e)
        {
            if (datePickerFrom.SelectedDate >= datePickerTo.SelectedDate)
            {
                MessageBox.Show("Неверное выставление даты начала", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                ReportParameter parameter = new ReportParameter("ReportParameterPeriod",
                "c " + datePickerFrom.SelectedDate.Value.ToShortDateString() +
                " по " + datePickerTo.SelectedDate.Value.ToShortDateString());
                reportViewer.LocalReport.SetParameters(parameter);
                var dataSource = logic.GetDiseases(new ReportBindingModel
                {
                    DateFrom = datePickerFrom.SelectedDate,
                    DateTo = datePickerTo.SelectedDate,
                    DoctorId=id
                });
                ReportDataSource source = new ReportDataSource("DataSetDiseases", dataSource);
                reportViewer.LocalReport.DataSources.Add(source);
                reportViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonToPdf_Click(object sender, RoutedEventArgs e)
        {
            if (datePickerFrom.SelectedDate >= datePickerTo.SelectedDate)
            {
                MessageBox.Show("Неверное выставление даты начала", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {

                logic.SaveDiseasesToPdfFile(new ReportBindingModel
                {
                    FileName = "D:\\Otchet.pdf",
                    DateFrom = datePickerFrom.SelectedDate,
                    DateTo = datePickerTo.SelectedDate,
                    DoctorId=id
                });
                MailLogic.MailSendAsync(new MailSendInfo
                {
                    MailAddress = _doctorStorage.GetElement(new DoctorBindingModel { Id = id })?.EMail,
                    Subject = $"Отчет",
                    Text = "Отчет по заболеваниям за период c " + datePickerFrom.SelectedDate.Value.ToShortDateString() +
                        " по " + datePickerTo.SelectedDate.Value.ToShortDateString(),
                    File = "D:\\Otchet.pdf"
                });
                MessageBox.Show("Сообщение отправлено", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}