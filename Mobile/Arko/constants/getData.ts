import { child, Database, get, ref } from "firebase/database";
import CryptoJS from "crypto-js";
import AsyncStorage from "@react-native-async-storage/async-storage";
import { database } from "./firebase";

const getData = async(PASS_KEY: string) =>{

    const dbRef = ref(database);
    console.log(PASS_KEY)
    const snapshot = await get(child(dbRef, `appLog/${PASS_KEY}`));
    console.log("snapshot:", snapshot)
    if (snapshot.exists()){
        const foundUser = snapshot.val();
        const decryptedData = {
            Email: CryptoJS.AES.decrypt(foundUser.Email, 'email').toString(CryptoJS.enc.Utf8),
            Name: CryptoJS.AES.decrypt(foundUser.Name, 'name').toString(CryptoJS.enc.Utf8),
            Username: CryptoJS.AES.decrypt(foundUser.Username, 'username').toString(CryptoJS.enc.Utf8),
        };

        console.log(foundUser);
        return decryptedData;
    }

}

export default getData;