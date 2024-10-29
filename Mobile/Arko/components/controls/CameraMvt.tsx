import Ionicons from '@expo/vector-icons/Ionicons';
import React, { useState, useRef, useEffect } from 'react';
import { View, Text, PanResponder, StyleSheet, Animated } from 'react-native';

const CamMvt = ({ onMove, wsURL }) => {
  
    const [position, setPosition] = useState({ x: 0, y: 0 });
    const [horizontalAngle, setHorizontalAngle] = useState(90);
    const [verticalAngle, setVerticalAngle] = useState(90);

    const radius = 50;
    const stickRadius = 20; 
    const maxMoveDistance = radius - stickRadius;

    
    const [socket, setSocket] = useState(null);

    // PanResponder & Animated configurations
    const pan = useRef(new Animated.ValueXY()).current;
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
                const newHorizontalAngle = Math.min(135, Math.max(45, 90 + Math.sign(dx) * 5));
                setHorizontalAngle(newHorizontalAngle);
            } else {

                newX = 0;
                newY = Math.max(-maxMoveDistance, Math.min(maxMoveDistance, dy));
                const newVerticalAngle = Math.min(120, Math.max(60, 90 + Math.sign(dy) * 5));
                setVerticalAngle(newVerticalAngle);
            }


        pan.setValue({ x: newX, y: newY });
        setPosition({ x: newX, y: newY });

        onMove({ x: newX / maxMoveDistance, y: newY / maxMoveDistance });
        
        sendJoystickData({ horizontal: horizontalAngle, vertical: verticalAngle });
    },
        onPanResponderRelease: () => {
            Animated.spring(pan, {
            toValue: { x: 0, y: 0 },
            useNativeDriver: false,
            }).start();
            setPosition({ x: 0, y: 0 });
            setHorizontalAngle(90);
            setVerticalAngle(90);
            onMove({ x: 0, y: 0 });
            sendJoystickData({ horizontal: 90, vertical: 90 });
        },
    })
    ).current;


    useEffect(() => {
        const ws = new WebSocket(wsURL);
        setSocket(ws);

        ws.onopen = () => console.log('Connected to WebSocket server');
        ws.onclose = () => console.log('Disconnected from WebSocket server');
        
        return () => ws.close();
    }, [wsURL]);


    const sendJoystickData = (angles: { horizontal: number; vertical: number; }) => {
        if (socket && socket.readyState === WebSocket.OPEN) {
        const message = JSON.stringify(angles);
        socket.send(message);
        }
    };

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
    angleDisplay: {
        marginTop: 15,
        alignItems: 'center',
    },
    angleText: {
        fontSize: 16,
        color: "#000",
    },
});

export default CamMvt;
