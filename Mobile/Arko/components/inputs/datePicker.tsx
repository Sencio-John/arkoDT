
import DatePicker from "react-native-ui-datepicker";
import React, { useState } from "react";
import {
  StyleSheet,
  View,
  Text,
  TextInput,
  TouchableOpacity,
} from "react-native";
import { ThemedText } from "../ThemedText";
import { useColorScheme } from "react-native";
import { Ionicons } from "@expo/vector-icons";

interface InputProps {
    label?: string;
    iconName?: string;
    error?: string;
    value?: string;
    onDateChange?: (date: Date) => void;
}

const DateInput: React.FC<InputProps> = ({
    label,
    error,
    value,
    onDateChange,
}) => {

    const colorScheme = useColorScheme();
    const [date, setDate] = useState(value ? new Date(value) : new Date());

    const handleConfirm = (selectedDate: Date) => {
        setDate(selectedDate);
        onDateChange && onDateChange(selectedDate);
    };


    const formattedDate = date.toLocaleDateString("en-US", {
        month: "short",
        day: "numeric",
        year: "numeric",
    });

    return (
        <View style={style.container}>
            <View style={[style.formcontainer, error && style.error]}>
                <ThemedText style={[style.label, error && style.error ,{ backgroundColor: colorScheme === 'dark' ? '#151718' : '#F6F6F6' }]}>
                    {label}
                </ThemedText>
                <Ionicons name='calendar' style={[style.iconHead, error && style.error, {color: colorScheme === 'dark' ? '#D3D3D3' : '#545454'}]} />
                <TouchableOpacity onPress={() => {}} style={style.textInput}>
                    <Text style={{ color: colorScheme === 'dark' ? '#D3D3D3' : '#545454', fontSize: 16 }}>
                        {formattedDate}
                    </Text>
                </TouchableOpacity>
            </View>



        {error && <Text style={style.errmsg}>{error}</Text>}
        </View>
    );
};

const style = StyleSheet.create({
  container: {
    width: '95%',
  },
  formcontainer: {
    flexDirection: 'row',
    alignItems: 'center',
    borderWidth: 1,
    borderColor: '#717171',
    borderRadius: 5,
    height: 47
  },
  textInput: {
    flex: 1,
    fontSize: 16,
    fontFamily: "CeraPro",
    justifyContent: 'center',
  },
  iconHead: {
    fontSize: 24,
    padding: 10,
  },
  focus: {
    borderColor: "#0B60B0",
    borderWidth: 2,
  },
  error: {
    borderColor: "red",
  },
  errmsg: {
    position: "absolute",
    marginVertical: 50,
    marginLeft: 5,
    color: "red",
    fontSize: 12,
    fontFamily: "CeraPro",
  },
  label: {
    position: "absolute",
    backgroundColor: "white",
    left: 25,
    top: -15,
    zIndex: 999,
    paddingHorizontal: 8,
    fontSize: 14,
    fontFamily: "CeraPro_Medium",
  },
  focusColor: {
    color: "#277CA5",
  },
});

export default DateInput;
