import { Ionicons } from '@expo/vector-icons';
import React, { useState, useRef } from 'react';
import { View, Text, TouchableOpacity, StyleSheet } from 'react-native';
import { CONTROL_IP } from '@/constants/config';

const ThrottleControl = () => {
  const [power, setPower] = useState(0);
  const throttleInterval = useRef(null);
  const [socket, setSocket] = useState<WebSocket | null>(null);

  // Function to send the power value (or log it for now)
  const sendPowerValue = (currentPower: number) => {
    console.log(`Chosen Power: ${currentPower}%`);
    // You can replace this with a function to send the power to your server
  };

  // Function to start increasing or decreasing power
  const handlePress = (isIncrease: boolean) => {
    throttleInterval.current = setInterval(() => {
      setPower((prevPower) => {
        let newPower = isIncrease ? prevPower + 1 : prevPower - 1;
        if (newPower > 100) newPower = 100; // max limit
        if (newPower < 0) newPower = 0; // min limit
        return newPower;
      });
    }, 50); // Change power every 100ms
  };

  // Stop the power adjustment and send the final value
  const handleRelease = () => {
    clearInterval(throttleInterval.current);
    sendPowerValue(power);
      if (socket) {
        const jsonMessage = JSON.stringify({ ctrlSpeed: power });
        socket.send(jsonMessage);
      }
  };


  React.useEffect(() => {
      const ws = new WebSocket(CONTROL_IP);
      setSocket(ws);

      ws.onopen = () => {
          console.log('Connected to the server');
      };

      ws.onmessage = (event) => {
          const data = JSON.parse(event.data);
          // Handle incoming messages if needed
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




  return (
    <View style={styles.container}>
      <View style={styles.buttonsContainer}>
        {/* Increase Button */}
        <TouchableOpacity
          onPressIn={() => handlePress(true)}
          onPressOut={handleRelease}
          style={styles.button}
        >
          <Ionicons name="add" style={styles.icon}/>
        </TouchableOpacity>

        {/* Decrease Button */}
        <TouchableOpacity
          onPressIn={() => handlePress(false)}
          onPressOut={handleRelease}
          style={styles.button}
        >
          <Ionicons name="remove" style={styles.icon}/>
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
    alignSelf: "center"
  },
  icon: {
    fontSize: 24,
    color: '#fff',
    alignSelf: "center",
    alignContent: "center",
    textAlign: "center"
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
