import * as React from 'react'
import { View, Text, TouchableOpacity, StyleSheet } from 'react-native';

interface BrakeProps {
    onDataSend: (data: object) => void;
}

const BrakeBtn: React.FC<BrakeProps> = ({onDataSend}) => {

    const toggleBrake = () => {
        onDataSend({ brake: true });
    };

    const releasedBrake = () => {
        onDataSend({ brake: true });
    };

    return(
        <View style={style.container}>
            <TouchableOpacity style={style.btn} onPressIn={toggleBrake} onPressOut={releasedBrake}>
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

export default BrakeBtn;