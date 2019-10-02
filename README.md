# Purpose:
To digitally transform pest control through operational efficiency driven by intelligent information. 

# Hypothesis: 
By instrumenting trap’s with IoT sensors, cataloging the specific positions, and capturing trap & clear events in the cloud, we can apply machine learning algorithms to understand patterns and provide predictive analytics to optimally schedule trap visits and ideal location placement for trap & field technician efficiency.

# Technology Overview:
PaaS Services used for easy Scale-out , Reliable “Always On” SLA, and low cost of maintenance.
Services loosely coupled together so the architecture is flexible to future enhancements & maintenance. 

# Trap Sensor: Windows IoT Core
- A closed circuit on GPIO is applied to a traditional trap, which is connected to an IP cloud-connected gateway device to collect and transmit data.
- Raspberry Pi 2 running Windows IOT Core
- UWP App monitors circuit and sends events to Azure IoT Hub

In production scenarios, it would be more cost efficient to use multiple sensors connected to a single gateway device, ideally through a wireless low power device (i.e. zigbee, zwave or Bluetooth)

