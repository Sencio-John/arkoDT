import { Ionicons } from '@expo/vector-icons';
import React, { useState, useRef, useEffect } from 'react';
import { View, Text, TouchableOpacity, StyleSheet } from 'react-native';

interface ThrottleControlProps {
    IP: string; 
}

const ThrottleControl: React.FC<ThrottleControlProps> = ({ IP }) => {
    const [power, setPower] = useState(0);
    const throttleInterval = useRef<NodeJS.Timeout | null>(null);
    const [socket, setSocket] = useState<WebSocket | null>(null);

    const sendPowerValue = (currentPower: number) => {
        console.log(`Chosen Power: ${currentPower}%`);
        if (socket) {
            const jsonMessage = JSON.stringify({ ctrlSpeed: currentPower });
            socket.send(jsonMessage);
        }
    };

    const handlePress = (isIncrease: boolean) => {
        throttleInterval.current = setInterval(() => {
            setPower((prevPower) => {
                let newPower = isIncrease ? prevPower + 1 : prevPower - 1;
                if (newPower > 100) newPower = 100; 
                if (newPower < 0) newPower = 0; 
                return newPower;
            });
        }, 50); 
    };

    const handleRelease = () => {
        clearInterval(throttleInterval.current!);
        sendPowerValue(power);
    };

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
            ws.close(); 
            setSocket(null);
        };

        return () => {
            ws.close();
        };
    }, [IP]); 

    return (
        <View style={styles.container}>
            <View style={styles.buttonsContainer}>
                {/* Increase Button */}
                <TouchableOpacity
                    onPressIn={() => handlePress(true)}
                    onPressOut={handleRelease}
                    style={styles.button}
                >
                    <Ionicons name="add" style={styles.icon} />
                </TouchableOpacity>

                {/* Decrease Button */}
                <TouchableOpacity
                    onPressIn={() => handlePress(false)}
                    onPressOut={handleRelease}
                    style={styles.button}
                >
                    <Ionicons name="remove" style={styles.icon} />
                </TouchableOpacity>
            </View>

            {/* Power Display */}
            <View style={styles.powerDisplay}>
                <Text style={styles.powerText}>{power}%</Text>
            </View>
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
    buttonsContainer: {
        justifyContent: 'center',
        alignItems: 'center',
    },
    button: {
        width: 40,
        height: 40,
        backgroundColor: '#277CA5',
        justifyContent: 'center',
        alignItems: 'center',
        borderRadius: 30,
        marginVertical: 10,
        alignSelf: "center",
    },
    icon: {
        fontSize: 24,
        color: '#fff',
    },
    powerDisplay: {
        marginLeft: 20,
        justifyContent: 'center',
        alignItems: 'center',
    },
    powerText: {
        fontSize: 24,
        fontWeight: 'bold',
    },
});

export default ThrottleControl;
