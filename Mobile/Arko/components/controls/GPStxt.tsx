import React from 'react';
import { StyleSheet, Text, View } from 'react-native';

interface GPSProps {
    gpsData: { latitude: number | null; longitude: number | null };
}

const GPSTracker: React.FC<GPSProps> = ({ gpsData }) => (
    <View style={styles.container}>
        <Text style={styles.text}>
            Latitude: {gpsData.latitude !== null ? gpsData.latitude : 'Loading...'}
        </Text>
        <Text style={styles.text}>
            Longitude: {gpsData.longitude !== null ? gpsData.longitude : 'Loading...'}
        </Text>
    </View>
);

const styles = StyleSheet.create({
    container: {
        flexDirection: "row",
        justifyContent: 'center',
        alignItems: 'center',
        backgroundColor: "rgba(0, 0, 0, 0.5)",
        zIndex: 1,
    },
    text: {
        fontSize: 12,
        margin: 10,
        color: "#fff",
    },
});

export default GPSTracker;
