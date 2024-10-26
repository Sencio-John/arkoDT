import * as React from 'react';
import { TouchableOpacity, View, Text, Image, StyleSheet, Alert } from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';
import { useColorScheme } from 'react-native';
import { useRouter } from 'expo-router';

import Button from '@/components/buttons/button';
import Input from '@/components/inputs/input';

import { ref, get, child, onValue, set } from 'firebase/database';
import { database } from '@/constants/firebase';
import CryptoJS from "crypto-js";
import AsyncStorage from '@react-native-async-storage/async-storage';

const ArkoIcon = require('../../assets/images/appImg/arko-logo.png');

const LoginScreen = () =>{


    const router = useRouter();
    const colorScheme = useColorScheme();
    const [errorMessage, setErrorMessage] = React.useState('');
    const [inputs, setInputs] = React.useState({
        username: "",
        password: "",
    })

    const [error, setError] = React.useState({
        username: "",
        password: "",
    });

    const handleOnChange = (text: string, input: string) => {
        setInputs((prevState) => ({ ...prevState, [input]: text.trim() }));
    };

    const handleError = (text: string | null, input: string) => {
        setError((prevState) => ({ ...prevState, [input]: text }));
    };

    const CheckLoginAuth = async() => {
        let isValid = false

        if(!inputs.username){
            handleError("Username is required!", "username")
            isValid = false
        } else{
            handleError("", "username")
            isValid = true
        }

        if(!inputs.password){
            handleError("Password is required", "password")
            isValid = false
        } else{
            handleError("", "password")
            isValid = true
        }

        if(isValid){
            try{
                // FIREBASE
                const username = inputs.username;
                const password = inputs.password;

                const dbRef = ref(database);
                const snapshot = await get(child(dbRef, 'Users'));

                if (snapshot.exists()) {
                    const users = snapshot.val();
                    
                    let foundUser = null;

                    for (const userId in users) {
                        if (users[userId].Username === username) {
                            foundUser = users[userId];
                            break;
                        }
                    }

                    if (foundUser) {

                        if (foundUser.Password === password) {
                            SavedLogIn(foundUser.Email, foundUser.Name)
                            Alert.alert('Login Successfully', "Welcome to ARKO")
                            LoginSuccess();

                        } else {
                            Alert.alert('Error', 'Incorrect password');
                        }
                    } else {
                        Alert.alert('Error', 'Username not found');
                    }

                } else {
                    Alert.alert('Error', 'Account does not exist');
                }

            } catch(error){ 
                Alert.alert('', error.message);
                console.log(error);
            }

        }
        
        
    }

    const LoginSuccess = () =>{
        router.replace("/(home)/")
        setInputs({username: "", password: "",})
    }

    const SavedLogIn = async(email: any, name: any) =>{

        const NAME = CryptoJS.AES.encrypt(name, "name").toString();
        const EMAIL = CryptoJS.AES.encrypt(email, "email").toString();
        const USERNAME = CryptoJS.AES.encrypt(inputs.username, "username").toString();
        const ENCRYPTED_KEY = CryptoJS.lib.WordArray.random(20).toString(CryptoJS.enc.Hex);
        const LOG_KEY = CryptoJS.lib.WordArray.random(10).toString(CryptoJS.enc.Hex);

        set(ref(database, 'appLog/' + LOG_KEY), {
            Name: NAME,
            Email: EMAIL,
            Username: USERNAME,
            CDN_KEY: ENCRYPTED_KEY,
        })

        await AsyncStorage.setItem("log_key", LOG_KEY)
    }

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
                        <Input label="Username" iconName='person-outline'  onChangeText={(text) => handleOnChange(text, "username")} error={error.username}/>
                    </View>
                    <View style={style.input}>
                        <Input label="Password" iconName='key-outline' password onChangeText={(text) => handleOnChange(text, "password")} error={error.password}/>
                    </View>
                </View>
                <View style={style.forgotlink}>
                    <TouchableOpacity onPress={() => {router.navigate("/(login)/forgot")}}>
                        <Text style={{color: "#277CA5"}}>Forgot Password?</Text>
                    </TouchableOpacity>
                </View>

                <View style={style.btn}>
                    <Button title="Login" onPress={() => CheckLoginAuth()}/>
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
        marginTop: 30,
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
        marginTop: 30,
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

export default LoginScreen;