import { ThemedView } from '@/components/ThemedView';
import { useRouter } from 'expo-router';
import React from 'react';
import { View, Text, StyleSheet, SafeAreaView, useColorScheme } from 'react-native';

import MenuDivider from '@/components/buttons/MenuDivider';
import { ThemedText } from '@/components/ThemedText';

export default function Settings() {

    const router = useRouter();
    const colorScheme = useColorScheme();


    const handleLogOut = () =>{
        router.replace("/(login)");
    }
    
    return(
        <SafeAreaView style={[style.container, {backgroundColor: colorScheme === 'dark' ? '#151718' : '#F6F6F6'}]}>
            <ThemedView>
                <View style={style.grp}>
                    <View style={style.divider}>
                        <ThemedText type='fade'>Privacy Settings</ThemedText>
                    </View>
                    <MenuDivider title="Change Username" icon_name="person" onPress={() => {router.navigate("/(changeuser)/")}}/>
                    <MenuDivider title="Change Password" icon_name="key" onPress={() => {router.navigate("/(changepass)/")}}/>
                    <MenuDivider title="Change Email" icon_name="mail" onPress={() => {router.navigate("/(changeemail)/")}}/>
                </View>
                <View style={style.grp}>
                    <View style={style.divider}>
                        <ThemedText type='fade'>App Settings</ThemedText>
                    </View>
                    <MenuDivider title="Theme" icon_name="contrast" onPress={() => {}}/>
                </View>
                <View style={style.grp}>
                    <View style={style.divider}>
                        <ThemedText type='fade'>Account Settings</ThemedText>
                    </View>
                    <MenuDivider title="Log Out" icon_name="log-out" onPress={() => {handleLogOut()}}/>
                </View>
            </ThemedView>
        </SafeAreaView>
    )
}

const style = StyleSheet.create({
    container:{
        flex: 1,
        backgroundColor: "#fff",
        paddingTop: 20,
    },
    grp:{
        marginBottom: 10,
    },
    divider:{
        margin: 10,
    }
})
