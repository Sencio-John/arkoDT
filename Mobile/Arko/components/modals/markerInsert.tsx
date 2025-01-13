import { View, Text, StyleSheet, SafeAreaView, TextInput, Modal } from 'react-native';
import * as React from 'react';
import { useColorScheme } from 'react-native';
import { ThemedView } from '../ThemedView';
import { ThemedText } from '../ThemedText';

import Input from '../inputs/input';
import Button from '../buttons/button';
import SelectBox from '../inputs/select';


interface MarkerModalProps {
    modalVisible: boolean;
    onClose: () => void;
    onAddMarker: (title: string, description: string, fam_name: string) => void;
}

const MarkerModal: React.FC<MarkerModalProps> = ({modalVisible, onClose, onAddMarker}) =>{

    const [inputs, setInputs] = React.useState({
        title: "",
        description: "",
        fam_name: "",
    })

    const [error, setError] = React.useState({
        title: '',
        description: '',
        fam_name: "",
    });

    const handleAddMarker = () => {
        let hasError = false;
        if(!inputs.title){
            handleError("Title is required!", "title");
            hasError = true;
        } else{
            handleError('', 'title');
        }

        if(!inputs.fam_name){
            handleError("Name is required!", "fam_name");
            hasError = true;
        } else{
            handleError('', 'fam_name');
        }

        if (!hasError) {
            onAddMarker(inputs.title, inputs.description, inputs.fam_name);
            setInputs({ title: '', description: '' , fam_name: ''}); // Reset inputs
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
        setInputs({title: "", description: "", fam_name: ""});
        onClose();
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
                    <ThemedText style={style.modalTitle}>Add Marker</ThemedText>
                    <View style={style.form}>
                        <View style={style.input}>
                            <SelectBox 
                                iconName='pin' 
                                changeAccount={(value) => handleOnChange(value, "title")} 
                                onFocus={() => handleError(null, "title")}
                                error={error.title}
                                value={inputs.title}
                                />
                        </View>
                        <View style={style.input}>
                            <Input 
                                placeholder='Name (e.g  Surname, Nickname)' 
                                iconName='people-outline' 
                                onChangeText={(text) => handleOnChange(text, "fam_name")}
                                onFocus={() => handleError(null, "fam_name")}
                                error={error.fam_name}
                                value={inputs.fam_name}
                                />
                        </View>

                        <View style={style.input}>
                            <Input 
                                placeholder='Description' 
                                iconName='chatbox-ellipses-outline' 
                                onChangeText={(text) => handleOnChange(text, "description")}
                                onFocus={() => handleError(null, "description")}
                                error={error.description}
                                value={inputs.description}
                                />
                        </View>
                    </View>
                    
                    <View style={style.buttons}>
                        <View style={style.btn}>
                            <Button title="Add Marker" onPress={handleAddMarker}/>
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

export default MarkerModal;