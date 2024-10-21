import { View, Text, StyleSheet, SafeAreaView, TextInput, Modal } from 'react-native';
import * as React from 'react';
import { useColorScheme } from 'react-native';
import { ThemedView } from '../ThemedView';
import { ThemedText } from '../ThemedText';

import Input from '../inputs/input';
import Button from '../buttons/button';
import BigInput from '../inputs/bigInput';
import ComboBox from '../inputs/select';
import SelectBox from '../inputs/select';


interface MarkerModalProps {
    modalVisible: boolean;
    onClose: () => void;
    onAddMarker: (title: string, description: string) => void;
}

const MarkerModal: React.FC<MarkerModalProps> = ({modalVisible, onClose, onAddMarker}) =>{

    const [markerTitle, setMarkerTitle] = React.useState('');
    const [markerDescription, setMarkerDescription] = React.useState('');

    const [inputs, setInputs] = React.useState({
        title: "",
        description: "",
    })

    const [error, setError] = React.useState({
        title: '',
        description: '',
    });

    const handleAddMarker = () => {
        let hasError = false;
        if(!inputs.title){
            handleError("Title is required!", "title");
            hasError = true;
        } else{
            handleError('', 'title');
        }
        if(!inputs.description){
            handleError("Description is required!", "description");
            hasError = true;
        } else{
            handleError('', 'description');
        }

        if (!hasError) {
            onAddMarker(inputs.title, inputs.description);
            setInputs({ title: '', description: '' }); // Reset inputs
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
        setInputs({title: "", description: ""});
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
        height: 300,
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