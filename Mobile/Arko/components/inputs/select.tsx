import { Ionicons } from "@expo/vector-icons";

import React, { useState } from "react";
import {
  StyleSheet,
  View,
  Text,
  TextInput,
  KeyboardAvoidingView,
  TouchableOpacity,
  TextInputProps,
} from "react-native";
import { ThemedText } from "../ThemedText";
import { useColorScheme } from "react-native";
import { Picker } from '@react-native-picker/picker';

interface InputProps extends TextInputProps {
  label?: string;
  iconName?: string;
  error?: string;
  password?: boolean;
  item?: any; 
  isEditable?: boolean;
  value?: string;
  changeAccount?: (value: string) => void;
  onFocus?: () => void;
}

const SelectBox: React.FC<InputProps> = ({
  label,
  iconName,
  error,
  password,
  item,
  isEditable,
  value,
  changeAccount,
  onFocus = () => {},
  ...props
}) => {

    const colorScheme = useColorScheme();

    const [hidePassword, setHidePassword] = React.useState(password);
    const [isFocused, setFocused] = React.useState(false);
    const [selectedValue, setSelectedValue] = React.useState('');

    const pickerRef = React.useRef();


    return (
        <View style={[style.container, error && style.error]}>


        <View style={[style.formcontainer, isFocused && style.focus]}>
            
            <Ionicons name={iconName} style={[style.iconHead, error && style.error, {color: colorScheme === 'dark' ? '#D3D3D3' : '#545454'}]} />
            <Picker
                selectedValue={value}
                onValueChange={(itemValue) => {
                    if (changeAccount) {
                        changeAccount(itemValue); // Call changeAccount to pass the selected value back
                    }
                }}
                style={[style.picker, {color: colorScheme === 'dark' ? '#D3D3D3' : '#545454'}]}
                placeholder=""
                dropdownIconColor={colorScheme === 'dark' ? '#D3D3D3' : '#545454'} 
            >
                <Picker.Item style={style.item} label="Select Marking Title..." value=""/>
                <Picker.Item style={style.item} label="Rescue" value="Rescue" />
                <Picker.Item style={style.item}label="For Relief" value="For Relief" />
            </Picker>

            
        </View>
        {error && <Text style={style.errmsg}>{error}</Text>}
        </View>
  );
};


const style = StyleSheet.create({
    container: {
        width: '95%',
        borderWidth: 1,
        borderColor: '#717171',
        borderRadius: 5,
    },
    item: {
        fontSize: 16,
        fontFamily: "CeraPro"
    },

    formcontainer: {
        flexDirection: 'row',
        alignItems: 'center',
        justifyContent: "center",
        alignContent: "center",
        height: 47,
        width: '95%',
    },

    picker: {
        flex: 1,
        fontSize: 16,
        margin: -15,
    },
    iconHead:{
        fontSize: 24,
        padding: 10,
    },
    icon: {
        fontSize: 24,
        position: "absolute",
        top: 0,
        right: 0,
        color: "#8B8D8F",
        marginTop: 10,
        marginRight: 10,
        justifyContent: "center",
        alignItems: "center",
    },
    focus: {
        borderColor: "#0B60B0",
        borderWidth: 2,
    },
    errorBG: {
        borderColor: "red",
    },
    errlabel: {
        color: "red",
    },
    errmsg: {
        position: "absolute",
        marginTop: 50,
        marginLeft: 5,
        color: "red",
        fontSize: 12,
        fontFamily: "CeraPro",
    },
    label: {
        position: "absolute",
        backgroundColor: "white",
        left: 12,
        top: -10,
        zIndex: 999,
        paddingHorizontal: 8,
        fontSize: 14,
        fontFamily: "CeraPro",
        color: "#537FE7",
    },
    error: {
        borderColor: "red",
        color: "red",
    },
});

export default SelectBox;
