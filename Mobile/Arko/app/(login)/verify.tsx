import * as React from 'react';
import { TouchableOpacity, View, Text, Image, StyleSheet } from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';
import { useColorScheme } from 'react-native';
import { useRouter } from 'expo-router';

;
import { ThemedText } from '@/components/ThemedText';

import Button from '@/components/buttons/button';
import { OtpInput } from 'react-native-otp-entry';


const ArkoIcon = require('../../assets/images/appImg/arko-logo.png');

const VerifyEmail = () =>{

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
                        The one-time pin verification code has been sent to your email, please check your inbox.
                    </ThemedText>
                </View>
                <View style={style.form}>
                    <View style={style.input}>
                        <OtpInput
                            numberOfDigits={6}
                            hideStick={true}
                            onTextChange={(text) => console.log(text)}
                            onFilled={(text) => console.log(`OTP is ${text}`)}
                            textInputProps={{
                                accessibilityLabel: "One-Time Password",
                            }}
                            theme={{
                                pinCodeContainerStyle: style.pinCodeContainer,
                                pinCodeTextStyle: {
                                    color: colorScheme === 'dark' ? '#F6F6F6' : '#151718',
                                    
                                },
                                focusedPinCodeContainerStyle: style.activePinCodeContainer,
                            }}
                            />
                    </View>
                    <View style={style.forgotlink}>
                    <TouchableOpacity onPress={() => {}}>
                        <Text style={{color: "#277CA5"}}>Resend Verification Code</Text>
                    </TouchableOpacity>
                    </View>
                </View>

                <View style={style.btn}>
                    <Button title="Verify Email Address" onPress={() => {router.replace("/(login)/reset")}}/>
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
    pinCodeContainer:{
        borderColor: "#808080",
        width: 50,
        height: 50,
        borderRadius: 10,
        justifyContent: "center",
        alignItems: "center",
        textAlign: "center",
        alignContent: "center"
    },
    activePinCodeContainer:{
        borderColor: '#3377DC'
    },
    forgotlink:{
        marginTop: 3,
        marginBottom: 15,
        marginHorizontal: 10,
        justifyContent: "flex-start",
        flexDirection: "row",
        alignContent: "center",
    },
});

export default VerifyEmail;