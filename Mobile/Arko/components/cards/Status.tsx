import React from 'react';
import { View, Text, StyleSheet, TouchableOpacity} from 'react-native';
import { ThemedText } from '../ThemedText';
import { ThemedView } from '../ThemedView';
import { Ionicons } from '@expo/vector-icons';


const Status = ({
    status,
    type = 'default',
    ...rest
}) =>{



    return(
        <View style={[
            type === 'default' ? styles.default : undefined,
            type === 'red' ? styles.red : undefined,
            type === 'yellow' ? styles.yellow : undefined,
            type === 'green' ? styles.green : undefined,
            styles.status
        ]}>
            <View style={[
            type === 'default' ? styles.defaultDot : undefined,
            type === 'red' ? styles.redDot : undefined,
            type === 'yellow' ? styles.yellowDot : undefined,
            type === 'green' ? styles.greenDot : undefined,
            styles.statusDot
        ]} />
            <ThemedText style={[
            type === 'default' ? styles.default : undefined,
            type === 'red' ? styles.red : undefined,
            type === 'yellow' ? styles.yellow : undefined,
            type === 'green' ? styles.green : undefined,
            styles.statusText,
        ]}>{status}</ThemedText>
        </View>
    )
}

const styles = StyleSheet.create({
    status: {
        flexDirection: 'row',
        alignItems: 'center',
        padding: 5,
        borderRadius: 4,
    },
    statusDot: {
        width: 8,
        height: 8,
        borderRadius: 4,
        marginRight: 5,
    },
    statusText: {
        fontSize: 14,
    },
    default:{
        backgroundColor: "#DED9D9",
        color: "black",
    },
    defaultDot:{
        backgroundColor: "#0E0E0E",
    },
    red:{
        backgroundColor: "#F0E0E0",
        color: "#AA3333",
    },
    redDot:{
        backgroundColor: "#993131",
    },
    yellow:{
        backgroundColor: "#EFF0E0",
        color: "#A8AA33",
    },
    yellowDot:{
        backgroundColor: "#D6B644",
    },
    green:{
        backgroundColor: "#E0F0E4",
        color: "#377E36",
    },
    greenDot:{
        backgroundColor: "#31994B",
    }
})

export default Status