import React from 'react';
import { View, Text, StyleSheet, TouchableOpacity } from 'react-native';
import BottomSheet, { BottomSheetScrollView, BottomSheetView } from '@gorhom/bottom-sheet';
import { Ionicons } from '@expo/vector-icons'; // Use Ionicons for icons
import { ThemedText } from '../ThemedText'; // Assuming you're using ThemedText for styled text

import { useColorScheme } from 'react-native';
import Button from '../buttons/button';
import PinDetail from '../buttons/PinDetails';

interface MarkerInfoProps {
    bottomSheetRef: React.RefObject<BottomSheet>;
    snapPoints: number[];
    doneMarker: () => void;
    delMarker: () => void;
    onClose?: () => void;
    pinnedLocations: any;
    onPress?: () => void
}

const ArchivedInfoSheet: React.FC<MarkerInfoProps> = ({ bottomSheetRef, snapPoints, pinnedLocations, onClose = () => {}, onPress}) => {

    const colorScheme = useColorScheme();

    const handleClose = () => {
        if (bottomSheetRef.current) {
            bottomSheetRef.current.close();
        }
        onClose(); 
    };


    return (
        <BottomSheet
            ref={bottomSheetRef}
            snapPoints={snapPoints}
            handleStyle={[styles.handleStyle, {backgroundColor: colorScheme === 'dark' ? '#151718' : '#fff'}]}
            handleIndicatorStyle={[styles.handleIndicator, {backgroundColor: colorScheme === 'dark' ? '#fff' : '#151718'}]}
            backgroundStyle={{backgroundColor: colorScheme === 'dark' ? '#fff' : '#151718'}}
            enablePanDownToClose={true}
            onClose={onClose}
        >
            <BottomSheetScrollView contentContainerStyle={[styles.contentContainer, {backgroundColor: colorScheme === 'dark' ? '#151718' : '#fff'}]}>
                <View style={styles.closeBtn}>
                    <TouchableOpacity onPress={handleClose}>
                        <Ionicons name="close-circle" style={styles.closeIcon} />
                    </TouchableOpacity>
                </View>
                {/* Header */}
                <View style={styles.header}>
                    <ThemedText type="subtitle">Archived Pinned Locations</ThemedText>
                </View>

                {/* Pinned Locations List */}
                {pinnedLocations.length === 0 ? (
                    <ThemedText type="med">No archived pinned locations yet</ThemedText>
                ) : (
                    pinnedLocations.map((location) => (
                        <PinDetail 
                            key={location.id}
                            icon_name="checkmark-circle"
                            title={location.title}
                            subtitle={"Name: " + location.name}
                            onPress={() => onPress(location.id)}
                        />
                    ))
                )}
                
            </BottomSheetScrollView>
        </BottomSheet>
    );
};

const styles = StyleSheet.create({
    contentContainer: {
        padding: 16,
        alignContent: "center",
        alignItems: "center",
        flex: 1,
    },
    header: {
        marginBottom: 16,
        alignItems: 'center',
    },
    handleStyle: {
        borderTopLeftRadius: 12,
        borderTopRightRadius: 12,
    },
    handleIndicator: {
        width: 40,
        height: 6,
        borderRadius: 3,
        backgroundColor: '#ccc',
        alignSelf: 'center',
        marginTop: 6,
    },
    closeBtn:{
      position: 'absolute',
        right: 10,
    },
    closeIcon:{
        fontSize: 26,
        color: "#4d4d4d",
    },
});

export default ArchivedInfoSheet;
