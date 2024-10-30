import * as React from 'react';
import { View, Text, StyleSheet, TouchableOpacity } from 'react-native';
import { Ionicons } from '@expo/vector-icons';
import { ThemedView } from '../ThemedView';
import { ThemedText } from '../ThemedText';


export default function PinsBtn({iconName, onPress=()=>{}}) {

    return (
        <View style={style.container}>
            <TouchableOpacity onPress={onPress} style={style.touchable}>
                <View style={style.button}>
                    <Text>
                        <Ionicons style={style.icon} name={iconName} />
                    </Text>
                </View>
            </TouchableOpacity>
        </View>
    );
}


const style = StyleSheet.create({
    container:{
        justifyContent: "center",
        textAlign: "center",
        flexDirection: "column",
    },
    touchable: {
        alignItems: 'center',
        justifyContent: 'center',
    },
    button:{
        padding: 8,
        borderRadius: 10,
        borderColor: '#ddd',
        borderWidth: 1,
        alignItems: 'center',
        justifyContent: 'center',
        marginBottom: 10,
        backgroundColor: '#113547'
    },
    icon:{
        fontSize: 24,
        color: "#fff"
    },
    btnName:{
        justifyContent: "center",
        textAlign: "center",
    },
})