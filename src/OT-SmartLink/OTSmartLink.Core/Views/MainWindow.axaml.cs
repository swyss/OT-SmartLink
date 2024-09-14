using Avalonia.Controls;
using OTSmartLink.Core.ViewModels;

namespace OTSmartLink.Core.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();  // Set the ViewModel as the DataContext
    }
}