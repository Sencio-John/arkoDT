import Ionicons from '@expo/vector-icons/Ionicons';
import * as React from 'react';
import { View, PanResponder, StyleSheet, Animated } from 'react-native';

const Joystick = ({ onMove }) => {
  const [position, setPosition] = React.useState({ x: 0, y: 0 });
  const radius = 50; // Radius of joystick container
  const stickRadius = 20; // Radius of the joystick stick
  const maxMoveDistance = radius - stickRadius; // Max movement of the stick
  
  const pan = React.useRef(new Animated.ValueXY()).current;

  const panResponder = React.useRef(
    PanResponder.create({
      onStartShouldSetPanResponder: () => true,
      onMoveShouldSetPanResponder: () => true,
      onPanResponderMove: (e, gestureState) => {
        const { dx, dy } = gestureState;

        // Constrain to up, down, left, and right movement
        let newX = 0;
        let newY = 0;
        
        if (Math.abs(dx) > Math.abs(dy)) {
          // Horizontal movement (left or right)
          newX = Math.max(-maxMoveDistance, Math.min(maxMoveDistance, dx));
          newY = 0;
        } else {
          // Vertical movement (up or down)
          newX = 0;
          newY = Math.max(-maxMoveDistance, Math.min(maxMoveDistance, dy));
        }

        pan.setValue({ x: newX, y: newY });
        setPosition({ x: newX, y: newY });

        // Pass normalized movement data to parent component
        onMove({ x: newX / maxMoveDistance, y: newY / maxMoveDistance });
      },
      onPanResponderRelease: () => {
        // Return joystick to center when released
        Animated.spring(pan, {
          toValue: { x: 0, y: 0 },
          useNativeDriver: false,
        }).start();
        setPosition({ x: 0, y: 0 });
        onMove({ x: 0, y: 0 });
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
    borderRadius: 100,
    backgroundColor: '#ccc',
    justifyContent: 'center',
    alignItems: 'center',
  },
  stick: {
    width: 80,
    height: 80,
    borderRadius: 40,
    backgroundColor: '#333',
  },
});

export default Joystick;
