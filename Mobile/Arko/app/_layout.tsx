import { Stack } from "expo-router";
import * as React from 'react';
import { View, Text, StyleSheet } from 'react-native';
import { useFonts } from 'expo-font';
import * as SplashScreen from 'expo-splash-screen';
import { useColorScheme } from "react-native";
import { useRouter } from "expo-router";

export default function RootLayout() {
  const router = useRouter()
  const [loaded] = useFonts({
    SpaceMono: require('../assets/fonts/SpaceMono-Regular.ttf'),
    CeraPro: require('../assets/fonts/CeraPro-Regular.ttf'),
    CeraPro_Light: require('../assets/fonts/CeraPro-Light.ttf'),
    CeraPro_Medium: require('../assets/fonts/CeraPro-Medium.ttf'),
    CeraPro_Bold: require('../assets/fonts/CeraPro-Bold.ttf'),
    CeraPro_BoldItalic: require('../assets/fonts/CeraPro-BoldItalic.ttf'),
    CeraPro_RegularItalic: require('../assets/fonts/CeraPro-RegularItalic.ttf'),
    CeraPro_Thin: require('../assets/fonts/CeraPro-Thin.ttf'),
  });

  React.useEffect(() => {

    if (loaded) {
      SplashScreen.hideAsync();
    }
  }, [loaded]);

  if (!loaded) {
    return null;
  }
  
  return (
    <Stack>
      <Stack.Screen name="index" options={{ headerBackButtonMenuEnabled: false, headerShown: false }} />
      <Stack.Screen name="(login)" options={{ headerBackButtonMenuEnabled: false, headerShown: false }} />
      <Stack.Screen name="(home)" options={{ headerBackButtonMenuEnabled: false, headerShown: false }} />
    </Stack>
  );
}
