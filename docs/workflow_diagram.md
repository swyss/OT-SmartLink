# Workflow Diagram

The following is a simplified workflow of the OT-SmartLink application:

1. **Sensor/PLC Communication**: Devices send data to OT-SmartLink via OPC UA/DA.
2. **Data Processing**: Data is processed and modeled using semantic technologies.
3. **Data Storage**: Time-series data is stored in InfluxDB, while relational data is stored in PostgreSQL.
4. **External Communication**: Data is shared via REST API, MQTT, and Modbus.
5. **User Interaction**: Users monitor and configure devices through the UI.
