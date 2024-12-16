import React from 'react';
import { View, Text, StyleSheet, ScrollView } from 'react-native';

const WaterLevelChart = ({ data }: { data: { timestamp: string; level: number }[] }) => {
  // Max and min values for the water level
  const maxLevel = 3; // 3 meters
  const minLevel = 0; // 0 meters
  
  // Helper function to calculate the height of the bar relative to the max level
  const getBarHeight = (level: number) => {
    return ((level - minLevel) / (maxLevel - minLevel)) * 200; // 200px as max height for the bar
  };

  return (
    <View style={styles.container}>
      <ScrollView horizontal contentContainerStyle={styles.chart}>
        {data.map((entry, index) => (
          <View key={index} style={styles.barContainer}>
            <View
              style={[
                styles.bar,
                { height: getBarHeight(entry.level) },
              ]}
            />
            <Text style={styles.timestamp}>{entry.timestamp}</Text>
          </View>
        ))}
      </ScrollView>
      <View style={styles.yAxis}>
        {Array.from({ length: maxLevel + 1 }, (_, i) => (
          <Text key={i} style={styles.yAxisText}>{`${maxLevel - i}m`}</Text>
        ))}
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flexDirection: 'row',
    alignItems: 'flex-end',
    marginTop: 20,
  },
  chart: {
    flexDirection: 'row',
    alignItems: 'flex-end',
  },
  barContainer: {
    width: 40,
    justifyContent: 'flex-end',
    alignItems: 'center',
    marginHorizontal: 10,
  },
  bar: {
    width: 30,
    backgroundColor: '#4CAF50', // Green for water level
    borderRadius: 5,
  },
  timestamp: {
    marginTop: 5,
    fontSize: 10,
    textAlign: 'center',
  },
  yAxis: {
    position: 'absolute',
    top: 20,
    left: 0,
    justifyContent: 'space-between',
    height: 200,
    paddingVertical: 10,
  },
  yAxisText: {
    fontSize: 12,
    color: '#000',
    textAlign: 'right',
  },
});

export default WaterLevelChart;
