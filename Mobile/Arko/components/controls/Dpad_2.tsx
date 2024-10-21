import React, { useState, useEffect } from 'react';
import { View, Text, TouchableOpacity, StyleSheet } from 'react-native';
import { ThemedText } from '../ThemedText';
import { Ionicons } from '@expo/vector-icons';

import { WEBSOCKET_IP } from '@/constants/config';

interface Message {
    message: string;
}

const DPadv2 = () => {
    const [isHoldingLeft, setIsHoldingLeft] = useState(false);
    const [isHoldingRight, setIsHoldingRight] = useState(false);

    const [socket, setSocket] = useState<WebSocket | null>(null);
    const [message, setMessage] = useState<string>('');
    const [output, setOutput] = useState<string[]>([]);

    useEffect(() => {
        const ws = new WebSocket(WEBSOCKET_IP);
        setSocket(ws);

        ws.onopen = () => {
            console.log('Connected to the server');
        };

        ws.onmessage = (event) => {
            const data = JSON.parse(event.data);
            // Handle incoming messages if needed
            console.log('Received:', data.message);
        };

        ws.onclose = () => {
            console.log('Disconnected from the server');
        };

        ws.onerror = (error) => {
            console.error('WebSocket error:', error);
        };

        return () => {
            ws.close();
        };
    }, []);

    useEffect(() => {
        let interval: NodeJS.Timeout | null = null;

        if (isHoldingLeft || isHoldingRight) {
            interval = setInterval(() => {
                const direction = isHoldingLeft ? "Left" : "Right";
                console.log(`Holding ${direction}`);

                if (socket) {
                    const jsonMessage = JSON.stringify({ ctrlRudder: direction });
                    socket.send(jsonMessage);
                }
            }, 150); // Sending interval
        }

        return () => {
            if (interval) {
                clearInterval(interval);
            }
        };
    }, [isHoldingLeft, isHoldingRight, socket]);


    return (
        <View style={styles.container}>
        <TouchableOpacity
            style={styles.button}
            onPressIn={() => setIsHoldingLeft(true)}
            onPressOut={() => {
            setIsHoldingLeft(false);
            }}
        >
            <ThemedText>
                <Ionicons style={styles.icon} name='chevron-back' />
            </ThemedText>
        </TouchableOpacity>
        <TouchableOpacity
            style={styles.button}
            onPressIn={() => setIsHoldingRight(true)}
            onPressOut={() => {
            setIsHoldingRight(false);
            }}
        >
            <ThemedText>
                <Ionicons style={styles.icon} name='chevron-forward' />
            </ThemedText>
        </TouchableOpacity>
        </View>
    );
};

const styles = StyleSheet.create({
    container: {
        flexDirection: 'row',
        justifyContent: 'center',
        alignItems: 'center',
        marginTop: 50,
    },
    button: {
        backgroundColor: '#277CA5',
        padding: 20,
        borderRadius: 10,
        margin: 10,
    },
    icon: {
        fontSize: 24,
        color: "#fff"
    },
    });

export default DPadv2;