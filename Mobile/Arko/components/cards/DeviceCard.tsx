import React from 'react';
import { View, Text, StyleSheet, TouchableOpacity} from 'react-native';
import { ThemedText } from '../ThemedText';
import { ThemedView } from '../ThemedView';
import { Ionicons } from '@expo/vector-icons';

import Status from './Status';
import { onDisconnect } from 'firebase/database';

interface DeviceStatusCardProps {
  deviceName?: string;
  ssid?: string, 
  deviceStatus: 'connected' | 'disconnected' | 'none';
  onPress: () => void;
  onDisconnect: () => void;
}

const DeviceStatusCard: React.FC<DeviceStatusCardProps> = ({
    deviceStatus,
    deviceName,
    ssid,
    onPress = () => {},
    onDisconnect = () => {}
}) => {

    
    const deviceStatusLimit = 23;
    const deviceNameLimit = 15;

    const getStatusText = () => {
        if (!ssid) return 'Connect to a network';
        if (ssid) return `Connected: ${ssid}`;
        if (deviceStatus === 'disconnected') return 'Disconnected';
        return 'Connect to device';
    };



    const getStatusType = () => {
        if (deviceStatus === 'connected') return { type: 'green', status: 'Connected' };
        if (deviceStatus === 'disconnected') return { type: 'red', status: 'Disconnected' };
        return { type: 'default', status: 'Add Device' };
    };


    const getDotColor = () => {
        if (deviceStatus === 'connected') return '#31994B'; // Green
        if (deviceStatus === 'disconnected') return '#993131'; // Red
        return '#D9D9D9'; // Default gray
    };


    return (
        <TouchableOpacity style={styles.card} onPress={onPress}>
            <View style={styles.header}>
                <View style={styles.left}>
                    <View style={[styles.dot, { backgroundColor: getDotColor() }]} />
                    <ThemedText style={styles.deviceStatusText}>
                        {getStatusText().length > deviceStatusLimit
                        ? `${getStatusText().substring(0, deviceStatusLimit)}...`
                        : getStatusText()}
                    </ThemedText>
                </View>
                <View style={styles.right}>
                    {deviceName && deviceStatus ? <TouchableOpacity style={styles.reconBtn} onPress={onDisconnect}>
                        <View style={styles.reconContent}>
                            <ThemedText style={styles.recon}>Disconnect </ThemedText>
                            <ThemedText>
                                <Ionicons style={[styles.icons, styles.disconnect]} name="power-outline"/>
                            </ThemedText>
                        </View>
                    </TouchableOpacity> : null}
                </View>
                {/* Icon placeholder */}
                
            </View>
            <View style={styles.body}>
                <ThemedText style={styles.deviceIdText}>
                    {!deviceName
                    ? "No Device Found"
                    : deviceName.length > deviceNameLimit
                        ? `${deviceName.substring(0, deviceNameLimit)}...`
                        : deviceName}
                </ThemedText>
                <Status type={getStatusType().type} status={getStatusType().status} />
            </View>
        </TouchableOpacity>
    );
};

const styles = StyleSheet.create({
    card: {
        borderWidth: 1,
        borderColor: '#ddd', // Purple border color like in your image
        padding: 16,
        borderRadius: 8,
    },
    header: {
        flexDirection: 'row',
        justifyContent: 'space-between',
        alignItems: 'center',
    },
    left:{
        flexDirection: 'row',
        alignItems: "center"
    },
    right:{
        flexDirection: 'row',
    },
    deviceStatusText: {
        color: '#277CA5', // Blue text color
        fontSize: 14, 
        fontFamily: "CeraPro_Bold"
    },
    dot: {
        width: 6,
        height: 6,
        backgroundColor: '#D9D9D9', // Green dot
        borderRadius: 3,
        marginRight: 5,
    },
    icon: {
        width: 20,
        height: 20,
        backgroundColor: '#000', // Placeholder for icon
        borderRadius: 10,
    },
    body: {
        marginTop: 10,
        flexDirection: 'row',
        justifyContent: 'space-between',
        alignItems: 'center',
    },
    deviceIdText: {
        fontSize: 20,
        fontFamily: "CeraPro_Bold",
        marginLeft: 10,
    },
    icons:{
        fontSize: 16,
    },
    
    reconBtn:{
        flexDirection: 'row',
        alignItems: 'center',
        justifyContent: 'center',
    },
    reconContent:{
        flexDirection: 'row',
        alignItems: 'center',
    },
    recon:{
        fontSize: 12,
        color: "red"
    },
    disconnect:{
        color: "red"
    }
});

export default DeviceStatusCard;
