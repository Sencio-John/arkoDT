import * as React from 'react';
import { View, Text, StyleSheet, StatusBar, ActivityIndicator, SafeAreaView } from 'react-native';
import * as ScreenOrientation from 'expo-screen-orientation';
import { WebView } from 'react-native-webview';

import CamMvt from '@/components/controls/CameraMvt';
import { CAMERA_IP, CONTROL_IP, READ_IP, VC_IP } from '@/constants/config';
import ThrottleControl from '@/components/controls/Throttle';
import DPadv2 from '@/components/controls/Dpad_2';
import Brake from '@/components/controls/BrakeBtn';
import Gear from '@/components/controls/GearBtn';
import Light from '@/components/controls/Light';
import GPSTracker from '@/components/controls/GPStxt';

interface FetchResponse {
    ok: boolean;
}

export default function Controller() {
    const [joystickPosition, setJoystickPosition] = React.useState({ x: 0, y: 0 });
    const [direction, setDirection] = React.useState('');

    const handleJoystickMove = (position: React.SetStateAction<{ x: number; y: number; }>) => {
        setJoystickPosition(position);
    };

    const handleDirectionChange = (newDirection: React.SetStateAction<string>) => {
        setDirection(newDirection);
    };

    React.useEffect(() => {

        checkIP();
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

    const [isOverlayVisible, setIsOverlayVisible] = React.useState(false);
    const [isLoading, setIsLoading] = React.useState(true)

    const fetchWithTimeout = (url: string, timeout = 3000) => {
        return Promise.race([
            fetch(url, { method: 'HEAD' }),
            new Promise((_, reject) =>
                setTimeout(() => reject(new Error(`Request to ${url} timed out`)), timeout)
            ),
        ]);
    };

    const checkIP = async () => {
        try {
            const responses = await Promise.all([
                fetchWithTimeout(CAMERA_IP, 3000),
                fetchWithTimeout(CONTROL_IP, 3000),
                fetchWithTimeout(READ_IP, 3000),
                fetchWithTimeout(VC_IP, 3000),
            ]);

            const [cameraResponse, controlResponse, readResponse, vcResponse] = responses as FetchResponse[];;

            if (!cameraResponse.ok || !controlResponse.ok || !readResponse.ok || !vcResponse.ok) {
                setIsOverlayVisible(true);
            }
        } catch (error) {
            console.log("IP check failed:", error);
            setIsOverlayVisible(true);
        } finally {
            setIsLoading(false);
        }
    };



    if (isLoading) {
        return (
            <SafeAreaView style={style.loadingContainer}>
                <ActivityIndicator size="large" color="#0000ff" />
                <Text>Loading...</Text>
            </SafeAreaView>
        );
    }

    return(
        <SafeAreaView style={style.container}>
            {/* {isOverlayVisible && (
                <View style={style.overlay}>
                    <Text style={style.errorText}>Connection is unavailable. Please try again.</Text>
                </View>
            )} */}
            <View>
                <WebView />
            </View>

            <View style={style.joystick}>
                <DPadv2 address={CONTROL_IP} />
            </View> 

            <View style={style.brakebtn}>
                <Gear address={CONTROL_IP}/>
                
            </View>

            <View style={style.throttle}>
                <ThrottleControl address={CONTROL_IP} />
            </View>
           <View style={style.gear}>
                <Brake />
            </View>

            <View style={style.dpad}>
                <CamMvt onMove={handleJoystickMove} address={CONTROL_IP}/>
            </View> 
            
            <View style={style.btns}>
                <Light />
            </View>
            
            <View style={style.details}>
                <GPSTracker GPSRead_IP={READ_IP} />
            </View>
        </SafeAreaView>
    );
}


const style = StyleSheet.create({
    container:{
        flex: 1,
    },
    loadingContainer: {
        flex: 1,
        justifyContent: 'center',
        alignItems: 'center',
        backgroundColor: '#ffffff',
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
    },
    overlay:{
        ...StyleSheet.absoluteFillObject,
        backgroundColor: '#212121',
        justifyContent: "center",
        alignItems: "center",
        zIndex: 9999,
    },
})