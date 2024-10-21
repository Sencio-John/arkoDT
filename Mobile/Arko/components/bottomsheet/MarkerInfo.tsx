import React from 'react';
import { View, Text, StyleSheet } from 'react-native';
import BottomSheet, { BottomSheetView } from '@gorhom/bottom-sheet';
import { Ionicons } from '@expo/vector-icons'; // Use Ionicons for icons
import { ThemedText } from '../ThemedText'; // Assuming you're using ThemedText for styled text

import { useColorScheme } from 'react-native';

const TrackingInformationBottomSheet = ({ bottomSheetRef, snapPoints, onChange }) => {

    const colorScheme = useColorScheme();

    return (
        <BottomSheet
            ref={bottomSheetRef}
            snapPoints={snapPoints}
            handleStyle={[styles.handleStyle, {backgroundColor: colorScheme === 'dark' ? '#151718' : '#fff'}]}
            handleIndicatorStyle={[styles.handleIndicator, {backgroundColor: colorScheme === 'dark' ? '#fff' : '#151718'}]}
            onChange={onChange}
        >
            <BottomSheetView style={[styles.contentContainer, {backgroundColor: colorScheme === 'dark' ? '#151718' : '#fff'}]}>
            {/* Header */}
                <View style={styles.header}>
                    <ThemedText type="subtitle">Marker Information</ThemedText>
                </View>

            {/* Content */}
                <View style={styles.infoContainer}>
                
                    <View style={styles.infoRow}>
                        <ThemedText>
                            <Ionicons name="location" style={styles.icon} />
                        </ThemedText>
                        <View style={styles.infoTextContainer}>
                            <Text style={styles.infoTitle}>Location Address</Text>
                            <ThemedText type="fade" style={styles.infoSubtitle}>21 St. Brgy 14, Dagat-Dagatan Caloocan City</ThemedText>
                        </View>
                    </View>

                    {/* Population */}
                    <View style={styles.infoRow}>
                        <ThemedText>
                            <Ionicons name="information-circle" style={styles.icon} />
                        </ThemedText>
                        <View style={styles.infoTextContainer}>
                            <Text style={styles.infoTitle}>Request Details</Text>
                            <ThemedText type="fade" style={styles.infoSubtitle}>Food Supplies Needed</ThemedText>
                        </View>
                    </View>
                </View>
            </BottomSheetView>
        </BottomSheet>
  );
};

const styles = StyleSheet.create({
    contentContainer: {
        flex: 1,
        padding: 16,
    },
    header: {
        marginBottom: 16,
        alignItems: 'center',
    },
    infoContainer: {
        marginVertical: 8,
    },
    infoRow: {
        flexDirection: 'row',
        alignItems: 'center',
        marginVertical: 10,
    },
    infoTextContainer: {
        marginLeft: 10,
    },
    infoTitle: {
        fontWeight: 'bold',
        fontSize: 16,
        color: "#277CA5"
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

export default TrackingInformationBottomSheet;
