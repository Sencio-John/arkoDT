import React from 'react';
import { View, Text, StyleSheet, TouchableOpacity, Image} from 'react-native';
import { Ionicons } from '@expo/vector-icons';

interface DeviceConnectedBtnProps {
    device_name?: string;
    device_id?: string;
    onPress?: () => {};
    onDisconnect: () => {};
}

export const DeviceConnectedBtn: React.FC<DeviceConnectedBtnProps> = ({device_name, device_id, onPress, onDisconnect}) => {

    return(
        <TouchableOpacity style={styles.container} onPress={onPress}>
            <View style={styles.iconContainer}>
                <Ionicons name="bluetooth" style={styles.icon} />
            </View>
            <View style={styles.textContainer}>
                <Text style={styles.deviceName}>{device_name}</Text>
                <Text style={styles.deviceId}>{device_id}</Text>
            </View>
            <TouchableOpacity style={styles.disconnectIcon} onPress={onDisconnect}>
                <Image source={require("@/assets/images/icons/link.png")} style={styles.disconnect}/>
            </TouchableOpacity>
        </TouchableOpacity>
    )
}   

const styles = StyleSheet.create({
    container: {
        flexDirection: 'row',
        alignItems: 'center',
        backgroundColor: '#7FF57F',
        borderRadius: 12,
        padding: 16,
        marginVertical: 5,
    },
    iconContainer: {
        marginRight: 15,
    },
    icon: {
        fontSize: 24,
        color: '#067E06',
        
    },
    textContainer: {
        flex: 1,
        
    },
    deviceName: {
        fontSize: 16,
        fontWeight: 'bold',
        fontFamily: "CeraPro_Regular",
        color: '#121212', 
    },
    deviceId: {
        fontSize: 14,
        fontFamily: "CeraPro_Light",
        color: '#121212',
    },
    disconnect:{
        marginLeft: 10,
        width: 24,
        height: 24,
    },
    disconnectIcon:{
        fontSize: 24,
        color: '#D82D2D',
    }

})
