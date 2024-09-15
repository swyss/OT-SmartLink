using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace MainUI.ViewModels;

public class ConfigurationViewModel : ViewModelBase
{
    // Observable collection for the list of endpoints
    public ObservableCollection<EndpointConfig> Endpoints { get; } = new();

    // Commands for managing endpoints
    public ICommand AddEndpointCommand { get; }
    public ICommand EditEndpointCommand { get; }
    public ICommand DeleteEndpointCommand { get; }

    // Constructor
    public ConfigurationViewModel()
    {
        // Initialize commands
        AddEndpointCommand = new RelayCommand(AddEndpoint);
        EditEndpointCommand = new RelayCommand<EndpointConfig>(EditEndpoint);
        DeleteEndpointCommand = new RelayCommand<EndpointConfig>(DeleteEndpoint);

        // Load some sample data (this would normally come from a service or configuration file)
        LoadInitialData();
    }

    // Method to load initial configuration data (simulated here)
    private void LoadInitialData()
    {
        Endpoints.Add(new EndpointConfig
            { Name = "OPC UA Server 1", Address = "opc.tcp://localhost:4840", Type = "OPC UA" });
        Endpoints.Add(new EndpointConfig
            { Name = "MQTT Broker", Address = "mqtt://broker.hivemq.com:1883", Type = "MQTT" });
    }

    // Method to add a new endpoint
    private void AddEndpoint()
    {
        // Simulated logic to add a new endpoint
        Endpoints.Add(new EndpointConfig { Name = "New Endpoint", Address = "localhost", Type = "Unknown" });
    }

    // Method to edit an existing endpoint
    private void EditEndpoint(EndpointConfig endpoint)
    {
        if (endpoint != null)
            // Simulated logic for editing the endpoint (you could open a dialog or show a form here)
            endpoint.Name = "Edited " + endpoint.Name;
    }

    // Method to delete an existing endpoint
    private void DeleteEndpoint(EndpointConfig endpoint)
    {
        if (endpoint != null) Endpoints.Remove(endpoint);
    }
}

// Class representing an endpoint configuration
public class EndpointConfig
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Type { get; set; }
}