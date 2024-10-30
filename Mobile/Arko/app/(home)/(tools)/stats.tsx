import * as React from 'react';
import { View, Text, StyleSheet, StatusBar, useColorScheme } from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';


export default function Stats() {
    
    const colorScheme = useColorScheme();

    return (
        <SafeAreaView style={[style.container, {backgroundColor: colorScheme === 'dark' ? '#151718' : '#F6F6F6'}] }>
            
        </SafeAreaView>
    );
}


const style = StyleSheet.create({
    container:{
        flex: 1,
        backgroundColor: "white",
    },

})