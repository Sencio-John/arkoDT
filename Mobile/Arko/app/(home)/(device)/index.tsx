import React from 'react';
import { View, Text, StyleSheet, SafeAreaView, TouchableOpacity, Animated, ScrollView, Alert } from 'react-native';
import { useColorScheme } from "react-native";

import { ThemedText } from "@/components/ThemedText";
import { Ionicons } from "@expo/vector-icons";

import RNBluetoothClassic from 'react-native-bluetooth-classic';
import { DeviceBtn } from '@/components/buttons/deviceBtn';
import { DeviceConnectedBtn } from '@/components/buttons/connectedBtn';
import AsyncStorage from '@react-native-async-storage/async-storage';
import VerifyDeviceCred from '@/components/modals/verifyDeviceCred';
import ConnectDeviceModal from '@/components/modals/connectDeviceModal';
import IPAddressModal from '@/components/modals/IPAddModal';
import { useRouter } from 'expo-router';

export default function ConnectDevice (){
    const colorScheme = useColorScheme()
    const router = useRouter()
    const [scanning, setScanning] = React.useState(false);
    const [devices, setDevices] = React.useState([]);
    const [connectedDevice, setConnectedDevice] = React.useState(null);
    const [isBluetoothEnabled, setIsBluetoothEnabled] = React.useState(false);

    React.useEffect(() => {
        let scanInterval: NodeJS.Timeout;
        
        const loadConnectedDevice = async () => {
            try {
                const storedDevice = await AsyncStorage.getItem('connectedDevice');
                if (storedDevice) {
                    const parsedDevice = JSON.parse(storedDevice);

                    const isEnabled = await RNBluetoothClassic.isBluetoothEnabled();
                    if (!isEnabled) {
                        console.warn("Bluetooth is disabled. Unable to reconnect.");
                        setConnectedDevice(null);
                        return;
                    }

                    const isConnected = await RNBluetoothClassic.isDeviceConnected(parsedDevice.id);
                    if (isConnected) {
                        console.log('Device is already connected:', parsedDevice.name);
                        setConnectedDevice(parsedDevice);
                    } else {
                        const connected = await RNBluetoothClassic.connectToDevice(parsedDevice.id);
                        if (connected) {
                            console.log('Reconnected to device:', parsedDevice.name);
                            setConnectedDevice(parsedDevice);
                        } else {
                            console.warn('Failed to reconnect to device:', parsedDevice.name);
                            setConnectedDevice(null);
                        }
                    }
                    console.log('Loaded connected device:', parsedDevice);
                    
                } else{
                    await AsyncStorage.removeItem('connectedDevice');
                    await AsyncStorage.removeItem('ssid');
                    await AsyncStorage.removeItem('deviceKey');
                    await AsyncStorage.removeItem('IPAddress');
                }
            } catch (error) {
                console.error("Error loading connected device:", error);
            }
        };

        const initializeBluetooth = async () => {
            try {
                const available = await RNBluetoothClassic.isBluetoothAvailable();
                
                if (available) {
                    const enabled = await RNBluetoothClassic.isBluetoothEnabled();
                    setIsBluetoothEnabled(enabled);

                    if (enabled) {
                        const pairedDevices = await RNBluetoothClassic.getBondedDevices();
                        setDevices(pairedDevices);
                    } else {
                       Alert.alert("Notice", "Bluetooth is not enabled in this device.",);
                    }
                } else {
                    Alert.alert("Error", "Bluetooth is not available on this device.");
                }
            } catch (error) {
                console.error("Error during Bluetooth initialization:", error);
                Alert.alert("Error", "Failed to initialize Bluetooth. Please check your settings.");
            }
        };
        loadConnectedDevice();
        initializeBluetooth();

        // Cleanup interval on component unmount
        return () => {
            if (scanInterval) {
                clearInterval(scanInterval);
                console.log("clear interval");
            }
        };
    }, []);

    const handleConnect = async(address : any) =>{
        try {

            if (connectedDevice && connectedDevice?.id === address.id) {
                Alert.alert('Info', `You are already connected to ${address.name}`);
                return;
            }

            if (address.isConnecting) {
                Alert.alert('Info', `Already attempting to connect to ${address.name}`);
                return;
            }
            address.isConnecting = true;

            const connected = await RNBluetoothClassic.connectToDevice(address.id);

            if (connected) {
                setConnectedDevice(address);
                await AsyncStorage.setItem('connectedDevice', JSON.stringify(address));
                Alert.alert('Success', `Connected to device: ${address.name}`);
            } else {
                Alert.alert('Error', `Could not connect to device: ${address.name}`);
            }
            
        } catch (error) {
            Alert.alert('Error', 'Failed to connect to device. Please try again.');
            console.error("Error connecting to device:", error);
        } finally {
            address.isConnecting = false; 
        }
    }

    const handleDisconnect = async() =>{
        try {
            if(connectedDevice){
                await RNBluetoothClassic.disconnectFromDevice(connectedDevice.address);
                console.log("Disconnected to device:", connectedDevice?.name);
                await AsyncStorage.removeItem("connectedDevice")
                setConnectedDevice(null);
            }
           
        } catch (error) {
             Alert.alert('Error', 'Failed to disconnect from the device. Please try again.');
            console.error("Error disconnecting from device:", error);
        }
    }

    const [deviceCredModal, setDeviceCredModal] = React.useState(false);
    const [deviceConModal, setDeviceConModal] = React.useState(false);
    const [deviceIPModal, setDeviceIPModal] = React.useState(false)
    const toggleModal = () => {
        setDeviceCredModal(!deviceCredModal);
    };

    const sendDeviceVerify = async(key: any, pass: any) =>{
        
        try {
            const keyVerification = "Verify," + key + "," + pass;
            await AsyncStorage.setItem("deviceKey", key)
            await RNBluetoothClassic.writeToDevice(connectedDevice.id, keyVerification, 'utf8')    
            .then(() => { 
                console.log("key: ", keyVerification)
                setDeviceCredModal(false);
                setDeviceConModal(true);
            })
            .catch(error => {
                
            });
        } catch (error) {
            console.log(error);
        }
    }

    const sendDeviceConnection = async(pin: any, ssid: any, pass: any) =>{
        try {
            const keyVerification = "Wifi," + pin + "," + ssid + "," + pass;
            await AsyncStorage.setItem("ssid", ssid)
            await AsyncStorage.setItem("pin", pin)
            await RNBluetoothClassic.writeToDevice(connectedDevice.id, keyVerification, 'utf8')    
            .then(() => { 
                console.log("key: ", keyVerification)
                setDeviceConModal(false);
                setDeviceIPModal(true)
            })
            .catch(error => {
                
            });
        } catch (error) {
            console.log(error);
        }
    }

    const saveDeviceIPAddress = async(ip: any) =>{
        try {
            await AsyncStorage.setItem("IPAddress", ip)
            .then(async () => { 
                console.log("saved key: ", ip)
                setDeviceIPModal(false)
                // const serverWS = new WebSocket(`ws://${ip}:7777/verify`);
                // serverWS.onopen = async() => {
                //     console.log("ServerWS connected")
                //     const pinCode = await AsyncStorage.getItem("pin");
                //     const data = { token: pinCode };
                //     if (serverWS && serverWS.readyState === WebSocket.OPEN) {
                //         serverWS.send(JSON.stringify(data));
                //         console.log("Sent data to server:", data);
                //     }
                // };
                // serverWS.onclose = () => console.log('ServerWS disconnected');
                // serverWS.onerror = (error) => console.error('Server error', error);
                // serverWS.onmessage = async (event) => {
                //     try {
                //         const data = await JSON.parse(event.data);
                //         console.log("Get TOKEN from server: ",data);
                //         await AsyncStorage.setItem("token", data.token)
                //         const suc = await AsyncStorage.getItem("token")
                //         console.log(suc)
                //     } catch (error) {
                //         console.error("Error getting token data:", error);
                //     }
                // }

                router.replace("/(home)/")
                await RNBluetoothClassic.disconnectFromDevice(connectedDevice.address)
                await AsyncStorage.removeItem("connectedDevice");
                setConnectedDevice(null);

            })
            .catch(error => {
                
            });
        } catch (error) {
            console.log(error);
        }
    }

    return(
        <SafeAreaView style={[style.container, {backgroundColor: colorScheme === 'dark' ? '#151718' : '#fff'}]}>
            <ScrollView contentContainerStyle={style.scrollContent}>
                <View style={style.header}>
                    <ThemedText type="fade">
                        Connected Device
                    </ThemedText>
                </View>
                <View style={style.scannableDevice}>
                    {connectedDevice ? (    
                        <DeviceConnectedBtn
                            key={connectedDevice?.id}
                            device_id={connectedDevice?.id}
                            device_name={connectedDevice?.name}
                            onPress={toggleModal}
                            onDisconnect={() => handleDisconnect()}
                        />
                    ) : (
                        <ThemedText>No device connected.</ThemedText>
                    )}
                </View>
                <View style={style.header}>
                    <ThemedText type="fade">
                        Paired Devices
                    </ThemedText>
                </View>

                <View style={style.scannableDevice}>
                    {scanning && <ThemedText>Scanning for devices...</ThemedText>}
                    {devices.length > 0 ? (
                        devices.map((device) => (
                        <DeviceBtn
                            key={device?.id}
                            device_id={device?.id}
                            device_name={device?.name || device?.localName || "Unnamed Device"}
                            onPress={() => handleConnect(device)}
                        />
                        ))
                    ) : (
                        !scanning && <ThemedText>No devices found.</ThemedText>
                    )}  

                </View>

            </ScrollView>

            <VerifyDeviceCred 
                onClose={async () => setDeviceCredModal(false)} 
                modalVisible={deviceCredModal} 
                onVerify={sendDeviceVerify} />
            <ConnectDeviceModal 
                onClose={async () => setDeviceConModal(false)} 
                modalVisible={deviceConModal} 
                onVerify={sendDeviceConnection} />

            <IPAddressModal 
                onClose = {async () => setDeviceIPModal(false)}
                modalVisible = {deviceIPModal}
                onVerify={saveDeviceIPAddress}
                />
        </SafeAreaView>
    )
}

const style = StyleSheet.create({
    container:{
        flex: 1,
    },
    scrollContent: {
        flexGrow: 1,
    },
    header:{
        margin: 25,
        paddingVertical: 10,
        borderBottomColor: "#7F7F7F",
        borderBottomWidth: 1,
        flexDirection: "row",
        justifyContent: "space-between"
    },
    icon:{
        fontSize: 24
    },
    scannableDevice:{
        flexDirection: "column",
        alignItems: "center",
        marginHorizontal: 25,
    }
});