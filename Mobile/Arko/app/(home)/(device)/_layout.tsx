import { Stack } from "expo-router";
import React from 'react';
import { View, Text, StyleSheet } from 'react-native';
import { useColorScheme } from "react-native";

export default function DeviceConnection() {

  const colorScheme = useColorScheme();

  return (
    <Stack>
      <Stack.Screen name="index" 
        options={{
          headerTitle: "Device Connection", 
          headerTitleAlign: "center",
          headerTitleStyle: {
            fontFamily: 'CeraPro_Medium',  
            fontSize: 20,
            color: colorScheme === 'dark' ? '#ECEDEE' : '#11181C',              
          },
          headerTintColor: colorScheme === 'dark' ? '#ECEDEE' : '#11181C', 
          headerStyle:{
            backgroundColor: colorScheme === 'dark' ? '#151718' : '#F6F6F6', 
          }
          }}/>
    </Stack>
  );
}

