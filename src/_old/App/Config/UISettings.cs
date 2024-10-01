namespace App;

public class UISettings
{
    public AvaloniaSettings AvaloniaUI { get; set; }
    public ElectronSettings ElectronClientUI { get; set; }
}

public class AvaloniaSettings
{
    public bool Enabled { get; set; }
    public string Description { get; set; }
}

public class ElectronSettings
{
    public bool Enabled { get; set; }
    public string Description { get; set; }
}
