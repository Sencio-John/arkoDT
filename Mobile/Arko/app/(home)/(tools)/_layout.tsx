import { Stack } from "expo-router";
import React from 'react';
import { View, Text, StyleSheet } from 'react-native';
import { useColorScheme } from "react-native";

export default function Tools() {

  const colorScheme = useColorScheme();

  return (
    <Stack>
      <Stack.Screen name="camera" options={{headerShown: false}}/>
      <Stack.Screen name="controller" options={{headerShown: false}}/>
      <Stack.Screen 
        name="index" 
        options={{
          headerTitle: "Tracking Map", 
          headerTitleAlign: "center",
          headerTitleStyle: {
            fontFamily: 'CeraPro_Medium',  
            fontSize: 20,
            color: colorScheme === 'dark' ? '#ECEDEE' : '#11181C',              
          },
          headerTintColor: colorScheme === 'dark' ? '#ECEDEE' : '#11181C', 
          headerStyle:{
            backgroundColor: colorScheme === 'dark' ? '#151718' : '#fff', 
          }
          }}/>
    </Stack>
  );
}
