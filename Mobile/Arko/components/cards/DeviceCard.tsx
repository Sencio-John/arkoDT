import React from 'react';
import { View, Text, StyleSheet, TouchableOpacity} from 'react-native';
import { ThemedText } from '../ThemedText';
import { ThemedView } from '../ThemedView';
import { Ionicons } from '@expo/vector-icons';

import Status from './Status';

const DeviceStatusCard = ({onPress = () => {}}) => {
  return (
    <ThemedView style={styles.card}>
        <View style={styles.header}>
            <View style={styles.left}>
                <View style={styles.dot} />
                <Text style={styles.deviceStatusText}>Device Status</Text>
            </View>
            <View style={styles.right}>
                <TouchableOpacity onPress={onPress}>
                    <ThemedText type='title'>
                        <Ionicons style={styles.icons} name="settings-outline"/>
                    </ThemedText>
                </TouchableOpacity>
            </View>
            {/* Icon placeholder */}
            
        </View>
        <View style={styles.body}>
            <ThemedText style={styles.deviceIdText}>XXXXXXX</ThemedText>
            <Status type="default" status="Add Device"/>
        </View>
    </ThemedView>
  );
};

const styles = StyleSheet.create({
    card: {
        borderWidth: 1,
        borderColor: '#ddd', // Purple border color like in your image
        padding: 20,
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
        fontSize: 16,
        fontWeight: 'bold',
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
        fontSize: 24,
        fontWeight: 'bold',
    },
    icons:{
        fontSize: 24,
    }
});

export default DeviceStatusCard;
