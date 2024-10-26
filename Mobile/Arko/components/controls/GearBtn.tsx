import { Ionicons } from '@expo/vector-icons';
import * as React from 'react'
import { View, Text, TouchableOpacity, StyleSheet } from 'react-native';

import { CONTROL_IP } from '@/constants/config';

export default function Gear(){

    const [gear, setGear] = React.useState("FORWARD");
    const [socket, setSocket] = React.useState<WebSocket | null>(null);

    React.useEffect(() => {
        const ws = new WebSocket(CONTROL_IP);
        setSocket(ws);

        ws.onopen = () => {
            console.log('Connected to the server');
        };

        ws.onmessage = (event) => {
            const data = JSON.parse(event.data);
            // Handle incoming messages if needed
        };

        ws.onclose = () => {
            console.log('Disconnected from the server');
        };

        ws.onerror = (error) => {
            console.error('WebSocket error:', error);
        };

        return () => {
            ws.close();
        };
    }, []);

    const toggleGear = () => {
        const newGear = gear === "FORWARD" ? "REVERSE" : "FORWARD";
        setGear(newGear);
        
        // // Send the new gear status to the WebSocket server
        // if (ws.current && ws.current.readyState === WebSocket.OPEN) {
        //     ws.current.send(JSON.stringify({ gear: newGear }));
        // }
    };

    return(
        <View style={style.container}>
            <TouchableOpacity style={style.btn} onPress={toggleGear}>
                <Text style={style.btnText}>{gear}</Text>
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
        backgroundColor: "#277CA5",
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