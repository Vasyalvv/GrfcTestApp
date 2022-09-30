using GrfcTestApp.Data.Entities;
using GrfcTestApp.Data.Entities.Engines;
using GrfcTestApp.Infrastructure.Commands;
using GrfcTestApp.Services.Interfaces;
using GrfcTestApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;

namespace GrfcTestApp.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private readonly IRepository<Automobile> _AutomobilesRepos;
        private readonly IRepository<CarMark> _CarMarksRepos;
        private readonly IRepository<EngineBase> _EnginesRepos;
        private readonly IRepository<Maintenance> _MaintenancesRepos;
        private readonly IRepository<CarModel> _CarModelsRepos;
        private readonly IRepository<Operation> _OperationsRepos;

        #region Свойства

        #region Коллекции
        private ObservableCollection<Automobile> _Automobiles;
        public ObservableCollection<Automobile> Automobiles
        {
            get => _Automobiles;
            set => Set(ref _Automobiles, value);
        }

        private ObservableCollection<CarMark> _CarMarks;
        public ObservableCollection<CarMark> CarMarks
        {
            get => _CarMarks;
            set => Set(ref _CarMarks, value);
        }

        private ObservableCollection<EngineBase> _Engines;
        public ObservableCollection<EngineBase> Engines
        {
            get => _Engines;
            set => Set(ref _Engines, value);
        }

        private ObservableCollection<Maintenance> _Maintenances;
        public ObservableCollection<Maintenance> Maintenances
        {
            get => _Maintenances;
            set => Set(ref _Maintenances, value);
        }

        private ObservableCollection<CarModel> _CarModels;
        public ObservableCollection<CarModel> CarModels
        {
            get => _CarModels;
            set => Set(ref _CarModels, value);
        }

        private ObservableCollection<Operation> _Operations;
        public ObservableCollection<Operation> Operations
        {
            get => _Operations;
            set => Set(ref _Operations, value);
        }
        #endregion

        #region Заголовок окна
        private string _Title = "Главное окно";
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion

        #region Выбранные элементы

        private EngineBase _SelectedEngine;
        public EngineBase SelectedEngine
        {
            get => _SelectedEngine;
            set => Set(ref _SelectedEngine, value);
        }

        private Automobile _SelectedAutomobile;
        public Automobile SelectedAutomobile
        {
            get => _SelectedAutomobile;
            set => Set(ref _SelectedAutomobile, value);
        }

        private CarModel _SelectedCarModel;
        public CarModel SelectedCarModel
        {
            get => _SelectedCarModel;
            set => Set(ref _SelectedCarModel, value);
        }

        private CarMark _SelectedCarMark;
        public CarMark SelectedCarMark
        {
            get => _SelectedCarMark;
            set => Set(ref _SelectedCarMark, value);
        }

        private Maintenance _SelectedMaintenance;
        public Maintenance SelectedMaintenance
        {
            get => _SelectedMaintenance;
            set => Set(ref _SelectedMaintenance, value);
        }

        private Operation _SelectedOperation;
        public Operation SelectedOperation
        {
            get => _SelectedOperation;
            set => Set(ref _SelectedOperation, value);
        }

        private DateTime _SelectedDate;
        public DateTime SelectedDate
        {
            get => _SelectedDate;
            set => Set(ref _SelectedDate, value);
        }

        #endregion

        #endregion

        #region Команды

        #region CloseApplicationCommand

        private ICommand _CloseApplicationCommand;
        public ICommand CloseApplicationCommand => _CloseApplicationCommand;

        private bool CanCloseApplicationCommandExecute(object p) => true;

        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }

        #endregion

        #region LoadDataCommand

        private ICommand _LoadDataCommand;

        public ICommand LoadDataCommand => _LoadDataCommand;

        private void OnLoadDataCommandExecuted(object p)
        {
            DataInitialize();
        }

        #endregion

        #region RemoveCarMarkCommand

        private ICommand _RemoveCarMarkCommand;

        public ICommand RemoveCarMarkCommand => _RemoveCarMarkCommand;

        private bool CanRemoveCarMarkCommandExecute(object p) => !(SelectedCarMark is null);

        private void OnRemoveCarMarkCommandExecuted(object p)
        {
            _CarMarksRepos.Remove(SelectedCarMark);
            CarMarks.Remove(SelectedCarMark);
        }

        #endregion

        #region RemoveAutomobileCommand

        private ICommand _RemoveAutomobileCommand;

        public ICommand RemoveAutomobileCommand => _RemoveAutomobileCommand;

        private bool CanRemoveAutomobileCommandExecute(object p) => !(SelectedAutomobile is null);

        private void OnRemoveAutomobileCommandExecuted(object p)
        {
            _AutomobilesRepos.Remove(SelectedAutomobile);
            Automobiles.Remove(SelectedAutomobile);
        }

        #endregion

        #region RemoveCarModelCommand

        private ICommand _RemoveCarModelCommand;

        public ICommand RemoveCarModelCommand => _RemoveCarModelCommand;

        private bool CanRemoveCarModelCommandExecute(object p) => !(SelectedCarModel is null);

        private void OnRemoveCarModelCommandExecuted(object p)
        {
            _CarModelsRepos.Remove(SelectedCarModel);
            CarModels.Remove(SelectedCarModel);
        }

        #endregion

        #region RemoveOperationCommand

        private ICommand _RemoveOperationCommand;

        public ICommand RemoveOperationCommand => _RemoveOperationCommand;

        private bool CanRemoveOperationCommandExecute(object p) => !(SelectedOperation is null);

        private void OnRemoveOperationCommandExecuted(object p)
        {
            _OperationsRepos.Remove(SelectedOperation);
            Operations.Remove(SelectedOperation);
        }

        #endregion

        #region RemoveEngineCommand

        private ICommand _RemoveEngineCommand;

        public ICommand RemoveEngineCommand => _RemoveEngineCommand;

        private bool CanRemoveEngineCommandExecute(object p) => !(SelectedEngine is null);

        private void OnRemoveEngineCommandExecuted(object p)
        {
            _EnginesRepos.Remove(SelectedEngine);
            Engines.Remove(SelectedEngine);
        }

        #endregion

        #region RemoveMaintenanceCommand

        private ICommand _RemoveMaintenanceCommand;

        public ICommand RemoveMaintenanceCommand => _RemoveMaintenanceCommand;

        private bool CanRemoveMaintenanceCommandExecute(object p) => !(SelectedMaintenance is null);

        private void OnRemoveMaintenanceCommandExecuted(object p)
        {
            _MaintenancesRepos.Remove(SelectedMaintenance);
            Maintenances.Remove(SelectedMaintenance);
        }

        #endregion

        #region RefreshCarMarkCommand

        private ICommand _RefreshCarMarkCommand;

        public ICommand RefreshCarMarkCommand => _RefreshCarMarkCommand;

        private void OnRefreshCarMarkCommandExecuted(object p)
        {
            CarMarks = new ObservableCollection<CarMark>(_CarMarksRepos.GetAll());
        }

        #endregion

        #region RefreshAutomobileCommand

        private ICommand _RefreshAutomobileCommand;

        public ICommand RefreshAutomobileCommand => _RefreshAutomobileCommand;

        private void OnRefreshAutomobileCommandExecuted(object p)
        {
            Automobiles = new ObservableCollection<Automobile>(_AutomobilesRepos.GetAll());
        }

        #endregion

        #region RefreshCarModelCommand

        private ICommand _RefreshCarModelCommand;

        public ICommand RefreshCarModelCommand => _RefreshCarModelCommand;

        private void OnRefreshCarModelCommandExecuted(object p)
        {
            CarModels = new ObservableCollection<CarModel>(_CarModelsRepos.GetAll());
        }

        #endregion

        #region RefreshOperationCommand

        private ICommand _RefreshOperationCommand;

        public ICommand RefreshOperationCommand => _RefreshOperationCommand;

        private void OnRefreshOperationCommandExecuted(object p)
        {
            Operations = new ObservableCollection<Operation>(_OperationsRepos.GetAll());
        }

        #endregion

        #region RefreshEngineCommand

        private ICommand _RefreshEngineCommand;

        public ICommand RefreshEngineCommand => _RefreshEngineCommand;

        private void OnRefreshEngineCommandExecuted(object p)
        {
            Engines = new ObservableCollection<EngineBase>(_EnginesRepos.GetAll());
        }

        #endregion

        #region RefreshMaintenanceCommand

        private ICommand _RefreshMaintenanceCommand;

        public ICommand RefreshMaintenanceCommand => _RefreshMaintenanceCommand;

        private void OnRefreshMaintenanceCommandExecuted(object p)
        {
            Maintenances = new ObservableCollection<Maintenance>(_MaintenancesRepos.GetAll());
        }

        #endregion

        #endregion

        public MainWindowViewModel(IRepository<Automobile> automobilesRepos,
            IRepository<CarMark> carMarksRepos,
            IRepository<EngineBase> enginesRepos,
            IRepository<Maintenance> maintenancesRepos,
            IRepository<CarModel> carModelsRepos,
            IRepository<Operation> operationsRepos)
        {
            _AutomobilesRepos = automobilesRepos;
            _CarMarksRepos = carMarksRepos;
            _EnginesRepos = enginesRepos;
            _MaintenancesRepos = maintenancesRepos;
            _CarModelsRepos = carModelsRepos;
            _OperationsRepos = operationsRepos;

            #region Команды

            _CloseApplicationCommand =
                new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);

            _LoadDataCommand = new LambdaCommand(OnLoadDataCommandExecuted);

            _RemoveCarMarkCommand =
                new LambdaCommand(OnRemoveCarMarkCommandExecuted, CanRemoveCarMarkCommandExecute);

            _RemoveAutomobileCommand =
                new LambdaCommand(OnRemoveAutomobileCommandExecuted, CanRemoveAutomobileCommandExecute);

            _RemoveCarModelCommand =
                new LambdaCommand(OnRemoveCarModelCommandExecuted, CanRemoveCarModelCommandExecute);

            _RemoveOperationCommand =
                new LambdaCommand(OnRemoveOperationCommandExecuted, CanRemoveOperationCommandExecute);

            _RemoveEngineCommand =
                new LambdaCommand(OnRemoveEngineCommandExecuted, CanRemoveEngineCommandExecute);

            _RemoveMaintenanceCommand =
                new LambdaCommand(OnRemoveMaintenanceCommandExecuted, CanRemoveMaintenanceCommandExecute);

            _RefreshCarMarkCommand =
                new LambdaCommand(OnRefreshCarMarkCommandExecuted);

            _RefreshAutomobileCommand =
                new LambdaCommand(OnRefreshAutomobileCommandExecuted);

            _RefreshCarModelCommand =
                new LambdaCommand(OnRefreshCarModelCommandExecuted);

            _RefreshOperationCommand =
                new LambdaCommand(OnRefreshOperationCommandExecuted);

            _RefreshEngineCommand =
                new LambdaCommand(OnRefreshEngineCommandExecuted);

            _RefreshMaintenanceCommand =
                new LambdaCommand(OnRefreshMaintenanceCommandExecuted);

            #endregion

            DataInitialize();
        }


        private void DataInitialize()
        {
            Automobiles = new ObservableCollection<Automobile>(_AutomobilesRepos.GetAll());
            CarMarks = new ObservableCollection<CarMark>(_CarMarksRepos.GetAll());
            Engines = new ObservableCollection<EngineBase>(_EnginesRepos.GetAll());
            Maintenances = new ObservableCollection<Maintenance>(_MaintenancesRepos.GetAll());
            CarModels = new ObservableCollection<CarModel>(_CarModelsRepos.GetAll());
            Operations = new ObservableCollection<Operation>(_OperationsRepos.GetAll());
        }
    }
}
