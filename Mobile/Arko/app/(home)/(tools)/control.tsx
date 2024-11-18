import * as React from 'react';
import { View, Text, StyleSheet, StatusBar, SafeAreaView, ActivityIndicator } from 'react-native';
import * as ScreenOrientation from 'expo-screen-orientation';

import Light from '@/components/controls/Light';
import GPSTracker from '@/components/controls/GPStxt';
import MicBtn from '@/components/controls/Mic';
import PinBtn from '@/components/controls/Pinbtn';
import DPad from '@/components/controls/Dpad';
import Gear from '@/components/controls/GearBtn';
import ThrottleControl from '@/components/controls/Throttle';
import BrakeBtn from '@/components/controls/BrakeBtn';
import { WebView } from 'react-native-webview';
import CamMvt from '@/components/controls/CameraMvt';

import { CAMERA_IP, CONTROL_IP, READ_IP } from '@/constants/config';

export default function Controller() {

    const lockOrientation = async () => {
        await ScreenOrientation.lockAsync(ScreenOrientation.OrientationLock.LANDSCAPE);
        StatusBar.setHidden(true);
    };

    const revertBack = async () =>{
        await ScreenOrientation.lockAsync(ScreenOrientation.OrientationLock.PORTRAIT);
        StatusBar.setHidden(false);
    }   

    React.useEffect(() => {

        lockOrientation();
        
        return () => {
            revertBack();
        };
    }, []);


    const [websocket, setWebSocket] = React.useState<WebSocket | null>(null);
    
    React.useEffect(() => {
        const controlWS = new WebSocket(`ws://${CONTROL_IP}`);
        setWebSocket(controlWS);

        controlWS.onopen = () => console.log('WebSocket connected');
        controlWS.onclose = () => console.log('WebSocket disconnected');
        controlWS.onerror = (error) => console.error('WebSocket error', error);

        return () => {
            controlWS.close();
        };
    }, []);

    const sendData = (data: object) => {
        console.log(data)
        if (websocket && websocket.readyState === WebSocket.OPEN) {
            websocket.send(JSON.stringify(data));
        }
    };

    const [gpsData, setGpsData] = React.useState({ latitude: null, longitude: null });

    React.useEffect(() => {
        const gpsWS = new WebSocket(`ws://${READ_IP}`);
        gpsWS.onopen = () => console.log("Connected to GPS WebSocket");

        gpsWS.onmessage = (event) => {
            try {
                const data = JSON.parse(event.data);
                setGpsData({ latitude: data.latitude, longitude: data.longitude });
            } catch (error) {
                console.error("Error parsing GPS data:", error);
            }
        };

        gpsWS.onclose = () => console.log("GPS WebSocket disconnected");

        gpsWS.onerror = (error) => {
            console.error("GPS WebSocket error:", error);
            gpsWS.close();
        };

        return () => gpsWS.close();
    }, []);

    return(
        <View style={style.container}>  
            <WebView 
                source={{uri: `http:${CAMERA_IP}`}}
                originWhitelist={['*']}
                style={style.video}
            />
            <View style={style.joystick}>
                <DPad onDataSend={sendData} />
            </View> 

            <View style={style.brakebtn}>
                <Gear onDataSend={sendData}/>
            </View>

            <View style={style.throttle}>
                <ThrottleControl onDataSend={sendData}/>
            </View>

            <View style={style.gear}>
                <BrakeBtn onDataSend={sendData}/>
            </View>

            <View style={style.dpad}>
                <CamMvt onDataSend={sendData} />
            </View> 

            <View style={style.btns}>
                <Light />
                <MicBtn />
                <PinBtn />
            </View>
            
            <View style={style.details}>
                <GPSTracker gpsData={gpsData} />
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
        right: 45,   
    },
    throttle:{
        position: 'absolute',
        left: 20,
        bottom: 125,
    },
    brakebtn: {
        position: 'absolute',
        bottom: 55,
        left: 225,
    },
    video: {
        width: '100%',
        height: 'auto',
    },
    gear:{
        position: 'absolute',
        bottom: 55,
        right: 175,
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
})