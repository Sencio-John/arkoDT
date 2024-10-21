import * as React from 'react';
import { View, Text, StyleSheet, TouchableOpacity } from 'react-native';
import { Ionicons } from '@expo/vector-icons';
import { ThemedView } from '../ThemedView';
import { ThemedText } from '../ThemedText';



export default function IconButton({ type, onPress=()=>{}}) {

    const [iconName, setIconName] = React.useState('');
    const [buttonName, setButtonName] = React.useState('');

    React.useEffect(() =>{
        Selection(type);
    }, [type])

    if(!iconName && !buttonName){
        return null;
    }

    function Selection(type: string){
        let icon, name;

        if(type === "map"){
            icon = "navigate";
            name = "Track Location"
        } else if(type === "camera"){
            icon = "videocam"
            name = "Live Camera Feed"
        } else if(type === "control"){
            icon = "game-controller"
            name = "Boat Controller"
        }

        setIconName(icon);
        setButtonName(name);
    }

    return (
        <View style={style.container}>
            <TouchableOpacity onPress={onPress} style={style.touchable}>
                <ThemedView style={style.button}>
                    <ThemedText>
                        <Ionicons style={style.icon} name={iconName} />
                    </ThemedText>
                </ThemedView>
                <View style={style.btnName}>
                    <ThemedText style={style.text} type='default'>
                        {buttonName}
                    </ThemedText>
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
        width: 100,
    },
    touchable: {
        alignItems: 'center',
        justifyContent: 'center',
    },
    button:{
        padding: 15,
        borderRadius: 10,
        borderColor: '#ddd',
        borderWidth: 1,
        alignItems: 'center',
        justifyContent: 'center',
        marginBottom: 10,
    },
    icon:{
        fontSize: 24,
    },
    btnName:{
        justifyContent: "center",
        textAlign: "center",
    },
    text: {
        textAlign: 'center',
        justifyContent: "center"
    },
})