import React, { useState, useEffect } from 'react';
import { View, TouchableOpacity, StyleSheet } from 'react-native';
import { ThemedText } from '../ThemedText';
import { Ionicons } from '@expo/vector-icons';

interface DPadv2Props {
    IP: string; 
}

const DPadv2: React.FC<DPadv2Props> = ({ IP }) => {
    const [isHoldingLeft, setIsHoldingLeft] = useState(false);
    const [isHoldingRight, setIsHoldingRight] = useState(false);
    const [socket, setSocket] = useState<WebSocket | null>(null);

    useEffect(() => {
        const ws = new WebSocket(IP);

        ws.onopen = () => {
            console.log('Connected to the server');
            setSocket(ws);
        };

        ws.onclose = () => {
            console.log('Disconnected from the server');
            setSocket(null);
        };

        ws.onerror = (error) => {
            console.error('WebSocket erro at DPAD:', error);
            ws.close();
            setSocket(null);
        };

        return () => {
            ws.close();
        };
    }, [IP]);

    useEffect(() => {
        let interval: NodeJS.Timeout | null = null;

        const sendMessage = (direction: string) => {
            if (socket) {
                const jsonMessage = JSON.stringify({ ctrlRudder: direction });
                socket.send(jsonMessage);
            }
        };

        if (isHoldingLeft) {
            interval = setInterval(() => sendMessage('Left'), 150);
        } else if (isHoldingRight) {
            interval = setInterval(() => sendMessage('Right'), 150);
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
                onPressOut={() => setIsHoldingLeft(false)}
            >
                <ThemedText>
                    <Ionicons style={styles.icon} name='chevron-back' />
                </ThemedText>
            </TouchableOpacity>
            <TouchableOpacity
                style={styles.button}
                onPressIn={() => setIsHoldingRight(true)}
                onPressOut={() => setIsHoldingRight(false)}
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
