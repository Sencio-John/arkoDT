import { Ionicons } from '@expo/vector-icons';
import * as React from 'react';
import { View, Text, TouchableOpacity, StyleSheet } from 'react-native';

interface GearProps {
    IP: string;
}

const Gear: React.FC<GearProps> = ({ IP }) => {
    const [gear, setGear] = React.useState("FORWARD");
    const [socket, setSocket] = React.useState<WebSocket | null>(null);

    React.useEffect(() => {
        const ws = new WebSocket(IP);

        ws.onopen = () => {
            console.log('Connected to the server');
            setSocket(ws);
        };

        ws.onclose = () => {
            console.log('Disconnected from the server');
            setSocket(null);
        };

        ws.onerror = (error) => {
            ws.close();
            setSocket(null);
        };

        return () => {
            ws.close();
        };
    }, [IP]);

    const toggleGear = () => {
        const newGear = gear === "FORWARD" ? "REVERSE" : "FORWARD";
        setGear(newGear);

        if (socket && socket.readyState === WebSocket.OPEN) {
            socket.send(JSON.stringify({ gear: newGear }));
        }
    };

    return (
        <View style={styles.container}>
            <TouchableOpacity style={styles.btn} onPress={toggleGear}>
                <Text style={styles.btnText}>{gear}</Text>
            </TouchableOpacity>
        </View>
    );
};

const styles = StyleSheet.create({
    container: {
        width: 175,
        justifyContent: "center",
    },
    btn: {
        padding: 10,
        backgroundColor: "#277CA5",
        borderRadius: 10,
    },
    btnText: {
        textAlign: "center",
        textAlignVertical: 'center',
        fontSize: 18,
        fontFamily: "CeraPro",
        color: "#F0E0E0",
    },
});

export default Gear;
