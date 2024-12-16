import React from 'react';
import { View, Text, StyleSheet, TouchableOpacity} from 'react-native';
import { Ionicons } from '@expo/vector-icons';
interface DeviceBtnProps {
    device_name?: string;
    device_id?: string;
    onPress?: () => {};
}

export const DeviceBtn: React.FC<DeviceBtnProps> = ({device_name, device_id, onPress}) => {
    return(
        <TouchableOpacity style={styles.container} onPress={onPress}>
            <View style={styles.iconContainer}>
                <Ionicons name="bluetooth" style={styles.icon}/>
            </View>
            <View style={styles.textContainer}>
                <Text style={styles.deviceName}>{device_name}</Text>
                <Text style={styles.deviceId}>{device_id}</Text>
            </View>
        </TouchableOpacity>
    )
}   

const styles = StyleSheet.create({
    container: {
        flexDirection: 'row',
        alignItems: 'center',
        backgroundColor: '#113547',
        borderRadius: 12,
        padding: 16,
        marginVertical: 5,
    },
    iconContainer: {
        marginRight: 15,
    },
    icon: {
        fontSize: 24,
        color: '#64C2EC',
    },
    textContainer: {
        flex: 1,
        
    },
    deviceName: {
        fontSize: 16,
        fontWeight: 'bold',
        fontFamily: "CeraPro_Regular",
        color: '#B8F5FF', // Black text
    },
    deviceId: {
        fontSize: 14,
        fontFamily: "CeraPro_Light",
        color: '#B8F5FF', // Lime green text
    },
})
