import * as React from 'react';
import { View, Text, StyleSheet, StatusBar, SafeAreaView, ActivityIndicator } from 'react-native';
import * as ScreenOrientation from 'expo-screen-orientation';

import CamMvt from '@/components/controls/CameraMvt';
import { CAMERA_IP, CONTROL_IP, READ_IP, VC_IP } from '@/constants/config';
import ThrottleControl from '@/components/controls/Throttle';
import DPad from '@/components/controls/Dpad';
import BrakeBtn from '@/components/controls/BrakeBtn';
import Gear from '@/components/controls/GearBtn';
import Light from '@/components/controls/Light';
import GPSTracker from '@/components/controls/GPStxt';
import MicBtn from '@/components/controls/Mic';
import PinBtn from '@/components/controls/Pinbtn';


export default function Controller() {

    //landscape
    React.useEffect(() => {

        const lockOrientation = async () => {
            await ScreenOrientation.lockAsync(ScreenOrientation.OrientationLock.LANDSCAPE);
            StatusBar.setHidden(true);
        };

        lockOrientation();
        
        const revertBack = async () =>{
            await ScreenOrientation.lockAsync(ScreenOrientation.OrientationLock.PORTRAIT);
            StatusBar.setHidden(false);
        }   

        return () => {
            revertBack();
        };
    }, []);

    
    const sendData = (data: object) => {
        console.log(data)
        // if (websocket && websocket.readyState === WebSocket.OPEN) {
        //     websocket.send(JSON.stringify(data));
        // }
    };

    return(
        <SafeAreaView style={style.container}>

            {/* <View style={style.joystick}>
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
            </View>  */}

            <View style={style.btns}>
                <Light />
                <MicBtn />
                <PinBtn />
            </View>
            
            <View style={style.details}>
                <GPSTracker GPSRead_IP={READ_IP} />
            </View>
        </SafeAreaView>
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