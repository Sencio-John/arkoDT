import { Ionicons } from '@expo/vector-icons';
import * as React from 'react'
import { View, Text, TouchableOpacity, StyleSheet } from 'react-native';

interface LightProps {
    onDataSend: (data: object) => void;
}

const Light: React.FC<LightProps> = ({ onDataSend }) => {
    
    const [toggle, setToggle] = React.useState(false);
    const [iconName, setIconName] = React.useState("flash-off")

    const handleLight = () =>{
        setToggle(prevToggle => {
            const newToggle = !prevToggle;
            const lightState = newToggle ? "on" : "off";
            setIconName(newToggle ? "flash" : "flash-off"); 
            onDataSend({ lights: lightState });
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
        borderColor: "#64C2EC",
        borderWidth: 1,
        borderRadius: 10,
    },
    icon:{
        fontSize: 24,
        color: "#64C2EC"
    },
    on:{
        backgroundColor: "#277CA5",
        color: "#f9f9f9"
    }
})

export default Light