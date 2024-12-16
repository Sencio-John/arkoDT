import React from 'react';
import { View, Text, StyleSheet, TouchableOpacity} from 'react-native';
import { ThemedText } from '../ThemedText';
import { ThemedView } from '../ThemedView';
import { Ionicons } from '@expo/vector-icons';
import { useColorScheme } from 'react-native';

export default function PinDetail({icon_name, title, subtitle, onPress=() => {}}) {

    const colorScheme = useColorScheme();
    const maxSubtitleLength = 28;

    const truncatedSubtitle = subtitle.length > maxSubtitleLength
        ? `${subtitle.substring(0, maxSubtitleLength)}...`
        : subtitle;

    return(
        <TouchableOpacity style={style.container} onPress={onPress}>     
            <View style={style.left}>  
                <View style={style.iconContainer}>
                    <Ionicons
                        style={[style.icon,{ color: icon_name === 'checkmark-circle' ? "#00ff00" : title === 'Rescue' ? "#ff9933": "#e3cd09"}]}
                        name={icon_name}
                        />
                </View>
                <View style={style.textContainer}>
                    <ThemedText type='fade' style={style.title}>
                        {title}
                    </ThemedText>
                    <ThemedText type="fade" style={style.subtitle}>
                        {truncatedSubtitle}
                    </ThemedText>
                </View>
            </View>  
            <View style={style.right}>
                <ThemedText>Go</ThemedText>
                <ThemedText>
                    <Ionicons style={style.iconGo} name="chevron-forward" />
                </ThemedText>
            </View>
        </TouchableOpacity>
    )
}

const style = StyleSheet.create({
    container:{
        flexDirection: "row",
        paddingHorizontal: 30,
        paddingVertical: 15,
        justifyContent: "space-between",
        alignItems: "center",
        borderColor:"black",
    },
    iconContainer:{
        justifyContent: "center",
    },
    title:{
        fontWeight: "bold"
    },
    subtitle:{
        fontSize: 14,
        
    },
    icon:{
        fontSize: 24,
        justifyContent: "center",
        alignContent: "center",
        alignItems: "center",
        alignSelf: "center",
        color: "#277CA5",
    },
    iconGo:{
        fontSize: 24,
        justifyContent: "center",
        alignContent: "center",
        alignItems: "center",
        alignSelf: "center",
    },
    textContainer:{
        flex: 1,
        marginHorizontal: 12,
        justifyContent: "center"
    },
    left:{
        flexDirection: "row"
    },
    right:{
        flexDirection: "row",
        justifyContent: "center",
        alignItems: "center",
        alignContent: "center",
    },
    dark:{
        backgroundColor: "#151718",
        color: "#11181C"
    },
    light:{
        backgroundColor: "#fff",
        color: "#ECEDEE"
    },
})