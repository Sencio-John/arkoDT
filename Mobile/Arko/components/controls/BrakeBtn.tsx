import { Ionicons } from '@expo/vector-icons';
import * as React from 'react'
import { View, Text, TouchableOpacity, StyleSheet } from 'react-native';


export default function Brake(){

    return(
        <View style={style.container}>
            <TouchableOpacity style={style.btn}>
                <Text style={style.btnText}>BRAKE</Text>
            </TouchableOpacity>
        </View>
    )
}


const style = StyleSheet.create({
    container:{
        width: 175,
        justifyContent: "center"
    },
    btn:{
        padding: 10,
        backgroundColor: "#AA3333",
        borderRadius: 10,
    },
    btnText:{
        textAlign: "center",
        textAlignVertical: 'center',
        fontSize: 18,
        fontFamily: "CeraPro",
        color: "#F0E0E0",
    },
})