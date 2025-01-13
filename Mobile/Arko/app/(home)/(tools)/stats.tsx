import * as React from 'react';
import { View, Text, StyleSheet, Dimensions, useColorScheme, Alert } from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';
import {LineChart} from 'react-native-chart-kit';
import { ThemedText } from '@/components/ThemedText';
import { ThemedView } from '@/components/ThemedView';
import AsyncStorage from '@react-native-async-storage/async-storage';

export default function Stats() {
    
    const colorScheme = useColorScheme();
    const screenWidth = Dimensions.get('window').width;
    const [IPAddress, setIPAddress] = React.useState(null)
    const [chartData, setChartData] = React.useState({
        labels: ["10:00PM"], // Time labels
        datasets: [
            {
                data: [0, 0.5, 1, 3, 2.5, 1.5, 0.5], // Water level data
                color: (opacity = 1) => colorScheme === 'dark' ? '#277CA5' : '#113547', // Line color
                strokeWidth: 2, // Line thickness
            }
        ],
    });

    
    const formatTime = () => {
        const now = new Date();
        return `${now.getHours().toString().padStart(2, '0')}:${now.getMinutes().toString().padStart(2, '0')}`;
    };

    const initialize = async () =>{
        const ip = await AsyncStorage.getItem("IPAddress");
        setIPAddress(ip)
    }

    const chartConfig = {
        backgroundGradientFrom: colorScheme === 'dark' ? '#1E1E1E' : '#FFFFFF',
        backgroundGradientTo: colorScheme === 'dark' ? '#1E1E1E' : '#FFFFFF',
        color: (opacity = 1) => colorScheme === 'dark' ? '#277CA5' : '#113547',
        strokeWidth: 2, 
        barPercentage: 0.5,
        useShadowColorFromDataset: false // optional
    };

    React.useEffect(() => {
        initialize(); // Fetch IP address on component mount
    }, []);

    React.useEffect(() => {

        if(IPAddress){
            const ws = new WebSocket(`ws://${IPAddress}:5000`);
            ws.onopen = () => {
                console.log("Device Connected")
            }
            ws.onclose = () =>{
                console.log("Device Disconnected")
            }

            ws.onmessage = (event) => {
                try {
                    const data = JSON.parse(event.data); // Parse incoming WebSocket data
                    const waterLevel = data.water_level; // Extract water_level
                    if (waterLevel !== undefined) {
                        // Update chart data
                        setChartData((prevData) => {
                            const updatedLabels = [...prevData.labels, formatTime()].slice(-10); // Keep last 10 labels
                            const updatedData = [...prevData.datasets[0].data, waterLevel].slice(-10); // Keep last 10 data points

                            return {
                                labels: updatedLabels,
                                datasets: [
                                    {
                                        ...prevData.datasets[0],
                                        data: updatedData,
                                    },
                                ],
                            };
                        });
                    }
                } catch (error) {
                    console.error('Error parsing WebSocket data:', error);
                }
            };

            return () => ws.close();
        } else{
            Alert.alert("Error", "Device is offline, cannot be detect real time water level")
        }

    }, [IPAddress])

    return (
        <SafeAreaView style={[style.container, {backgroundColor: colorScheme === 'dark' ? '#151718' : '#fff'}] }>
            <View>
                <ThemedText style={style.header}>Water Level Graph</ThemedText>
                <LineChart
                    data={chartData}
                    width={screenWidth}
                    height={250}
                    chartConfig={chartConfig}
                    fromZero 
                    yAxisSuffix=" m"
                    yAxisInterval={0.5}
                />
            </View>
        </SafeAreaView>
    );
}


const style = StyleSheet.create({
    container:{
        flex: 1,
        backgroundColor: "white",
    },
    header:{
        marginHorizontal: 10,
    },
    chart: {
        marginVertical: 5,
        flex: 1,
    },

})