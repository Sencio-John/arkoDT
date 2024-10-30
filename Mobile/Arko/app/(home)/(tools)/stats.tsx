import * as React from 'react';
import { View, Text, StyleSheet, StatusBar, useColorScheme } from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';

import { Dimensions } from "react-native";
import { LineChart} from 'react-native-chart-kit';

export default function Stats() {
    
    const colorScheme = useColorScheme();
    const screenWidth = Dimensions.get("window").width;

    const colorProps = ({
        dataColor: colorScheme === 'dark' ? '#B8F5FF' : `#277CA5`,
        bgColor: colorScheme === 'dark' ? '#151718' : '#F6F6F6',
        legendColor: colorScheme === 'dark' ? '#64C2EC' : `#64C2EC`,
        labelColor: colorScheme === 'dark' ? '#f8f8f8' : `#113547`,
    })

    const data = {
        labels: ["1:00 PM", "2:00 PM", "3:00 PM", "4:00 PM", "5:00 PM"],
        datasets: [
            {
            data: [0, 0.5, 1, 1.5, 2, 3],
            color: (opacity = 1) => colorProps.dataColor,
            strokeWidth: 2 
            }
        ],
        legend: ["Water Level"]
    };

    return (
        <SafeAreaView style={[style.container, {backgroundColor: colorScheme === 'dark' ? '#151718' : '#F6F6F6'}] }>
            <LineChart
                data={data}
                width={screenWidth}
                height={220}
                chartConfig={{
                    backgroundGradientFrom: colorProps.bgColor,
                    backgroundGradientTo: colorProps.bgColor,
                    color: (opacity = 1) => colorProps.legendColor,
                    labelColor: (opacity = 1) => colorProps.labelColor,
                }}
                />
            
        </SafeAreaView>
    );
}


const style = StyleSheet.create({
    container:{
        flex: 1,
    },

})