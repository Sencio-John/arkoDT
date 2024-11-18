import React from 'react';
import { 
    StyleSheet, 
    Text,
    TouchableOpacity,
} from "react-native";

const Button = ({ title, onPress = () => {}, type = 'primary', disabled = false }) => {
    return (
        <TouchableOpacity
            style={[
                style.button,
                type === 'primary' && style.primary,
                type === 'secondary' && style.secondary,
                type === 'danger' && style.danger,
                disabled && style.disabled
            ]}
            onPress={onPress}
            activeOpacity={0.7}
            disabled={disabled}
        >
            <Text style={style.btnTitle}>
                {title}
            </Text>
        </TouchableOpacity>
    );
};

const style = StyleSheet.create({
    button: {
        width: "95%",
        height: 47,
        justifyContent: "center",
        alignItems: "center",
        borderRadius: 10,
    },
    primary: {
        backgroundColor: "#113547", // Primary color (blue)
    },
    secondary: {
        backgroundColor: "#277CA5", // Secondary color (light blue/gray)
    },
    btnTitle: {
        color: "white",
        fontSize: 16,
        fontFamily: "CeraPro_Medium",
    },
    danger:{
        backgroundColor: "#cc0000"
    },

    disabled: {
        opacity: 0.7,
    },
});

export default Button;
