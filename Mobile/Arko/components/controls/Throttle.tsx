import { Ionicons } from '@expo/vector-icons';
import React, { useState, useRef, useEffect } from 'react';
import { View, Text, TouchableOpacity, StyleSheet } from 'react-native';

interface ThrottleControlProps {
    onDataSend: (data: object) => void;
    isBraking: boolean;
}

const ThrottleControl: React.FC<ThrottleControlProps> = ({ onDataSend, isBraking }) => {
    const [power, setPower] = useState(0);
    const throttleInterval = useRef<NodeJS.Timeout | null>(null);

    const sendPowerValue = (currentPower: number) => {
        onDataSend({speed: currentPower})
    };

    useEffect(() => {
        if (isBraking) {
            // Start reducing power while brake is active
            throttleInterval.current = setInterval(() => {
                setPower((prevPower) => {
                    let newPower = prevPower - 10;
                    if (newPower < 0) newPower = 0;
                    return newPower;
                });
            }, 50);
        } else {
            clearInterval(throttleInterval.current!); // Stop reducing power when brake is released
            sendPowerValue(power);
        }

        return () => clearInterval(throttleInterval.current!); // Clean up on component unmount
    }, [isBraking]);

    const handlePress = (isIncrease: boolean) => {
        throttleInterval.current = setInterval(() => {
            setPower((prevPower) => {
                let newPower = isIncrease ? prevPower + 10 : prevPower - 10;
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
        color: "#64C2EC",
        fontFamily: "CeraPro_Bold"
    },
});

export default ThrottleControl;
