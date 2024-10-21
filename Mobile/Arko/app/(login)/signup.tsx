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

const ArkoIcon = require('../../assets/images/appImg/arko-logo.png');

const SignUp = () =>{

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
                <View style={style.form}>
                    <View style={style.input}>
                        <Input label="Username" iconName='person-outline'/>
                    </View>
                    <View style={style.input}>
                        <Input label="Password" iconName='key-outline' password/>
                    </View>

                    <View style={style.input}>
                        <Input label="Confirm Password" iconName='key-outline' password/>
                    </View>
                </View>

                <View style={style.btn}>
                    <Button title="Create an Account" onPress={() => {router.replace("/(home)")}}/>
                </View>

                <View style={style.dividerContainer}>
                    <View style={style.divider} />
                    <Text style={style.dividerText}>OR</Text>
                    <View style={style.divider} />
                </View>
                

                <View style={style.create}>
                    <ThemedText>Already have an Account? </ThemedText>
                        <TouchableOpacity onPress={() => router.navigate("/(login)")}>
                            <Text style={style.createAccountText}>Sign In</Text>
                        </TouchableOpacity>
                </View>
            </View>
        
        </SafeAreaView>
    )
}

const style = StyleSheet.create({
    container:{
        paddingHorizontal: 20,
        paddingVertical: 25,
        flex: 1,
        alignContent: "center",
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
    forgotlink:{
        marginTop: 3,
        marginBottom: 15,
        marginHorizontal: 10,
        justifyContent: "flex-start",
        flexDirection: "row",
        alignContent: "center",
    },
    btn:{
        justifyContent: "center",
        alignItems: "center",
    },
    dividerContainer: {
        flexDirection: 'row',
        alignItems: 'center',
        marginVertical: 50,
        marginHorizontal: 10,
    },
    divider: {
        flex: 1,
        height: 1,
        backgroundColor: '#B3B4BA',
    },
    dividerText: {
        marginHorizontal: 10,
        color: '#B3B4BA',
        fontFamily: "CeraPro"
    },
    create:{
        flexDirection: "row",
        justifyContent: "center",
        alignContent: "center",
        alignItems: "center"
    },

    createAccountText: {
        color: '#004495',
        textDecorationLine: 'underline',
        fontFamily: "CeraPro"
    },

});

export default SignUp;