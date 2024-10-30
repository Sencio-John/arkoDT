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
                <View style={[style.iconContainer, {backgroundColor: colorScheme === 'dark' ? style.light.backgroundColor : style.dark.backgroundColor }]}>
                    <View>
                        <Ionicons
                            style={[style.icon, {color: colorScheme === 'dark' ? style.dark.color: style.light.color}]}
                            name={icon_name}
                            />
                    </View>
                </View>
                <View style={style.textContainer}>
                    <ThemedText type='med'>
                        {title}
                    </ThemedText>
                    <ThemedText type="fade">
                        {truncatedSubtitle}
                    </ThemedText>
                </View>
            </View>  
            <View style={style.right}>
                <ThemedText>Go</ThemedText>
                <ThemedText>
                    <Ionicons style={style.icon} name="chevron-forward" />
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
        backgroundColor: "black",
        borderRadius: 50,
        padding: 10,
        justifyContent: "center",
    },
    icon:{
        fontSize: 24,
        justifyContent: "center",
        alignContent: "center",
        alignItems: "center",
        alignSelf: "center"
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