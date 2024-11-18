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

import { database } from '@/constants/firebase';
import { ref, get, child, set} from 'firebase/database';

export default function Map() {

    const colorScheme = useColorScheme();

    const bottomSheetRef = React.useRef<BottomSheet>(null);
    const MapInfoRef = React.useRef<BottomSheet>(null);
    const snapPoints = React.useMemo(() => ["1%", "5%",'25%','35%', '50%', '75%', '90%'], []);
    const InfosnapPoints = React.useMemo(() => ["1%", "5%",'25%','35%', '50%', '75%'], []);
    const [isBottomSheetOpen, setIsBottomSheetOpen] = React.useState(false);
    const [detailBottomSheetOpen,  setDetailBottomSheetOpen] = React.useState(false);
    const [userLocation, setUserLocation] = React.useState(null);
    const [mapRegion, setMapRegion] =  React.useState({
        latitude: 14.6495,
        longitude: 120.9832,
        latitudeDelta: 0.0922,
        longitudeDelta: 0.0421,
    });

    const [markers, setMarkers] = React.useState([]);

    const fetchPinnedData = async() =>{
        const dbRef = ref(database);
        const snapshot = await get(child(dbRef, 'Pinned'));
        
        if(snapshot.exists()){
            const data = snapshot.val();
            const formattedMarkers = Object.keys(data).map(key => ({
                id: key,
                coordinate: {
                    latitude: parseFloat(data[key].Latitude),
                    longitude: parseFloat(data[key].Longitude),
                },
                title: data[key].Type, 
                description: data[key].Description,
                status: data[key].Status,
                dateAdded: data[key].DateAdded,
                name: data[key].Name,
            }));

            setMarkers(formattedMarkers)
        }

    }

    const getUserLocation = async () => {
        let { status } = await Location.requestForegroundPermissionsAsync();

        if (status !== 'granted') {
            console.log("Permission to access location was denied");
            return;
        }

        let location = await Location.getCurrentPositionAsync({ enableHighAccuracy: true });

        setUserLocation({
            latitude: location.coords.latitude,
            longitude: location.coords.longitude,
        });
    };


    const [modalVisible, setModalVisible] = React.useState(false);
    const [markerCoordinate, setMarkerCoordinate] = React.useState(null);
    
    const handleLongPress = (event: { nativeEvent: { coordinate: any; }; }) => {
        const coordinate = event.nativeEvent.coordinate;
        setMarkerCoordinate(coordinate);
        setModalVisible(true);
        bottomSheetRef.current?.close();
        MapInfoRef.current?.close();
    };

    const handleAddMarker = async(title: string, description: string, fam_name: string) => {

        const newMarkerId = "ARKO-" + Math.floor(Math.random() * 9999999);
        const newMarker = {
            id: newMarkerId,
            Latitude: markerCoordinate.latitude,
            Longitude: markerCoordinate.longitude,
            Type: title,
            Description: description,
            Status: "Pending", 
            DateAdded: new Date().toLocaleDateString('en-US', {
                month: 'short', 
                day: 'numeric', 
                year: 'numeric',
            }),
            Name: fam_name,
        };


        const markerRef = ref(database, `Pinned/${newMarkerId}`);

        await set(markerRef, newMarker);

        setMarkers(prevMarkers => [...prevMarkers, {
            id: newMarkerId,
            coordinate: markerCoordinate,
            title,
            description,
            status: "Pending",
            dateAdded: newMarker.DateAdded,
            name: fam_name,
        }]);

        setModalVisible(false);
        bottomSheetRef.current?.snapToIndex(2);
    };

    const toggleBottomSheet = () =>{
        MapInfoRef.current?.close();
        if (isBottomSheetOpen) {
            bottomSheetRef.current?.close();
        } else {
            bottomSheetRef.current?.snapToIndex(4);
        }
        setIsBottomSheetOpen(!isBottomSheetOpen);
    }

    const DetailtoggleBottomSheet = () =>{
        bottomSheetRef.current?.close();
        if (!detailBottomSheetOpen) {
            MapInfoRef.current?.snapToIndex(5);
        }   
        setDetailBottomSheetOpen(!detailBottomSheetOpen);
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
            MapInfoRef.current?.snapToIndex(4); 
            setModalVisible(false);
        }
    };


    const [selectedMarker, setSelectedMarker] = React.useState(null);
    const handleMarkerPress = async (marker: any) => {
        const address = await getAddressFromCoordinates(marker.coordinate.latitude, marker.coordinate.longitude);
        setSelectedMarker({ ...marker, address }); 
        bottomSheetRef.current?.close(); 
        DetailtoggleBottomSheet()
        setModalVisible(false);
    };


    React.useEffect(() => {
        getUserLocation();
        fetchPinnedData();
    }, []);

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
                        snapPoints={InfosnapPoints}
                        index={-1}
                        address={selectedMarker ? selectedMarker.address : ""} // Use the physical address
                        description={selectedMarker ? selectedMarker.description : ""} 
                        name={selectedMarker ? selectedMarker.name : "" }
                        dateAdded={selectedMarker ? selectedMarker.dateAdded : ""}
                        type={selectedMarker ? selectedMarker.title : ""}
                        onClose={() => {
                            MapInfoRef.current?.close(); 
                            setDetailBottomSheetOpen(false);
                        }}
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

