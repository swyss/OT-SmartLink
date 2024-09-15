using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MainUI.ViewModels;

namespace MainUI.Views;

public partial class DashboardView : UserControl
{
    public DashboardView()
    {
        InitializeComponent();

        // DataContext für die View setzen, falls nicht durch die App initialisiert
        DataContext = new DashboardViewModel(App.ServiceBrokerInstance); // Hier den ServiceBroker übergeben
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}