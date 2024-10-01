namespace App;

public class ModuleSettings
{
    public List<AgentConfig> Agents { get; set; }
    public List<ServiceConfig> Services { get; set; }
    public CoreSettings Core { get; set; }
}

public class AgentConfig
{
    public string Name { get; set; }
    public bool Enabled { get; set; }
    public string Description { get; set; }
}

public class ServiceConfig
{
    public string Name { get; set; }
    public bool Enabled { get; set; }
    public string Description { get; set; }
}

public class CoreSettings
{
    public bool Enabled { get; set; }
    public BrokerSettings Broker { get; set; }
    public OrchestratorSettings Orchestrator { get; set; }
}

public class BrokerSettings
{
    public bool Enabled { get; set; }
}

public class OrchestratorSettings
{
    public bool Enabled { get; set; }
}
