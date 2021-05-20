using ClinicBusinessLogic.BusinessLogics;
using ClinicBusinessLogic.Interfaces;
using ClinicDatabaseImplement.Implements;
using System.Windows;
using Unity;
using Unity.Lifetime;

namespace ClinicViewDoctor
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            IUnityContainer currentContainer = BuildUnityContainer();

            var mainWindow = currentContainer.Resolve<WindowInitial>();
            mainWindow.Show();
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<IDiseaseStorage, DiseaseStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ISymptomaticStorage, SymptomaticStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IDoctorStorage, DoctorStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IDrugCourseStorage, DrugCourseStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMedicineStorage, MedicineStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IReceiptStorage, ReceiptStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStatisticStorage, StatisticStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<StatisticLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<MedicineLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ReceiptLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<DiseaseLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<SymptomaticLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<DoctorLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<DrugCourseLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ReportLogicDoctor>(new HierarchicalLifetimeManager());
            return currentContainer;
        }
    }
}
