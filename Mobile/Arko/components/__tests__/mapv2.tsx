import * as React from 'react';
import { View, Text, StyleSheet, StatusBar, SafeAreaView, Button, Dimensions } from 'react-native';

import * as ScreenOrientation from 'expo-screen-orientation';
import * as Location from 'expo-location'

import { useColorScheme } from 'react-native';
import MapView, {Marker} from 'react-native-maps';
import BottomSheet, { BottomSheetView } from '@gorhom/bottom-sheet';
import { GestureHandlerRootView } from 'react-native-gesture-handler';

const { width } = Dimensions.get('window');


export default function Map() {

    const colorScheme = useColorScheme();

    const bottomSheetRef = React.useRef<BottomSheet>(null);
    const snapPoints = React.useMemo(() => ['5%', '35%'], []);

    const handleSheetChanges = React.useCallback((index: number) => {
        console.log('handleSheetChanges', index);
    }, []);
    
    const [userLocation, setUserLocation] = React.useState(null);
    const [mapRegion, setMapRegion] =  React.useState({
        latitude: 14.6495,
        longitude: 120.9832,
        latitudeDelta: 0.0922,
        longitudeDelta: 0.0421,
    });

    const getUserLocation = async () => {
        let { status } = await Location.requestForegroundPermissionsAsync();

        if (status !== 'granted') {
            console.log("Permission to access location was denied");
            return;
        }

        let location = await Location.getCurrentPositionAsync({ enableHighAccuracy: true });

        setMapRegion({
            latitude: location.coords.latitude,
            longitude: location.coords.longitude,
            latitudeDelta: 0.0922,
            longitudeDelta: 0.0421,
        });

        setUserLocation({
            latitude: location.coords.latitude,
            longitude: location.coords.longitude,
        });

        console.log(location.coords.latitude, location.coords.longitude);
    };

    React.useEffect(() => {
        getUserLocation();
    }, []);


    return (
        <GestureHandlerRootView>
        <SafeAreaView style={[style.container, {backgroundColor: colorScheme === 'dark' ? '#151718' : '#fff'}]}>
            <MapView style={style.map}
            initialRegion={mapRegion}
            >

            </MapView>
        </SafeAreaView>
        </GestureHandlerRootView>
    );
}


const style = StyleSheet.create({
    container:{
        flex: 1,
        justifyContent: "center",
        alignItems: "center"
    },
    map: {
        width: '100%',
        height: '100%',
    },
})


const bottomSheet = StyleSheet.create({
    contentContainer:{
        flex: 1,
    },
    handleStyle:{
        
    },
    handleIndicator:{
        width: 100,
    },
    header:{
        padding: 10,
        textAlign: "center",
        justifyContent: "center",
        alignContent: "center",
        alignItems: "center"
    },
    pinnedLoc:{

    },
})