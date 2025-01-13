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
    onVerify: (ip: string) => void;
}

const IPAddressModal: React.FC<DeviceCredProps> = ({modalVisible, onClose, onVerify}) =>{

    const [inputs, setInputs] = React.useState({
        ip: "",
    })

    const [error, setError] = React.useState({
        ip: "",
    });

    const handleVerify = () => {
        let hasError = false;
        if(!inputs.ip){
            handleError("SSID is required!", "key");
            hasError = true;
        } else{
            handleError('', 'key');
        }

        if (!hasError) {
            onVerify(inputs.ip)
            setInputs({ ip: ''}); // Reset inputs
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
        setInputs({ ip: ''});
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
                    <ThemedText style={style.modalTitle}>Input Device IP Address</ThemedText>
                    <View style={style.form}>


                        <View style={style.input}>
                            <Input 
                                placeholder='Device IP Address' 
                                iconName='key-outline' 
                                onChangeText={(text) => handleOnChange(text, "ip")}
                                onFocus={() => handleError(null, "ip")}
                                error={error.ip}
                                value={inputs.ip}
                                />
                        </View>

                    </View>
                    
                    <View style={style.buttons}>
                        <View style={style.btn}>
                            <Button title="Enter" onPress={handleVerify}/>
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

export default IPAddressModal;