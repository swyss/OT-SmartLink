using Core.Services;
using NModbus; // Modbus library for .NET
using System.Net.Sockets;

namespace AgentModbus;

public class ModbusWorker : WorkerBase<ModbusWorker>
{
    private readonly TcpClient _tcpClient;
    private readonly IModbusMaster _modbusMaster;
    private const string ModbusServerIp = "192.168.0.100"; // Beispiel IP-Adresse
    private const int ModbusPort = 502; // Standard Modbus TCP Port

    public ModbusWorker(ILogger<ModbusWorker> logger)
        : base(logger)
    {
        // Initialisieren des TCP Clients und Modbus Masters
        _tcpClient = new TcpClient(ModbusServerIp, ModbusPort);
        var factory = new ModbusFactory();
        _modbusMaster = factory.CreateMaster(_tcpClient);
    }

    protected override async Task DoWorkAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                // Beispiel: Lese Holding Register ab Adresse 1 (10 Register)
                ushort startAddress = 1;
                ushort numberOfPoints = 10;
                ushort[] registers = _modbusMaster.ReadHoldingRegisters(1, startAddress, numberOfPoints);

                _logger.LogInformation("Modbus data: {Data}", string.Join(", ", registers));

                // Verarbeite Modbus-Daten oder speichere in Datenbank
                await Task.Delay(5000, stoppingToken); // Lese alle 5 Sekunden
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during Modbus communication");
            }
        }
    }

    public override void Dispose()
    {
        _tcpClient?.Close();
        base.Dispose();
    }
}