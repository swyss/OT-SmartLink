using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Core.Broker;

namespace MainUI.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly ServiceBroker _serviceBroker;
    private object _currentView;

    // Constructor
    public MainWindowViewModel(ServiceBroker serviceBroker)
    {
        _serviceBroker = serviceBroker;

        // Initialize the navigation commands
        NavigateDashboardCommand = new RelayCommand(() => CurrentView = new DashboardViewModel(_serviceBroker));
        NavigateAnalysisCommand = new RelayCommand(() => CurrentView = new AnalysisViewModel());
        NavigateConfigurationCommand = new RelayCommand(() => CurrentView = new ConfigurationViewModel());

        // Initialize the file-related commands
        OpenProjectCommand = new RelayCommand(OpenProject);
        SaveProjectCommand = new RelayCommand(SaveProject);
        ExitCommand = new RelayCommand(ExitApplication);

        // Set default view to Dashboard
        CurrentView = new DashboardViewModel(_serviceBroker);
    }

    // Property for the current view
    public object CurrentView
    {
        get => _currentView;
        set => SetProperty(ref _currentView, value);
    }

    // Commands for file actions
    public ICommand OpenProjectCommand { get; }
    public ICommand SaveProjectCommand { get; }
    public ICommand ExitCommand { get; }

    // Commands for navigation
    public ICommand NavigateDashboardCommand { get; }
    public ICommand NavigateAnalysisCommand { get; }
    public ICommand NavigateConfigurationCommand { get; }

    // Methods for commands
    private void OpenProject()
    {
        // Logic to open a project
    }

    private void SaveProject()
    {
        // Logic to save a project
    }

    private void ExitApplication()
    {
        // Logic to exit the application
    }
}