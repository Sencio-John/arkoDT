import * as React from 'react';
import { TouchableOpacity, View, Text, Image, StyleSheet } from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';
import { useColorScheme } from 'react-native';
import { useRouter } from 'expo-router';

import { ThemedView } from '@/components/ThemedView';
import { ThemedText } from '@/components/ThemedText';
import Ionicons from '@expo/vector-icons/Ionicons';
import Button from '@/components/buttons/button';
import Firebase from '@react-native-firebase/app';

const ArkoIcon = require('../assets/images/appImg/arko-logo.png');

const OnboardingScreen = () =>{

    
    const router = useRouter();
    const colorScheme = useColorScheme();

    return(
        <SafeAreaView style={[style.container, {backgroundColor: colorScheme === 'dark' ? '#151718' : '#F6F6F6'}] }>
            <View style={style.top}>

                <View style={style.logocontainer}>
                    <Image source={ArkoIcon} style={style.logo}/>
                </View>

                <View style={style.title}>
                    <ThemedText type='title'>
                        A  R  K  O
                    </ThemedText>
                </View>
                <View style={style.subtitle}>
                    <ThemedText type='defaultSemiBold'>
                        subtitle here
                    </ThemedText>
                </View>
            </View>
            <View style={style.bottom}>
                <View style={style.btn}>
                    <Button title="Login" type='primary' onPress={() => {router.navigate('/(login)')}}/>
                </View>
            </View>
            
            
            
        </SafeAreaView>
    );

}

const style = StyleSheet.create({
    container:{
        paddingHorizontal: 20,
        paddingVertical: 25,
        flex: 1,
        alignContent: "center",
        justifyContent: 'space-between',
    },
    top:{
        marginTop: 40,
    },
    logocontainer:{
        flexDirection: "column",
        justifyContent: "center",
        alignContent: "center",
        alignItems: "center"
    },
    logo:{
        height: 275,
        width: 275,
    },
    title:{
        margin: 10,
        alignContent: "center",
        justifyContent: "center",
        alignItems: "center",
        textAlign: "center"
    },
    subtitle:{
        margin: 5,
        textAlign: "center",
        alignItems: "center",
    },
    bottom:{
        flexDirection: "column",
    },
    btn:{
        marginVertical: 10,
        justifyContent: "center",
        alignItems: "center"
    }
})

export default OnboardingScreen;