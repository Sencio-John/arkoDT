import * as React from 'react';
import { View, Text, StyleSheet, StatusBar, ActivityIndicator } from 'react-native';
import * as ScreenOrientation from 'expo-screen-orientation';
import { WebView } from 'react-native-webview';

import Joystick from '../../../components/controls/Joystick';
import { CAMERA_IP } from '@/constants/config';
import ThrottleControl from '@/components/controls/Throttle';
import DPadv2 from '@/components/controls/Dpad_2';
import Brake from '@/components/controls/BrakeBtn';
import Gear from '@/components/controls/GearBtn';
import Light from '@/components/controls/Light';
import GPSTracker from '@/components/controls/GPStxt';


export default function Controller() {

    const [joystickPosition, setJoystickPosition] = React.useState({ x: 0, y: 0 });
    const [direction, setDirection] = React.useState('');
    const [cameraReachable, setCameraReachable] = React.useState<boolean | null>(null);


    const handleJoystickMove = (position: React.SetStateAction<{ x: number; y: number; }>) => {
        setJoystickPosition(position);
    };

    const handleDirectionChange = (newDirection: React.SetStateAction<string>) => {
        setDirection(newDirection);
    };

    React.useEffect(() => {
        const lockOrientation = async () => {
            await ScreenOrientation.lockAsync(ScreenOrientation.OrientationLock.LANDSCAPE);
            StatusBar.setHidden(true);
        };


        const checkCameraReachable = async () => {
            try {
                const response = await fetch(`http://${CAMERA_IP}`);
                if (response.ok) {
                    setCameraReachable(true);
                } else {
                    setCameraReachable(false);
                }
            } catch (error) {
                console.log('Camera unreachable:', error);
                setCameraReachable(false);
            }
        };

        lockOrientation();
        checkCameraReachable();
        const revertBack = async () =>{
            await ScreenOrientation.lockAsync(ScreenOrientation.OrientationLock.PORTRAIT);
            StatusBar.setHidden(false);
        }   

        return () => {
            // Unlock orientation when leaving the screen
            revertBack();
        };
    }, []);

    return (
        <View style={style.container}>
            {cameraReachable === null ? (
                <ActivityIndicator size="large" color="#0000ff" /> // Loading spinner while checking
            ) : cameraReachable ? (
                <WebView
                    source={{ uri: `http://${CAMERA_IP}` }}
                    style={style.video}
                />
            ) : (
                <Text style={style.errorText}>Camera feed is unreachable.</Text> // Error message if unreachable
            )}

            <View style={style.joystick}>
                <DPadv2 />
            </View> 

            <View style={style.brakebtn}>
                <Gear />
                
            </View>

            <View style={style.throttle}>
                <ThrottleControl />
            </View>

            <View style={style.gear}>
                <Brake />
            </View>

            <View style={style.dpad}>
                <Joystick onMove={handleJoystickMove}/>
            </View> 
            
            <View style={style.btns}>
                <Light />
            </View>
            
            <View style={style.details}>
                <GPSTracker />
            </View>
        </View>
    );
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
    },
    details:{
        position: "absolute",
    },
    errorText: {
        color: 'red',
        fontSize: 18,
        textAlign: 'center',
        marginTop: 20,
        justifyContent: "center",
        alignContent: "center",
        alignItems: "center"
    }

})