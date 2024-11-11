import { Ionicons } from '@expo/vector-icons';
import * as React from 'react'
import { View, Text, TouchableOpacity, StyleSheet } from 'react-native';


export default function PinBtn(){


    return(
        <View style={style.container}>
            <TouchableOpacity style={style.btn} onPress={() => {}}>
                <Ionicons name='pin' style={style.icon} />
            </TouchableOpacity>
        </View>
    )
}


const style = StyleSheet.create({
    container:{
        justifyContent: "center",
    },
    btn:{
        padding: 10,
        borderColor: "#black",
        borderWidth: 1,
        borderRadius: 10,
        backgroundColor: "#277CA5",
    },
    icon:{
        fontSize: 24,
        color: "#f9f9f9"
    },
})