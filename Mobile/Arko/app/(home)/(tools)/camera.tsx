import * as React from 'react';
import { View, Text, StyleSheet, StatusBar } from 'react-native';
import * as ScreenOrientation from 'expo-screen-orientation';
import { WebView } from 'react-native-webview';

import { CAMERA_IP } from '@/constants/config';

export default function Camera() {


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
            // Unlock orientation when leaving the screen
            revertBack();
        };
    }, []);

    return (
        <View style={style.container}>
            <WebView
                source={{ uri: `http://${CAMERA_IP}` }}
                style={style.video}

            />
            
        </View>
    );
}


const style = StyleSheet.create({
    container:{
        flex: 1,
    },
    video: {
        width: '100%',
        height: 'auto',
    },

})