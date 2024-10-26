import { Stack } from "expo-router";
import React from 'react';
import { View, Text, StyleSheet } from 'react-native';
import { useColorScheme } from "react-native";

export default function LoginLayout() {

  const colorScheme = useColorScheme();

  return (
    <Stack>
        <Stack.Screen name="index" 
        options={{
            headerShown: false,
            }}/>
        <Stack.Screen name="login" 
        options={{
            headerShown: false,
            }}/>
        <Stack.Screen name="forgot" 
        options={{
          headerTitle: "Forgot Password", 
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
        <Stack.Screen name="reset" 
            options={{
            headerTitle: "Reset Password", 
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
        <Stack.Screen name="verify" 
            options={{
            headerTitle: "Verify Email", 
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

