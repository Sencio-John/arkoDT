
import CryptoJS from "crypto-js";
import AsyncStorage from "@react-native-async-storage/async-storage";

const getData = async(key: string) =>{

    const email = await AsyncStorage.getItem("encryptedEmail");
    const name = await AsyncStorage.getItem("encryptedName");
    const username = await AsyncStorage.getItem("encryptedUsername");

    const decryptedData = {
        Email: CryptoJS.AES.decrypt(email, key).toString(CryptoJS.enc.Utf8),
        Name: CryptoJS.AES.decrypt(name, key).toString(CryptoJS.enc.Utf8),
        Username: CryptoJS.AES.decrypt(username, key).toString(CryptoJS.enc.Utf8),
    };

    return decryptedData;
}

export default getData;