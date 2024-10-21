import { ThemedText } from '@/constants/components/ThemedText';
import { Ionicons } from '@expo/vector-icons';
import * as React from 'react'
import { View, Text, TouchableOpacity, StyleSheet } from 'react-native';


export default function Light(){

    const [toggle, setToggle] = React.useState(false);
    const [iconName, setIconName] = React.useState("flash-off")

    const handleLight = () =>{
        setToggle(prevToggle => {
            const newToggle = !prevToggle;
            console.log("Light: ", newToggle); 
            setIconName(newToggle ? "flash" : "flash-off"); 
            return newToggle;
        });
    }

    return(
        <View style={style.container}>
            <TouchableOpacity style={[style.btn, toggle && style.on]} onPress={() => handleLight()}>
                <Ionicons name={iconName} style={[style.icon, toggle && style.on]} />
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
    },
    icon:{
        fontSize: 24,
    },
    on:{
        backgroundColor: "#277CA5",
        color: "#f9f9f9"
    }
})