using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Core.Broker;

namespace MainUI.ViewModels;

public class AnalysisViewModel: ViewModelBase
{
    // Example data that can be analyzed
    public ObservableCollection<string> AnalysisResults { get; } = new ObservableCollection<string>();

    public ICommand RefreshAnalysisCommand { get; }

    public AnalysisViewModel()
    {
        // Initialize the command to refresh the analysis data
        RefreshAnalysisCommand = new RelayCommand(RefreshAnalysis);

        // Load initial data for the analysis view
        RefreshAnalysis();
    }

    // Method to refresh or load the analysis data
    private void RefreshAnalysis()
    {
        AnalysisResults.Clear();

        // Example: Simulate some analysis data
        AnalysisResults.Add("Analysis Result 1: System Performance is Optimal.");
        AnalysisResults.Add("Analysis Result 2: 85% of services are running.");
        AnalysisResults.Add("Analysis Result 3: No critical errors detected.");
        // Add more data as needed
    }
}