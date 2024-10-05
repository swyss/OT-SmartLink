# Software Architecture

## Microservice Architecture
The OT-SmartLink application is designed using a microservice architecture, where each module operates independently. Major modules include:
- **OPC UA Gateway**: Handles communication with OPC UA/DA devices.
- **Database Management**: Manages data storage in InfluxDB and PostgreSQL.
- **API Module**: Provides external interfaces (REST API, MQTT, Modbus).

## Key Components
- **OPC UA/DA Module**: Integrates with industrial devices for data collection.
- **Data Processor**: Processes and models incoming data using semantic technologies.
- **UI Module**: User interface for real-time monitoring and configuration.

[Return to README](../README.md)
