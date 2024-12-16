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
    onVerify: (ssid: string, password: string) => void;
}

const VerifyDeviceCred: React.FC<DeviceCredProps> = ({modalVisible, onClose, onVerify}) =>{

    const [inputs, setInputs] = React.useState({
        key: "",
        password: "",
    })

    const [error, setError] = React.useState({
        key: "",
        password: "",
    });

    const handleVerify = () => {
        let hasError = false;
        if(!inputs.key){
            handleError("SSID is required!", "key");
            hasError = true;
        } else{
            handleError('', 'key');
        }
        if(!inputs.password){
            handleError("Password is required!", "password");
            hasError = true;
        } else{
            handleError('', 'password');
        }

        if (!hasError) {
            onVerify(inputs.key, inputs.password)
            setInputs({ key: '', password: ''}); // Reset inputs
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
        setInputs({ key: '', password: ''});
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
                    <ThemedText style={style.modalTitle}>Verify ARKO Device</ThemedText>
                    <View style={style.form}>
                        <View style={style.input}>
                            <Input 
                                placeholder='Key' 
                                iconName='key-outline' 
                                onChangeText={(text) => handleOnChange(text, "key")}
                                onFocus={() => handleError(null, "key")}
                                error={error.key}
                                value={inputs.key}
                                />
                        </View>

                        <View style={style.input}>
                            <Input 
                                placeholder='Password' 
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
                            <Button title="Verify" onPress={handleVerify}/>
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

export default VerifyDeviceCred;