import React, { useState, useEffect } from 'react';
import { StyleSheet, Text, View } from 'react-native';

const GPSTracker = () => {
    const [gpsData, setGpsData] = useState({ latitude: null, longitude: null });
    console.log(gpsData);
    useEffect(() => {
        const socket = new WebSocket('ws://192.168.1.153:6789');

        socket.onopen = () => {
        console.log('Connected to WebSocket server');
        };

        socket.onmessage = (event) => {
        const data = JSON.parse(event.data);
        setGpsData({ latitude: data.latitude, longitude: data.longitude });
        };

        socket.onclose = () => {
        console.log('Disconnected from WebSocket server');
        };

        socket.onerror = (error) => {
        console.error('WebSocket error:', error);
        };

        return () => {
        socket.close();
        };

        
    }, []);

  return (
    <View style={styles.container}>
      <Text style={styles.text}>
        Latitude: {gpsData.latitude ? gpsData.latitude : 'Loading...'}
      </Text>
      <Text style={styles.text}>
        Longitude: {gpsData.longitude ? gpsData.longitude : 'Loading...'}
      </Text>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flexDirection: "row",
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: "rgba(0, 0, 0, 0.5)"
  },
  text: {
    fontSize: 12,
    margin: 10,
    color: "#fff"
  },
});

export default GPSTracker;
