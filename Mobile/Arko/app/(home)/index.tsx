
import * as React from 'react';
import { TouchableOpacity, View, Text, Image, StyleSheet, BackHandler } from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';
import { ThemedView } from '@/components/ThemedView';
import { ThemedText } from '@/components/ThemedText';
import Ionicons from '@expo/vector-icons/Ionicons';

import WeatherCard from '@/components/cards/WeatherCard';
import DeviceStatusCard from '@/components/cards/DeviceCard';
import IconButton from '@/components/buttons/IconButton';
import { useRouter } from 'expo-router';
import { useColorScheme } from 'react-native';
import getData from '@/constants/getData';
import AsyncStorage from '@react-native-async-storage/async-storage';
import SplashScreen from '@/components/SplashScreen';

const Dashboard = () =>{

    const router = useRouter();
    const colorScheme = useColorScheme();

    const [userData, setUserData] = React.useState(null)
    const [loading, setLoading] = React.useState(true);
    

    React.useEffect(() => {
        const fetchData = async () => {
            const key = await AsyncStorage.getItem('log_key');
            if(key){
                const data = await getData(key);
                setUserData(data);
            } else{
                router.replace("/(login)/");
            }
            
            setLoading(false);
        };

        fetchData();
    }, []);

    if (loading) {
        return <SplashScreen />; // Replace with your actual loading component
    }

    return(
        <SafeAreaView style={[style.container, {backgroundColor: colorScheme === 'dark' ? '#151718' : '#F6F6F6'}] }>
            {/* TOP BAR */}
            <ThemedView style={style.header}>
                <View style={style.profilegrp}>
                    <View style={style.profileContainer}>
                        <Image style={style.profile} source={require('../../assets/images/user-profile-icon.png')}/>
                    </View>
                    <ThemedText style={style.user}>Hi, {userData?.Name}</ThemedText>
                </View>
                <View>
                    <TouchableOpacity onPress={() => router.push('/(home)/(settings)')}>
                        <ThemedText>
                            <Ionicons style={style.icon} name="settings-outline"/>
                        </ThemedText>
                    </TouchableOpacity>
                </View>
            </ThemedView>
            

            {/* CONTENT DASHBOARD */}
            <ThemedView style={dashboard.container}>
                <View style={dashboard.card}>
                    <WeatherCard/>
                </View>
                <ThemedText type='subtitle' style={dashboard.header}>Device</ThemedText>
                <View style={dashboard.card}>
                    <DeviceStatusCard onPress={() => {router.navigate('/(home)/(device)/')}} />
                </View>
                <ThemedText type='subtitle' style={dashboard.header}>Quick Access Tools</ThemedText>
                <View style={dashboard.card}>
                    <View style={dashboard.buttons}>
                        <IconButton type="map" onPress={() => router.navigate('/(home)/(tools)/')}/>
                        <IconButton type="stats" onPress={() => router.navigate('/(home)/(tools)/stats')}/>
                        <IconButton type="control" onPress={() => router.navigate('/(home)/(tools)/controller')}/>
                    </View>
                </View>
            </ThemedView>
        </SafeAreaView>
    )
}

const style = StyleSheet.create({
    container: {
        backgroundColor: "white",
        flex: 1,
    },
    header:{
        height: 85,
        width: "100%",
        paddingHorizontal: 20,
        paddingTop: 15,
        alignItems: "center",
        flexDirection: "row",
        direction: "ltr",
        justifyContent: "space-between",
        alignContent: "center",
        borderBottomWidth: 2,
        borderColor: 'rgba(175, 175, 175, 0.5)'
    },
    profilegrp:{
        flexDirection: "row",
        alignContent: "center",
        alignItems: "center"
    },
    profileContainer: {
        height: 50,
        width: 50,
        overflow: "hidden",
        borderRadius: 50,
        marginRight: 15,
        alignItems: "center",
        alignContent: "center",
        justifyContent: "center"
    },
    profile: {
        width: 50,
        height: 50,
    },
    user:{
        fontFamily: "CeraPro_Medium",
        fontSize: 18,
    },
    icon:{
        fontSize: 24,
    }
});

const dashboard = StyleSheet.create({
    container:{
        paddingHorizontal: 20,
        paddingVertical: 25,
        justifyContent: "center",
    },
    header:{
        marginVertical: 10,
    },
    card:{
        marginBottom: 20,
        marginTop: 10,
    },
    buttons:{
        flexDirection: "row",
        justifyContent: "space-between",
        alignContent: "center",
        marginVertical: 10,
    },
})

export default Dashboard;