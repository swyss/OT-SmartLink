# API Specifications

## OPC UA/DA
- **OPC UA/DA Interface**: Enables real-time data collection and monitoring from industrial devices.
- **Security**: Supports encryption and authentication via OPC UA standards.

## REST API
- **Endpoints**:
    - `/api/devices`: Fetch device data.
    - `/api/sensors`: Retrieve sensor readings.
    - `/api/logs`: Fetch system logs for auditing.
- **Methods**: GET, POST, PUT, DELETE.

## MQTT
- **Topics**:
    - `iot/sensors/data`: Publishes sensor data.
    - `iot/devices/status`: Monitors device status.
- **QoS**: Supports different quality of service levels for message delivery.

## Modbus
- **Communication**: Connects to PLCs and industrial devices for data exchange.
- **Registers**: Mapping of device data to Modbus registers.

[Return to README](../README.md)