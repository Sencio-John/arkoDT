import React, { useState, useEffect } from 'react';
import { View, TouchableOpacity, StyleSheet } from 'react-native';
import { ThemedText } from '../ThemedText';
import { Ionicons } from '@expo/vector-icons';

interface DPadv2Props {
    onDataSend: (data: object) => void;
}

const DPadv2: React.FC<DPadv2Props> = ({ onDataSend }) => {
    const [isHoldingLeft, setIsHoldingLeft] = useState(false);
    const [isHoldingRight, setIsHoldingRight] = useState(false);

    const handleDirection = (direction: 'left' | 'right') => {
        onDataSend({ rudder: direction });
    };


    return (
        <View style={styles.container}>
            <TouchableOpacity
                style={styles.button}
                onPressIn={() => {
                    setIsHoldingLeft(true);
                    handleDirection('left');
                }}
                onPressOut={() => setIsHoldingLeft(false)}
            >
                <ThemedText>
                    <Ionicons style={styles.icon} name='chevron-back' />
                </ThemedText>
            </TouchableOpacity>
            <TouchableOpacity
                style={styles.button}
                onPressIn={() => {
                    setIsHoldingRight(true);
                    handleDirection('right');
                }}
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
