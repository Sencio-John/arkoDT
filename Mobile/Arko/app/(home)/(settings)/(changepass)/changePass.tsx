import * as React from 'react';
import { TouchableOpacity, View, Text, Image, StyleSheet } from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';
import { useColorScheme } from 'react-native';
import { useRouter } from 'expo-router';

import { ThemedView } from '@/components/ThemedView';
import { ThemedText } from '@/components/ThemedText';
import Ionicons from '@expo/vector-icons/Ionicons';

import Button from '@/components/buttons/button';
import Input from '@/components/inputs/input';

const ArkoIcon = require('../../../../assets/images/appImg/arko-logo.png');

const ChangePassword = () =>{

    const router = useRouter();
    const colorScheme = useColorScheme();
    

    return(
        <SafeAreaView style={[style.container, {backgroundColor: colorScheme === 'dark' ? '#151718' : '#F6F6F6'}] }>
            <View style={style.top}>

                <View style={style.logocontainer}>
                    <Image source={ArkoIcon} style={style.logo}/>
                </View>

            </View>
            <View style={style.middle}>
                <View style={style.msg}>
                    <ThemedText type="fade">
                       Please enter your new password. We recommend that you choose a strong password that includes a mix of uppercase and lowercase letters, numbers, and special characters for enhanced security.
                    </ThemedText>
                </View>
                <View style={style.form}>
                    <View style={style.input}>
                        <Input label="New Password" iconName='key-outline'/>
                    </View>
                    <View style={style.input}>
                        <Input label="Confirm New Password" iconName='key-outline'/>
                    </View>
                </View>

                <View style={style.btn}>
                    <Button title="Update New Password" onPress={() => {router.replace("/(settings)/")}}/>
                </View>
            
            </View>
        
        </SafeAreaView>
    )
}

const style = StyleSheet.create({
    container:{
        paddingHorizontal: 20,
        flex: 1,
        alignContent: "center",
    },
    top:{
        marginTop: 5,
    },
    logocontainer:{
        flexDirection: "column",
        justifyContent: "center",
        alignContent: "center",
        alignItems: "center"
    },
    logo:{
        height: 175,
        width: 175,
    },
    title:{
        margin: 10,
        alignContent: "center",
        justifyContent: "center",
        alignItems: "center",
        textAlign: "center"
    },
    middle:{
        flexDirection: "column",
        justifyContent: "center",
        alignContent: "center",
        marginTop: 20,
    },
    form:{
        marginVertical: 10,
    },
    input:{
        justifyContent: "center",
        alignItems: "center",
        marginVertical: 13,
    },
    msg:{
        marginVertical: 15,
        marginHorizontal: 10,
        justifyContent: "flex-start",
        flexDirection: "row",
        alignContent: "center",
    },
    btn:{
        justifyContent: "center",
        alignItems: "center",
        marginVertical: 10,
    },

});

export default ChangePassword;