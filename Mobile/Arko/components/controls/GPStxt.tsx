import React from 'react';
import { StyleSheet, Text, View } from 'react-native';

interface GPSProps {
    gpsData: { latitude: number | null; longitude: number | null };
    waterLvl: number,
    infrared: string,
}

const Readings: React.FC<GPSProps> = ({ gpsData, waterLvl, infrared }) => (
    <View style={styles.container}>
        <Text style={styles.text}>
            Latitude: {gpsData.latitude !== null ? gpsData.latitude : 'Connecting...'}
        </Text>
        <Text style={styles.text}>
            Longitude: {gpsData.longitude !== null ? gpsData.longitude : 'Connecting...'}
        </Text>
        <Text style={styles.text}>
            Water Level: {waterLvl !== null ? waterLvl : 'Connecting...'}
        </Text>
        <Text style={styles.text}>
            Infrared: {infrared !== null ? infrared : 'Connecting...'}
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
        fontFamily: "CeraPro_Bold"
    },
});

export default Readings;
