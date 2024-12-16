import * as React from 'react'
import { View, Text, TouchableOpacity, StyleSheet, Image } from 'react-native';

interface BrakeProps {
    onDataSend: (data: object) => void;
    onBrakeChange: (isBraking: boolean) => void;
}


const BrakeBtn: React.FC<BrakeProps> = ({onDataSend, onBrakeChange }) => {

    const toggleBrake = () => {
        onBrakeChange(true)
    };

    const releasedBrake = () => {
        onBrakeChange(false)
    };

    return(
        <View style={style.container}>
            <TouchableOpacity style={style.btn} onPressIn={toggleBrake} onPressOut={releasedBrake}>
                <Image style={style.image} source={require("@/assets/images/disc-brake.png")}/>
            </TouchableOpacity>
        </View>
    )
}


const style = StyleSheet.create({
    container:{
        justifyContent: "center"
    },
    btn:{
        padding: 15,
        backgroundColor: "#AA3333",
        borderRadius: 10,
    },
    image:{
        width: 24,
        height: 24,
    },
})

export default BrakeBtn;