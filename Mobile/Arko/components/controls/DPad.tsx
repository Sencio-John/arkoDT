import { Ionicons } from '@expo/vector-icons';
import React, { useState } from 'react';
import { View, StyleSheet, TouchableOpacity } from 'react-native';

const DPad = ({ onDirectionChange }) => {
  const handlePress = (direction) => {
    onDirectionChange(direction);
  };

  return (
    <View style={styles.dpadContainer}>
      <TouchableOpacity style={[styles.button, styles.up]} onPress={() => handlePress('up')}> 
        <Ionicons style={styles.icon} name="chevron-up" />
      </TouchableOpacity>
      <View style={styles.middleRow}>
        <TouchableOpacity style={[styles.button, styles.left]} onPress={() => handlePress('left')}>
          <Ionicons style={styles.icon} name="chevron-back" />
        </TouchableOpacity>
        <TouchableOpacity style={[styles.button, styles.right]} onPress={() => handlePress('right')}>
          <Ionicons style={styles.icon} name="chevron-forward" />
        </TouchableOpacity>
      </View>
      <TouchableOpacity style={[styles.button, styles.down]} onPress={() => handlePress('down')}>
        <Ionicons style={styles.icon} name="chevron-down" />
      </TouchableOpacity>
    </View>
  );
};

const styles = StyleSheet.create({
  dpadContainer: {
    justifyContent: 'center',
    alignItems: 'center',
  },
  button: {
    padding: 20,
    backgroundColor: '#ccc',
    borderRadius: 10,
    justifyContent: 'center',
    alignItems: 'center',
  },
  middleRow: {
    flexDirection: 'row',
  },
  up: {
    marginBottom: 5,
  },
  down: {
    marginTop: 5,
  },
  left: {
    marginRight: 20,
  },
  right: {
    marginLeft: 20,
  },
  icon:{
    fontSize: 20,
  }
});

export default DPad;