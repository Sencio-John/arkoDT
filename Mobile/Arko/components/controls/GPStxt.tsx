import React, { useState, useEffect } from 'react';
import { StyleSheet, Text, View } from 'react-native';

interface GPSProps {
    GPSRead_IP: string;
}

const GPSTracker : React.FC<GPSProps> = ({ GPSRead_IP }) => {
    const [gpsData, setGpsData] = useState({ latitude: null, longitude: null });
    const [socket, setSocket] = useState<WebSocket | null>(null);

    useEffect(() => {
        const ws = new WebSocket(GPSRead_IP);
        setSocket(ws);

        ws.onopen = () => {
            console.log('Connected to WebSocket server');
        };

        ws.onmessage = (event) => {
            try {
                const data = JSON.parse(event.data);
                setGpsData({ latitude: data.latitude, longitude: data.longitude });
            } catch (error) {
                console.error('Error parsing GPS data:', error);
            }
        };

        ws.onclose = () => {
            console.log('Disconnected from WebSocket server');
            setSocket(null);
        };

        ws.onerror = (error) => {
            ws.close();
            setSocket(null); 
        };

        return () => {
            ws.close(); 
        };
    }, [GPSRead_IP]);

    return (
        <View style={styles.container}>
            <Text style={styles.text}>
                Latitude: {gpsData.latitude !== null ? gpsData.latitude : 'Loading...'}
            </Text>
            <Text style={styles.text}>
                Longitude: {gpsData.longitude !== null ? gpsData.longitude : 'Loading...'}
            </Text>
        </View>
    );
};

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
