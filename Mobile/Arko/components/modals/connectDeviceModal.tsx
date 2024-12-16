import { View, Text, StyleSheet, SafeAreaView, TextInput, Modal } from 'react-native';
import * as React from 'react';
import { useColorScheme } from 'react-native';
import { ThemedView } from '../ThemedView';
import { ThemedText } from '../ThemedText';

import Input from '../inputs/input';
import Button from '../buttons/button';

interface DeviceCredProps {
    modalVisible: boolean;
    onClose: () => {};
    onVerify: (pin: string, ssid: string, password: string) => void;
}

const ConnectDeviceModal: React.FC<DeviceCredProps> = ({modalVisible, onClose, onVerify}) =>{

    const [inputs, setInputs] = React.useState({
        pin: "",
        ssid: "",
        password: "",
    })

    const [error, setError] = React.useState({
        pin: "",
        ssid: "",
        password: "",
    });

    const handleVerify = () => {
        let hasError = false;
        if(!inputs.pin){
            handleError("OTP is required!", "pin");
            hasError = true;
        } else{
            handleError('', 'pin');
        }

        if(!inputs.ssid){
            handleError("SSID is required!", "ssid");
            hasError = true;
        } else{
            handleError('', 'ssid');
        }
        if(!inputs.password){
            handleError("Password is required!", "password");
            hasError = true;
        } else{
            handleError('', 'password');
        }

        if (!hasError) {
            onVerify(inputs.pin, inputs.ssid, inputs.password)
            setInputs({ pin: '', ssid: '', password: ''}); // Reset inputs
        }
    };
    
    // HANDLES INPUT
    const handleOnChange = (text: string, input: string) => {
        setInputs((prevState) => ({ ...prevState, [input]: text }));
    };

    const handleError = (text: string | null, input: string) => {
        setError((prevState) => ({ ...prevState, [input]: text }));
    };

    const CloseModal =() =>{
        onClose();
        setInputs({ pin: '', ssid: '', password: ''});
    }

    return(
        <Modal
            transparent={true}
            animationType="slide"
            visible={modalVisible}
            onRequestClose={onClose}
        >
            <View style={style.modalOverlay}>
                <ThemedView style={style.modalContainer}>
                    <ThemedText style={style.modalTitle}>Connect ARKO Device</ThemedText>
                    <View style={style.form}>
                        <View style={style.input}>
                            <Input 
                                placeholder='One-Time Pin Code' 
                                iconName='keypad-outline' 
                                onChangeText={(text) => handleOnChange(text, "pin")}
                                onFocus={() => handleError(null, "pin")}
                                error={error.pin}
                                value={inputs.pin}
                                />
                        </View>

                        <View style={style.input}>
                            <Input 
                                placeholder='Wi-Fi SSID' 
                                iconName='key-outline' 
                                onChangeText={(text) => handleOnChange(text, "ssid")}
                                onFocus={() => handleError(null, "ssid")}
                                error={error.ssid}
                                value={inputs.ssid}
                                />
                        </View>

                        <View style={style.input}>
                            <Input 
                                placeholder='Wi-Fi Password' 
                                iconName='ellipsis-horizontal-outline' 
                                onChangeText={(text) => handleOnChange(text, "password")}
                                onFocus={() => handleError(null, "password")}
                                error={error.password}
                                value={inputs.password}
                                password
                                />
                        </View>
                    </View>
                    
                    <View style={style.buttons}>
                        <View style={style.btn}>
                            <Button title="Next" onPress={handleVerify}/>
                        </View>
                        
                        <View style={style.btn} >
                            <Button title="Cancel" type='secondary' onPress={CloseModal} />
                        </View>
                    </View>
                </ThemedView>
            </View>
        </Modal>
    )
}


const style = StyleSheet.create({
    modalOverlay: {
        flex: 1,
        justifyContent: "center",
        alignItems: "center",
        backgroundColor: 'rgba(0, 0, 0, 0.5)',
    },
    modalContainer: {
        width: "90%",
        paddingVertical: 30,
        justifyContent: "center",
        alignItems: "center",
        borderRadius: 10,
    },
    modalTitle: {
        fontSize: 24,
        marginBottom: 20,
    },
    form:{
        flexDirection: 'column',
        justifyContent: "center",
        alignContent: "center",
        alignItems: "center"
    },
    input:{
        marginHorizontal: 10,
        marginVertical: 12,
        flexDirection: "row",
    },

    buttons:{
        flexDirection: "row",
        width: 300,
        justifyContent: "center",
        marginTop: 20,
    },
    btn:{
        marginHorizontal: 15,
        width: 125,
    }
})

export default ConnectDeviceModal;