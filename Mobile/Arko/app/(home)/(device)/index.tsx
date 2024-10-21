import React from 'react';
import { View, Text, StyleSheet, SafeAreaView, TouchableOpacity } from 'react-native';
import { useColorScheme } from "react-native";

import { ThemedView } from "@/components/ThemedView";
import { ThemedText } from "@/components/ThemedText";
import { Ionicons } from "@expo/vector-icons";


export default function Device (){
    const colorScheme = useColorScheme()

    return(
        <SafeAreaView style={[style.container, {backgroundColor: colorScheme === 'dark' ? '#151718' : '#fff'}]}>
            <View style={style.header}>
                <ThemedText type="fade">
                    Connected Device
                </ThemedText>
            </View>
            <ThemedView>

            </ThemedView>
            <View style={style.header}>
                <ThemedText type="fade">
                    Recent Devices
                </ThemedText>
                <TouchableOpacity>
                    <ThemedText>
                        <Ionicons style={style.icon} name="add" />
                    </ThemedText>
                </TouchableOpacity>
                
            </View>
        </SafeAreaView>
    )
}

const style = StyleSheet.create({
    container:{
        flex: 1,
    },
    header:{
        margin: 25,
        paddingVertical: 10,
        borderBottomColor: "#7F7F7F",
        borderBottomWidth: 1,
        flexDirection: "row",
        justifyContent: "space-between"
    },
    icon:{
        fontSize: 24
    }
});