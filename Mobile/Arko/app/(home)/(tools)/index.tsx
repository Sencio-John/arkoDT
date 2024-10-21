import * as React from 'react';
import { View, Text, StyleSheet, StatusBar, SafeAreaView, Button, Dimensions } from 'react-native';
import * as Location from 'expo-location'

import { useColorScheme } from 'react-native';
import MapView, {Marker} from 'react-native-maps';
import BottomSheet, { BottomSheetView } from '@gorhom/bottom-sheet';
import { GestureHandlerRootView } from 'react-native-gesture-handler';
import MarkerModal from '@/components/modals/markerInsert';
import PinnedLocation from '@/components/bottomsheet/PinnedLocation';

export default function Map() {

    const colorScheme = useColorScheme();

    const bottomSheetRef = React.useRef<BottomSheet>(null);
    const snapPoints = React.useMemo(() => ["5%",'25%','50%'], []);
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
    

    const [markers, setMarkers] = React.useState([]);
    const [modalVisible, setModalVisible] = React.useState(false);
    const [markerCoordinate, setMarkerCoordinate] = React.useState(null);
    
    const handleLongPress = (event: { nativeEvent: { coordinate: any; }; }) => {
        const coordinate = event.nativeEvent.coordinate;
        setMarkerCoordinate(coordinate);
        setModalVisible(true);
    };

    const handleAddMarker = (title: string, description: string) => {
        bottomSheetRef.current?.snapToIndex(-1);
        if (markerCoordinate) {
            setMarkers((currentMarkers) => [
                ...currentMarkers,
                {
                    id: Math.random(), // Generate a unique ID
                    coordinate: markerCoordinate,
                    title,
                    description,
                },
            ]);
            
            setModalVisible(false);
            bottomSheetRef.current?.snapToIndex(1);
        } else{
            bottomSheetRef.current?.snapToIndex(1);
        }
    };



    return (
        <GestureHandlerRootView>
            <SafeAreaView style={[style.container, {backgroundColor: colorScheme === 'dark' ? '#151718' : '#fff'}]}>
                <MapView style={style.map}
                    region={mapRegion}
                    showsUserLocation={true}
                    followsUserLocation={true}
                    onPress={handleLongPress}
                >
                    {markers.map((marker) => (
                        <Marker
                        key={marker.id}
                        coordinate={marker.coordinate}
                        title={marker.title}
                        description={marker.description}
                        />
                    ))}
                </MapView>
                
                    <MarkerModal 
                        modalVisible={modalVisible}
                        onClose={() => {
                            setModalVisible(false)
                            bottomSheetRef.current?.snapToIndex(1);
                        }}
                        onAddMarker={handleAddMarker}
                    />

                    <PinnedLocation 
                        bottomSheetRef={bottomSheetRef} 
                        index={1}
                        snapPoints={snapPoints} 
                        pinnedLocations={markers}
                    />

                    
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
        flex: 1,
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