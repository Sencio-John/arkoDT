import React, { useState, useEffect } from 'react';
import { View, TouchableOpacity, StyleSheet } from 'react-native';
import { ThemedText } from '../ThemedText';
import { Ionicons } from '@expo/vector-icons';
import WebSocketManager from '@/constants/controlsocket';

interface DPadv2Props {
    address: string;
}

const DPadv2: React.FC<DPadv2Props> = ({ address }) => {
    const [isHoldingLeft, setIsHoldingLeft] = useState(false);
    const [isHoldingRight, setIsHoldingRight] = useState(false);
    const wsManager = WebSocketManager.getInstance();

    useEffect(() => {
        wsManager.connect(address);

        return () => {
            wsManager.disconnect(address);
        };
    }, [address]);

    useEffect(() => {
        let interval: NodeJS.Timeout | null = null;

        const sendMessage = (direction: string) => {
            wsManager.sendMessage(address, { rudder: direction });
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
    }, [isHoldingLeft, isHoldingRight, address]); 

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
