import React from 'react';
import { View, Text, StyleSheet, ScrollView  } from 'react-native';
import BottomSheet, { BottomSheetView } from '@gorhom/bottom-sheet';
import { Ionicons } from '@expo/vector-icons'; // Use Ionicons for icons
import { ThemedText } from '../ThemedText'; // Assuming you're using ThemedText for styled text

import { useColorScheme } from 'react-native';
import PinDetail from '../buttons/PinDetails';

const PinnedLocation = ({ bottomSheetRef, snapPoints, pinnedLocations, index}) => {

    const colorScheme = useColorScheme();

    return (
        <BottomSheet
            ref={bottomSheetRef}
            snapPoints={snapPoints}
            handleStyle={[styles.handleStyle, {backgroundColor: colorScheme === 'dark' ? '#151718' : '#fff'}]}
            handleIndicatorStyle={[styles.handleIndicator, {backgroundColor: colorScheme === 'dark' ? '#fff' : '#151718'}]}
            index={index}
            enableHandlePanningGesture={false}
        >
            <BottomSheetView style={[styles.contentContainer, {backgroundColor: colorScheme === 'dark' ? '#151718' : '#fff'}]}>
            {/* Header */}
                <View style={styles.header}>
                    <ThemedText type="subtitle">Pinned Locations</ThemedText>
                </View>

            {/* Content */}
                
                <ScrollView contentContainerStyle={styles.info} style={styles.scrollView} nestedScrollEnabled>

                    {pinnedLocations.length === 0 ? (
                        <ThemedText type="title">No pinned locations yet</ThemedText>
                        ) : (
                            pinnedLocations.map((location: { id: React.Key | null | undefined; title: any; description: any; }) => (
                                <PinDetail 
                                    key={location.id}
                                    icon_name="pin"
                                    title={location.title}
                                    subtitle={location.description}
                                />
                            ))
                    )}
                
                </ScrollView>
            </BottomSheetView>
        </BottomSheet>
  );
};

const styles = StyleSheet.create({
    contentContainer: {
        flex: 1,
        padding: 16,
        justifyContent: "center",
    },
    scrollView: {
        flexGrow: 1, 
    },
    header: {
        marginBottom: 16,
        alignItems: 'center',
    },
    info: {
        flexDirection: 'column',
        justifyContent: 'flex-start',
        alignItems: 'center',
        marginVertical: 10,
    },
    infoSubtitle: {
        fontSize: 14,
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
    icon:{
        fontSize: 24,
        color: "#277CA5"
    },
});

export default PinnedLocation;
