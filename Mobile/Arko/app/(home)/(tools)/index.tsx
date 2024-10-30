import * as React from 'react';
import { View, Text, StyleSheet, StatusBar, SafeAreaView, Button, Dimensions } from 'react-native';
import * as Location from 'expo-location'

import { useColorScheme } from 'react-native';
import MapView, {Marker} from 'react-native-maps';
import BottomSheet, { BottomSheetView } from '@gorhom/bottom-sheet';
import { GestureHandlerRootView } from 'react-native-gesture-handler';
import MarkerModal from '@/components/modals/markerInsert';
import PinnedLocation from '@/components/bottomsheet/PinnedLocation';
import PinsBtn from '@/components/buttons/Iconbtn';
import MarkerInfo from '@/components/bottomsheet/MarkerInfo';

export default function Map() {

    const colorScheme = useColorScheme();

    const bottomSheetRef = React.useRef<BottomSheet>(null);
    const MapInfoRef = React.useRef<BottomSheet>(null);
    const snapPoints = React.useMemo(() => ["1%", "5%",'25%','35%'], []);
    const [isBottomSheetOpen, setIsBottomSheetOpen] = React.useState(false);
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
        bottomSheetRef.current?.close();
        MapInfoRef.current?.close();;
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
            bottomSheetRef.current?.snapToIndex(2);
        } else{
            bottomSheetRef.current?.snapToIndex(2);
        }
    };

    const toggleBottomSheet = () =>{
        if (isBottomSheetOpen) {
            bottomSheetRef.current?.close();
        } else {
            bottomSheetRef.current?.snapToIndex(2);
        }
        setIsBottomSheetOpen(!isBottomSheetOpen);
    }

    const getAddressFromCoordinates = async (latitude: any, longitude: any) => {
        try {
            const result = await Location.reverseGeocodeAsync({ latitude, longitude });
            if (result.length > 0) {
                const { street, city, region, country } = result[0];
                return `${street}, ${city}, ${region}, ${country}`; // Format the address as needed
            }
            return null;
        } catch (error) {
            console.error("Error getting address:", error);
            return null;
        }
    };

    const seeMarkerInfo = async (markerId: any) => {
    const marker = markers.find((m) => m.id === markerId);
    if (marker) {
        const address = await getAddressFromCoordinates(marker.coordinate.latitude, marker.coordinate.longitude);
        
        setSelectedMarker({ ...marker, address }); 

        setMapRegion({
            ...mapRegion,
            latitude: marker.coordinate.latitude,
            longitude: marker.coordinate.longitude,
        });

    
        bottomSheetRef.current?.close();
        MapInfoRef.current?.snapToIndex(3); 
    }
};


    const [selectedMarker, setSelectedMarker] = React.useState(null);
    const handleMarkerPress = async (marker: any) => {
        const address = await getAddressFromCoordinates(marker.coordinate.latitude, marker.coordinate.longitude);
        setSelectedMarker({ ...marker, address }); 
        bottomSheetRef.current?.close(); 
        MapInfoRef.current?.snapToIndex(3); 
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
                        onPress={() => handleMarkerPress(marker)}
                        />
                    ))}
                    
                </MapView>
                <View style={style.pinbtn}>
                    <PinsBtn iconName='pin' onPress={toggleBottomSheet}/>
                </View>
                
                
                    <MarkerModal 
                        modalVisible={modalVisible}
                        onClose={() => {
                            setModalVisible(false)
                            bottomSheetRef.current?.expand;
                        }}
                        onAddMarker={handleAddMarker}
                    />

                    <PinnedLocation 
                        bottomSheetRef={bottomSheetRef} 
                        snapPoints={snapPoints} 
                        pinnedLocations={markers}
                        onPress={seeMarkerInfo}
                        onClose={() => setIsBottomSheetOpen(false)}
                        index={-1}
                    />

                    <MarkerInfo 
                        bottomSheetRef={MapInfoRef}
                        snapPoints={snapPoints}
                        index={-1}
                        address={selectedMarker ? selectedMarker.address : null} // Use the physical address
                        description={selectedMarker ? selectedMarker.description : ''} 
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
    pinbtn:{
        position: "absolute",
        top: 75,
        right: 10,
    }
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

function getAddressFromCoordinates(latitude: any, longitude: any) {
    throw new Error('Function not implemented.');
}
