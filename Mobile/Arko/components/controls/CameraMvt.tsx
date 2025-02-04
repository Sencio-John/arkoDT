import React, { useState, useRef } from 'react';
import { View, PanResponder, StyleSheet, Animated } from 'react-native';

interface CamMvtProps {
    onDataSend: (data: object) => void;
}

const CamMvt: React.FC<CamMvtProps> = ({ onDataSend }) => {
    const [horizontalAngle, setHorizontalAngle] = useState(90);
    const [verticalAngle, setVerticalAngle] = useState(90); 
    const radius = 50;
    const stickRadius = 20; 
    const maxMoveDistance = radius - stickRadius;

    const pan = useRef(new Animated.ValueXY()).current;

    const sendJoystickData = (horizontal: number, vertical: number) => {
        onDataSend({ horizontal, vertical });
    };

    const panResponder = useRef(
        PanResponder.create({
            onStartShouldSetPanResponder: () => true,
            onMoveShouldSetPanResponder: () => true,
            onPanResponderMove: (e, gestureState) => {
                const { dx, dy } = gestureState;
                let newX = 0, newY = 0;

                if (Math.abs(dx) > Math.abs(dy)) {
                    newX = Math.max(-maxMoveDistance, Math.min(maxMoveDistance, dx));
                    newY = 0;

                    if (newX > 0) {
                        setHorizontalAngle(prevAngle => Math.min(180, prevAngle + 5));
                    } else {
                        setHorizontalAngle(prevAngle => Math.max(0, prevAngle - 5));
                    }
                } else {
                    newX = 0;
                    newY = Math.max(-maxMoveDistance, Math.min(maxMoveDistance, dy));

                    if (dy < 0) {
                        setVerticalAngle(prevAngle => Math.min(120, prevAngle + 5)); 
                    } else {
                        setVerticalAngle(prevAngle => Math.max(90, prevAngle - 5));
                    }
                }

                pan.setValue({ x: newX, y: newY });
                sendJoystickData(horizontalAngle, verticalAngle);
            },
            onPanResponderRelease: () => {
                Animated.spring(pan, {
                    toValue: { x: 0, y: 0 },
                    useNativeDriver: false,
                }).start();
                sendJoystickData(horizontalAngle, verticalAngle);
            },
        })
    ).current;

    return (
        <View style={styles.joystickContainer}>
            <View style={styles.base}>
                <Animated.View
                    {...panResponder.panHandlers}
                    style={[pan.getLayout(), styles.stick]}
                />
            </View>
        </View>
    );
};

const styles = StyleSheet.create({
    joystickContainer: {
        justifyContent: 'center',
        alignItems: 'center',
        marginTop: 50,
    },
    base: {
        width: 100,
        height: 100,
        borderRadius: 50,
        backgroundColor: '#ccc',
        justifyContent: 'center',
        alignItems: 'center',
    },
    stick: {
        width: 40,
        height: 40,
        borderRadius: 20,
        backgroundColor: '#333',
    },
});

export default CamMvt;
