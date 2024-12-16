import * as React from 'react';
import { View, Text, TouchableOpacity, StyleSheet } from 'react-native';
import { Ionicons } from '@expo/vector-icons';

interface GearProps {
    onDataSend: (data: object) => void;
}

const Gear: React.FC<GearProps> = ({onDataSend}) => {
    const [gear, setGear] = React.useState<"FORWARD" | "REVERSE">("FORWARD");

    const toggleGear = () => {
        const newGear = gear === "FORWARD" ? "REVERSE" : "FORWARD";
        setGear(newGear);
        onDataSend({ gear: newGear });
    };

    const gearIcon = gear === "FORWARD" 
        ? "chevron-up-circle-outline" 
        : "chevron-down-circle-outline";

    return (
        <View style={styles.container}>
            <TouchableOpacity style={styles.btn} onPress={toggleGear}>
                <Ionicons style={styles.btnText} name={gearIcon}/>
            </TouchableOpacity>
        </View>
    );
};

const styles = StyleSheet.create({
    container: {
        justifyContent: "center",
    },
    btn: {
        padding: 15,
        backgroundColor: "#277CA5",
        borderRadius: 10,
    },
    btnText: {
        textAlign: "center",
        textAlignVertical: 'center',
        fontSize: 24,
        fontFamily: "CeraPro",
        color: "#F0E0E0",
    },
});

export default Gear;

