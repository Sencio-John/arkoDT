import { Stack } from "expo-router";
import * as React from 'react';
import { View, Text, StyleSheet } from 'react-native';
import { useFonts } from 'expo-font';
import { useColorScheme } from "react-native";
import { useRouter } from "expo-router";
import AsyncStorage from "@react-native-async-storage/async-storage";
import SplashScreen from "@/components/SplashScreen";
import DeviceInfo from 'react-native-device-info';

export default function RootLayout() {
  const router = useRouter();
  const ip = DeviceInfo.getIpAddress()
  console.log(ip)
  const [loading, setLoading] = React.useState(true);
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
    const checkLoginStatus = async () => {
      const logKey = await AsyncStorage.getItem('key');

      if (logKey) {
        setLoading(false)
        router.replace("/(home)/"); 
      } else{
        setLoading(false)
        router.replace("/(login)/")
      }
    };

      if (loaded) {
        checkLoginStatus();
      }
  }, [loaded]);

  if (!loaded || loading) {
    return <SplashScreen/>;
  }
  
  return (
    <Stack>
      <Stack.Screen name="index" options={{ headerBackButtonMenuEnabled: false, headerShown: false }} />
      <Stack.Screen name="(login)" options={{ headerBackButtonMenuEnabled: false, headerShown: false }} />
      <Stack.Screen name="(home)" options={{ headerBackButtonMenuEnabled: false, headerShown: false }} />
    </Stack>
  );
}


































