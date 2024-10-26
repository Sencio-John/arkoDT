import getData from "@/constants/getData";
import AsyncStorage from "@react-native-async-storage/async-storage";
import { Stack } from "expo-router";
import React from 'react';
import { View, Text, StyleSheet } from 'react-native';
import { useColorScheme } from "react-native";


export default function Home() {


  const colorScheme = useColorScheme();
  return (
    <Stack
      screenOptions={{
        headerShown: false,
      }}>
      <Stack.Screen name="index" />
      <Stack.Screen name="(tools)" />
      <Stack.Screen name="(settings)" />
      <Stack.Screen name="(device)" />
    </Stack>
  );
}

