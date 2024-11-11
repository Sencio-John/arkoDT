import * as React from 'react';
import { View, Text, TouchableOpacity, StyleSheet } from 'react-native';

interface GearProps {
    onDataSend: (data: object) => void;
}

const Gear: React.FC<GearProps> = ({onDataSend}) => {
    const [gear, setGear] = React.useState("FORWARD");

    const toggleGear = () => {
        const newGear = gear === "FORWARD" ? "REVERSE" : "FORWARD";
        setGear(newGear);
        onDataSend({ gear: newGear });
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
