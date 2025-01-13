import { Ionicons } from '@expo/vector-icons';
import * as React from 'react'
import { View, Text, TouchableOpacity, StyleSheet } from 'react-native';

interface LightProps {
    onDataSend: (data: object) => void;
}

const MicBtn: React.FC<LightProps> = ({onDataSend}) => {

    const [toggle, setToggle] = React.useState(false);
    const [iconName, setIconName] = React.useState("mic-off")

    const toggleMic = () =>{
        setToggle(prevToggle => {
            const newToggle = !prevToggle;
            const micState = newToggle ? "on" : "off";
            setIconName(newToggle ? "mic" : "mic-off"); 
            onDataSend({ mic: micState });
            return newToggle;
        });
    }

    return(
        <View style={style.container}>
            <TouchableOpacity style={[style.btn, toggle && style.on]} onPress={() => toggleMic()}>
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

export default MicBtn;