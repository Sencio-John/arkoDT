import * as React from 'react';
import { View, Text, StyleSheet, StatusBar, SafeAreaView, ActivityIndicator, Alert } from 'react-native';
import * as ScreenOrientation from 'expo-screen-orientation';

import Light from '@/components/controls/Light';
import MicBtn from '@/components/controls/Mic';
import DPad from '@/components/controls/Dpad';
import Gear from '@/components/controls/GearBtn';
import ThrottleControl from '@/components/controls/Throttle';
import BrakeBtn from '@/components/controls/BrakeBtn';
import { WebView } from 'react-native-webview';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { useRouter } from 'expo-router';
import Readings from '@/components/controls/GPStxt';

import { ThemedView } from '@/components/ThemedView';
import { ThemedText } from '@/components/ThemedText';


export default function Controller() {
    const router = useRouter();
    const [isBraking, setIsBraking] = React.useState(false);
    const [IPAddress, setIPAddress] = React.useState<string | null>(null);
    const [controlWS, setControlWS] = React.useState<WebSocket | null>(null);
    const [serverWS, setServerWS] = React.useState<WebSocket | null>(null);
    const [pinCode, setPinCode] = React.useState(null);
    const [loading, setLoading] = React.useState(true);


    const lockOrientation = async () => {
        await ScreenOrientation.lockAsync(ScreenOrientation.OrientationLock.LANDSCAPE);
        StatusBar.setHidden(true);
    };

    const revertBack = async () =>{
        await ScreenOrientation.lockAsync(ScreenOrientation.OrientationLock.PORTRAIT);
        console.log('WebSocket disconnected');
        StatusBar.setHidden(false);

    }   

    //initialize
    React.useEffect(() => {
        const getIPAddress = async () => {
            try {
                const ip = await AsyncStorage.getItem("IPAddress");
                const pin = await AsyncStorage.getItem("pin");

                setIPAddress(ip);
                setPinCode(pin);
                console.log("Retrieved IP:", ip);
                console.log("Retrieved PIN:", pin);
            } catch (error) {
                console.error("Error fetching IP or PIN:", error);
            } finally {
                setLoading(false);
            }
        };

        getIPAddress();
        lockOrientation();

        return () => {
            revertBack();
        };
    }, []);

    //Controls
    React.useEffect(() => {

        if (!IPAddress) return;

        const control = new WebSocket(`ws://${IPAddress}:4343/controls`);
        setControlWS(control);

        control.onopen = () => console.log('Control Socket connected');
        control.onclose = () => {

        };
        control.onerror = (error) => console.error('Control Socket error', error);

        control.onmessage = async (event) => {
            try {
                const data = JSON.parse(event.data);
                console.log("control response: ",data);
            } catch (error) {
                console.error("Error parsing GPS data:", error);
            }
        }

        return () => {     
            control.close()
            setControlWS(null)
            console.log(controlWS)
        };
    }, [IPAddress]);

    const sendControlData = async(data: object) => {
        // const token = await AsyncStorage.getItem("token");
        const token = "Test"

        if (controlWS && controlWS.readyState === WebSocket.OPEN) {
            const payload = {
                token: token, 
                ...data
            };
            controlWS.send(JSON.stringify(payload));
            console.log(payload)
        }
    };

    const [gpsData, setGpsData] = React.useState({ latitude: null, longitude: null });
    const [waterLvl, setWaterLvl] = React.useState(null)
    const [infrared, setInfrared] = React.useState(null)


    //readings
    React.useEffect(() => {

        if (!IPAddress) return;

        const readings = new WebSocket(`ws://${IPAddress}:5000`);

        readings.onopen = () => {
            console.log("Readings connected")
            const token = "Test"

            if (readings && readings.readyState === WebSocket.OPEN) {
                const payload = {
                    token: token, 
                };
                readings.send(JSON.stringify(payload));
                console.log("load:", payload)
            }
        };
        readings.onclose = () => {

        };
        readings.onerror = (error) => console.error('Readings Socket error', error);

        readings.onmessage = async (event) => {
            try {
                const data = JSON.parse(event.data);
                console.log(data);
                setGpsData({ latitude: data.GPS.Latitude, longitude: data.GPS.Longitude });
                setWaterLvl(data.water_level)
                setInfrared(data.detected)
            } catch (error) {
                console.error("Error getting readings data:", error);
            }
        }

        return () => {     
            readings.close()
        };
    }, [IPAddress]);

    
    React.useEffect(() => {

        if (!IPAddress) return;

        const serverWS = new WebSocket(`ws://${IPAddress}:7777/verify`);
        setServerWS(serverWS)
        serverWS.onopen = async() => {
            console.log("ServerWS connected")
            sendData();
        };
        serverWS.onclose = () => console.log('ServerWS disconnected');
        serverWS.onerror = (error) => console.error('Server error', error);
        serverWS.onmessage = async (event) => {
            try {
                const data = JSON.parse(event.data);
                console.log("Get TOKEN from server: ",data);
                await AsyncStorage.setItem("token", data.token)
                const suc = await AsyncStorage.getItem("token")
                console.log(suc)
            } catch (error) {
                console.error("Error getting token data:", error);
            }
        }

        return () => {
            serverWS.close()
        };
    }, [IPAddress]);

    const sendData = async() =>{
        if (pinCode) {
            const data = { token: pinCode };
            console.log(serverWS);
            if (serverWS && serverWS.readyState === WebSocket.OPEN) {
                serverWS.send(JSON.stringify(data));
                console.log("Sent data to server:", data);
            }
        } else{
            Alert.alert("Error", "No Pin Code");
        }
        
    }

    if (loading) {
        return (
            <ThemedView style={style.centered}>
                <ActivityIndicator size="large" color="#0000ff" />
                <ThemedText>Loading...</ThemedText>
            </ThemedView>
        );
    }


    return(
        <View style={style.container}>  
            {IPAddress ? (
                <WebView
                    source={{ uri: `http://${IPAddress}:4000` }}
                    originWhitelist={['*']}
                    javaScriptEnabled={true}
                    domStorageEnabled={true}
                    style={style.video}
                />
            ) : (
                <View style={style.centered}>
                    <Text style={{ color: "red" }}>No valid IP Address found</Text>
                </View>
            )}

            <View style={style.gear}>
                <Gear onDataSend={sendControlData}/>
            </View>

            <View style={style.throttle}>
                <ThrottleControl onDataSend={sendControlData} isBraking={isBraking}/>
            </View>

            <View style={style.brakebtn}>
                <BrakeBtn onDataSend={sendControlData} onBrakeChange={setIsBraking}/>
            </View>

            <View style={style.dpad}>
                <DPad onDataSend={sendControlData} />
            </View> 

            <View style={style.btns}> 
                <Light onDataSend={sendControlData} />
                <MicBtn onDataSend={sendControlData}/>
            </View>
            
            <View style={style.details}>
                <Readings gpsData={gpsData} waterLvl={waterLvl} infrared={infrared}/>
            </View>
        </View>
    )
}   

const style = StyleSheet.create({
    container:{
        flex: 1,
    },
    joystick:{
        position: 'absolute',
        bottom: 45, 
        left: 45,   
    },
    dpad:{
        position: 'absolute',
        bottom: 45, 
        right: 40,   
    },
    throttle:{
        position: 'absolute',
        left: 20,
        bottom: 125,
    },
    brakebtn: {
        position: 'absolute',
        bottom: 55,
        left: 150,
    },
    video: {
        width: '100%',
        height: 'auto',
    },
    gear:{
        position: 'absolute',
        bottom: 55,
        left: 75,
    },
    btns:{
        position: 'absolute',
        right: 10,
        top: 30,
        zIndex: 1,
    },
    details:{
        position: "absolute",
        zIndex: 2,
    },
    overlay:{
        ...StyleSheet.absoluteFillObject,
        backgroundColor: '#212121',
        justifyContent: "center",
        alignItems: "center",
        zIndex: 9999,
    },
    centered: {
        flex: 1,
        justifyContent: "center",
        alignItems: "center",
    },
})