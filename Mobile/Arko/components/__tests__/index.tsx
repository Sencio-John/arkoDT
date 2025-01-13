import React, { useState, useEffect } from 'react';
import { View, Text, Button, Alert } from 'react-native';
import RNBluetoothClassic from 'react-native-bluetooth-classic';

const BluetoothExample = () => {
  const [isBluetoothEnabled, setIsBluetoothEnabled] = useState(false);
  const [devices, setDevices] = useState([]);
  const [connectedDevice, setConnectedDevice] = useState(null);
  const [receivedData, setReceivedData] = useState('');

  useEffect(() => {
    // Check if Bluetooth is available and enabled
    const checkBluetoothStatus = async () => {
      try {
        const available = await RNBluetoothClassic.isBluetoothAvailable();
        if (available) {
          const enabled = await RNBluetoothClassic.isBluetoothEnabled();
          setIsBluetoothEnabled(enabled);
        }
      } catch (error) {
        console.error("Error checking Bluetooth status:", error);
      }
    };

    checkBluetoothStatus();
  }, []);

  const scanForDevices = async () => {
    try {
      const pairedDevices = await RNBluetoothClassic.getBondedDevices();
      setDevices(pairedDevices);
    } catch (error) {
      Alert.alert('Error', 'Failed to scan for devices');
      console.error("Error scanning for devices:", error);
    }
  };

  const connectToDevice = async (device) => {
    try {
      await RNBluetoothClassic.connect(device.id);
      setConnectedDevice(device);
      Alert.alert('Success', `Connected to ${device.name}`);

      // Listen for incoming data if necessary
      // You might have to use `read` or some other method depending on your version
      setInterval(async () => {
        try {
          const data = await RNBluetoothClassic.read();
          setReceivedData(data);
        } catch (error) {
          console.error("Error reading data:", error);
        }
      }, 1000); // Read data every second (adjust as necessary)

    } catch (error) {
      Alert.alert('Error', 'Failed to connect to device');
      console.error("Error connecting to device:", error);
    }
  };

  const disconnectDevice = async () => {
    if (connectedDevice) {
      try {
        await RNBluetoothClassic.disconnect();
        setConnectedDevice(null);
        Alert.alert('Disconnected', 'Device disconnected successfully');
      } catch (error) {
        Alert.alert('Error', 'Failed to disconnect from device');
        console.error("Error disconnecting device:", error);
      }
    } else {
      Alert.alert('Error', 'No device connected');
    }
  };

  return (
    <View style={{ padding: 20 }}>
      <Text>Bluetooth Classic Example</Text>
      <Text>Bluetooth Enabled: {isBluetoothEnabled ? 'Yes' : 'No'}</Text>

      <Button title="Scan for Devices" onPress={scanForDevices} />
      <View style={{ marginTop: 20 }}>
        {devices.length > 0 && <Text>Available Devices:</Text>}
        {devices.map((device) => (
          <Button
            key={device.id}
            title={`Connect to ${device.name}`}
            onPress={() => connectToDevice(device)}
          />
        ))}
      </View>

      {connectedDevice && (
        <>
          <Text style={{ marginTop: 20 }}>
            Connected to: {connectedDevice.name}
          </Text>
          <Button title="Send Data" onPress={sendData} />
          <Button title="Disconnect" onPress={disconnectDevice} />
          <Text>Received Data: {receivedData}</Text>
        </>
      )}
    </View>
  );
};

export default BluetoothExample;
