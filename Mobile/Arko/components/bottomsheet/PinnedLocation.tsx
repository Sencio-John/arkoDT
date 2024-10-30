import React from 'react';
import { View, StyleSheet, Dimensions } from 'react-native';
import BottomSheet, { BottomSheetScrollView } from '@gorhom/bottom-sheet';
import { useColorScheme } from 'react-native';
import { ThemedText } from '../ThemedText';
import PinDetail from '../buttons/PinDetails';

const PinnedLocation = ({ bottomSheetRef, snapPoints, pinnedLocations, onPress, index, onClose = () => {} }) => {

    const colorScheme = useColorScheme();
    
    return (
        <BottomSheet
            ref={bottomSheetRef}
            snapPoints={snapPoints}
            handleStyle={[styles.handleStyle, {backgroundColor: colorScheme === 'dark' ? '#151718' : '#fff'}]}
            handleIndicatorStyle={[styles.handleIndicator, {backgroundColor: colorScheme === 'dark' ? '#fff' : '#151718'}]}
            index={index}
            enablePanDownToClose={true}
            onClose={onClose}
        >
            <BottomSheetScrollView contentContainerStyle={[styles.contentContainer, {backgroundColor: colorScheme === 'dark' ? '#151718' : '#fff'}]}>
                
                {/* Header */}
                <View style={styles.header}>
                    <ThemedText type="subtitle">Pinned Locations</ThemedText>
                </View>

                {/* Pinned Locations List */}
                {pinnedLocations.length === 0 ? (
                    <ThemedText type="med">No pinned locations yet</ThemedText>
                ) : (
                    pinnedLocations.map((location) => (
                        <PinDetail 
                            key={location.id}
                            icon_name="pin"
                            title={location.title}
                            subtitle={location.description}
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
        justifyContent: "center",
        alignContent: "center",
        alignItems: "center"
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
});

export default PinnedLocation;
