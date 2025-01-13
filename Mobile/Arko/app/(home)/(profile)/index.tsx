
import * as React from 'react';
import { TouchableOpacity, View, Text, Image, StyleSheet, BackHandler, Alert } from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';
import { useRouter } from 'expo-router';
import { useColorScheme } from 'react-native';
import { Ionicons } from '@expo/vector-icons';

import InputV2 from '@/components/inputs/input2';
import Button from '@/components/buttons/button';
import DatePicker from '@/components/inputs/datePicker';
import getData from '@/constants/getData';
import AsyncStorage from '@react-native-async-storage/async-storage';
import EditProfileModal from '@/components/modals/choose';

import { launchCamera, launchImageLibrary } from 'react-native-image-picker';

const Profile = () =>{

    const colorScheme = useColorScheme();

    const [modalOpen, setModalOpen] = React.useState(false)
    const [profileImage, setProfileImage] = React.useState(null);
    const [userData, setUserData] = React.useState(null)

    React.useEffect(() => {
        const fetchData = async () => {
            const key = await AsyncStorage.getItem('key');
            const data = await getData(key);
            setUserData(data);
            console.log(data)
        };

        fetchData();
    }, [])

    const [birthDate, setBirthDate] = React.useState<Date | null>(null);

    const [inputs, setInputs] = React.useState({
        first_name: "",
        last_name: "",
        address: "",
        contact: "",
        birthdate: "",
    })

    const [error, setError] = React.useState({
        first_name: "",
        last_name: "",
        address: "",
        contact: "",
        birthdate: "",
    });

    const handleOnChange = (text: string, input: string) => {
        setInputs((prevState) => ({ ...prevState, [input]: text.trim() }));
    };

    const handleError = (text: string | null, input: string) => {
        setError((prevState) => ({ ...prevState, [input]: text }));
    };

    const toggleModal = async() => {
        setModalOpen(!modalOpen)
    }
    

    const handleCamera = async() => {
        const result = await launchCamera();
        setProfileImage(result?.assets[0]?.uri)
    };


    const handleGallery = async() => {
        const result = await launchImageLibrary();
        setProfileImage(result?.assets[0]?.uri)
    };

    return(
        <SafeAreaView style={[style.container, {backgroundColor: colorScheme === 'dark' ? '#151718' : '#F6F6F6'}] }>
            <View style={style.profilegrp}>
                <View style={style.profileContainer}>
                    <Image style={style.profile} source={require('../../../assets/images/arko_icon.png')}/>
                    {/* <View style={style.btnContainer}>
                        <TouchableOpacity style={style.iconBtn} onPress={() => {toggleModal()} }>
                            <Ionicons name='pencil' style={style.icon} />
                        </TouchableOpacity>
                    </View> */}
                </View>
            </View>
            <View style={style.form}>

                <View style={style.input}>
                    <InputV2 
                        label="First Name" 
                        iconName='people' 
                        onChangeText={(text) => handleOnChange(text, "first_name")} 
                        error={error.first_name}
                        item={userData?.First_Name}
                        />
                </View>
                <View style={style.input}>
                    <InputV2 
                        label="Last Name" 
                        iconName='people' 
                        onChangeText={(text) => handleOnChange(text, "last_name")} 
                        error={error.last_name}
                        item={userData?.Last_Name}
                        />
                </View>
                <View style={style.input}>
                    <InputV2 
                        label="Address" 
                        iconName='home' 
                        onChangeText={(text) => handleOnChange(text, "address")} 
                        error={error.address}
                        item={userData?.Address}
                        />
                </View>
                <View style={style.input}>
                    <InputV2
                        label="Contact No." 
                        iconName='call' 
                        onChangeText={(text) => handleOnChange(text, "contact")} 
                        error={error.contact}  
                        item={userData?.Phone}
                        />
                </View>

                <View style={style.input}>
                    <DatePicker 
                        label='Birth Date'
                        value={birthDate}
                        onDateChange={(date) => setBirthDate(date)}
                    />
                </View>

                <View style={style.btnGrp}>
                    <View style={style.btn}>
                        <Button title="Save Changes" />
                    </View>
                </View>

                <EditProfileModal modalVisible={modalOpen} onClose={() => setModalOpen(false)} openCamera={handleCamera} openGallery={handleGallery}/>
            </View>

            
        </SafeAreaView>
    )
}

const style = StyleSheet.create({
    container: {
        backgroundColor: "white",
        flex: 1,
    },
    profilegrp:{
        flexDirection: "column",
        alignContent: "center",
        alignItems: "center"
    },
    profileContainer: {
        height: 150,
        width: 150,
        overflow: "hidden",
        marginRight: 15,
        alignItems: "center",
        alignContent: "center",
        justifyContent: "center"
    },
    profile: {
        width: 150,
        borderRadius: 75,
        height: 150,
    },
    btnContainer:{
        position: "absolute",
        bottom: 0,
        right: 5,
    },
    iconBtn:{
        backgroundColor: "#277CA5",
        justifyContent: "center",
        alignContent: "center",
        alignItems: "center",
        padding: 10,
        borderRadius: 50,
    },
    icon:{
        color: "#fff",
        fontSize: 24,
    },
    form:{
        marginVertical: 20,
        marginHorizontal: 20,
    },
    input:{
        justifyContent: "center",
        alignItems: "center",
        marginVertical: 10,
    },
    btnGrp:{
        marginVertical: 10,
    },
    btn:{
        justifyContent: "center",
        alignItems: "center"
    }
});

export default Profile;