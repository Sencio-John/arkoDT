import React from 'react';
import { View, Text, StyleSheet, TouchableOpacity } from 'react-native';
import BottomSheet, { BottomSheetView } from '@gorhom/bottom-sheet';
import { Ionicons } from '@expo/vector-icons'; // Use Ionicons for icons
import { ThemedText } from '../ThemedText'; // Assuming you're using ThemedText for styled text

import { useColorScheme } from 'react-native';
import Button from '../buttons/button';

const MarkerInfo = ({ bottomSheetRef, snapPoints, address, description, index, dateAdded, name, type, onClose = () => {}}) => {

    const colorScheme = useColorScheme();

    const handleClose = () => {
        if (bottomSheetRef.current) {
            bottomSheetRef.current.close(); // Manually close the BottomSheet
        }
        onClose(); // Call the onClose callback passed in as a prop
    };

    return (
        <BottomSheet
            ref={bottomSheetRef}
            snapPoints={snapPoints}
            handleStyle={[styles.handleStyle, {backgroundColor: colorScheme === 'dark' ? '#151718' : '#fff'}]}
            handleIndicatorStyle={[styles.handleIndicator, {backgroundColor: colorScheme === 'dark' ? '#fff' : '#151718'}]}
            backgroundStyle={{backgroundColor: colorScheme === 'dark' ? '#fff' : '#151718'}}
            index={index}
            enablePanDownToClose={true}
            onClose={onClose}
        >
            <BottomSheetView style={[styles.contentContainer, {backgroundColor: colorScheme === 'dark' ? '#151718' : '#fff'}]}>

                <View style={styles.closeBtn}>
                    <TouchableOpacity onPress={handleClose}>
                        <Ionicons name="close-circle" style={styles.closeIcon} />
                    </TouchableOpacity>
                </View>
            {/* Header */}
                <View style={styles.header}>
                    <ThemedText type="subtitle">Marker Information</ThemedText>
                </View>

            {/* Content */}
                <View style={styles.infoContainer}>

                    <View style={styles.infoRow}>
                        <ThemedText>
                            <Ionicons name="information-circle" style={styles.icon} />
                        </ThemedText>
                        <View style={styles.infoTextContainer}>
                            <Text style={styles.infoTitle}>Type</Text>
                            <ThemedText type="fade">{type}</ThemedText>
                        </View>
                    </View>
                
                    <View style={styles.infoRow}>
                        <ThemedText>
                            <Ionicons name="location" style={styles.icon} />
                        </ThemedText>
                        <View style={styles.infoTextContainer}>
                            <Text style={styles.infoTitle}>Location Address</Text>
                            <ThemedText type="fade">{address}</ThemedText>
                        </View>
                    </View>

                    <View style={styles.infoRow}>
                        <ThemedText>
                            <Ionicons name="people" style={styles.icon} />
                        </ThemedText>
                        <View style={styles.infoTextContainer}>
                            <Text style={styles.infoTitle}>Name</Text>
                            <ThemedText type="fade">{name}</ThemedText>
                        </View>
                    </View>

                    <View style={styles.infoRow}>
                        <ThemedText>
                            <Ionicons name="information-circle" style={styles.icon} />
                        </ThemedText>
                        <View style={styles.infoTextContainer}>
                            <Text style={styles.infoTitle}>Description</Text>
                            <ThemedText type="fade">{description}</ThemedText>
                        </View>
                    </View>

                    <View style={styles.infoRow}>
                        <ThemedText>
                            <Ionicons name="calendar" style={styles.icon} />
                        </ThemedText>
                        <View style={styles.infoTextContainer}>
                            <Text style={styles.infoTitle}>Date Added:</Text>
                            <ThemedText type="fade">{dateAdded}</ThemedText>
                        </View>
                    </View>
                </View>
                <View style={styles.btnContainer}>
                    <View style={styles.btn}>
                        <Button title="Complete Task or Mission"/>
                    </View>
                    <View style={styles.btn}>
                        <Button title="Remove Marker" type='danger'/>
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
    btnContainer:{
        flexDirection: 'column',
    },
    closeBtn:{
        position: 'absolute',
        right: 10,
    },
    closeIcon:{
        fontSize: 26,
        color: "#4d4d4d",
    },
    btn:{
        marginVertical: 5,
        alignItems: 'center',
    }
});

export default MarkerInfo;
