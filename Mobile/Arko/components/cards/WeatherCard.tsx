import React, { useState, useEffect } from 'react';
import { View, Text, StyleSheet, Image } from 'react-native';
import axios from 'axios';
import { ThemedText } from '../ThemedText';
import { ThemedView } from '../ThemedView';
import { Ionicons } from '@expo/vector-icons';
import { MaterialIcons } from '@expo/vector-icons';

import WeatherCardLoader from './WeatherCardLoading';

interface WeatherData {
  main: {
    temp: number;
    temp_min: number;
    temp_max: number;
    humidity: number;
  };
  weather: {
    description: string;
    icon: string;
  }[];
  wind: {
    speed: number;
  };
  clouds: {
    all: number;
  }[];
}


export default function WeatherCard() {
  const [weatherData, setWeatherData] = useState<WeatherData | null>(null);
  const city = 'Caloocan'; // City name

  useEffect(() => {
    const fetchWeather = async () => {
      const apiKey = 'f1e54b0821efe234eaae9b9fcb6a4d55';  // Insert your OpenWeatherMap API key here
      const url = `http://api.openweathermap.org/data/2.5/weather?q=${city}&appid=${apiKey}&units=metric`;

      try {
        const response = await axios.get(url);
        setWeatherData(response.data);
      } catch (error) { 
        console.error("Error fetching weather data:", error);
      }
    };

    fetchWeather();
    
  }, []);


  if (!weatherData) {
    return <WeatherCardLoader/>;
  }

  const weatherIcon = weatherData.weather[0].icon;
  const { temp, temp_min, temp_max, humidity } = weatherData.main;
  const { speed: windSpeed } = weatherData.wind;
  const precipitation = weatherData.clouds.all;
  const currentDate = new Date();
  const formattedDate = currentDate.toLocaleDateString('en-US', {
    weekday: 'short', 
    month: 'short',   
    day: '2-digit',  
  });
  const weatherPicture = WeatherCheck(weatherIcon);

  function WeatherCheck(weatherIcon: string | undefined){
    let weather;

    if(weatherIcon === "01d"){
      weather = require('../../assets/images/weather/sun.png');
    } else if(weatherIcon === "01n"){
      weather = require('../../assets/images/weather/night.png');
    } else if(weatherIcon === "02d"){
      weather = require('../../assets/images/weather/fewclouds_day.png');
    } else if(weatherIcon === "02n"){
      weather = require('../../assets/images/weather/fewclouds_night.png');
    } else if(weatherIcon === "03d" || weatherIcon === "03n"){
      weather = require('../../assets/images/weather/cloud_1.png');
    } else if(weatherIcon === "04d" || weatherIcon === "04n"){
      weather = require('../../assets/images/weather/cloud_2.png');
    } else if(weatherIcon === "09d"){
      weather = require('../../assets/images/weather/shower_day.png');
    } else if(weatherIcon === "09n"){
      weather = require('../../assets/images/weather/shower_night.png');
    } else if(weatherIcon === "10d" || weatherIcon === "10n"){
      weather = require('../../assets/images/weather/rain.png');
    } else if(weatherIcon === "11d" || weatherIcon === "11n"){
      weather = require('../../assets/images/weather/thunderstorm.png');
    }

    return weather;
  }

  return (
    <ThemedView style={styles.container}>
      <View style={styles.top}>
        <View style={styles.head}>
          <ThemedText style={styles.date}>{formattedDate.toUpperCase()}</ThemedText>
          <ThemedText style={styles.city}>{city}</ThemedText>
          <ThemedText style={styles.temp}>{Math.round(temp_max)}°C / {Math.round(temp_min)}°C</ThemedText>
        </View>
        <View style={styles.mid}>
          <ThemedText type="title" style={styles.tempMain}>{Math.round(temp)}°C</ThemedText>
          <View>
            <Image style={styles.image} source={weatherPicture} />
          </View>
        </View>
      </View>
      <View style={styles.details}>
        <View style={styles.grp}>
          <ThemedText>
            <Ionicons style={styles.icondetails} name="cloud"/>
          </ThemedText>
          <ThemedText>Precipitation</ThemedText>
          <ThemedText>{precipitation}%</ThemedText>
        </View>
        <View style={styles.grp}>
           <ThemedText>
            <Ionicons style={styles.icondetails} name="water"/>
          </ThemedText>
          <ThemedText>Humidity</ThemedText>
          <ThemedText>{humidity}%</ThemedText>
        </View>
        <View style={styles.grp}>
          <ThemedText>
            <MaterialIcons style={styles.icondetails} name="air"/>
          </ThemedText>
          <ThemedText>Wind Speed</ThemedText>
          <ThemedText>{windSpeed} m/s</ThemedText>
        </View>
      </View>
    </ThemedView>
  );
}

const styles = StyleSheet.create({
  container: {
    padding: 20,
    borderRadius: 10,
    borderWidth: 1,
    borderColor: '#ddd',
  },
  top:{
    flexDirection: "row",
    paddingBottom: 20,
    justifyContent: "space-between",
    alignItems: "center",
  },
  head:{
    flexDirection: "column"
  },
  mid:{
    flexDirection: "row",
    alignItems: "center",  
    justifyContent: "center", 
    height: "auto",
    flexWrap: "nowrap",
    marginTop: 10,  
    
  },
  image:{
    marginLeft: 5, 
    height: 70,
    width: 70, 
  },
  tempMain:{  
    fontFamily: "CeraPro_Medium",
    marginTop: 0, 
    textAlign: "center",
    marginRight: 20,
  },
  date: {
    fontSize: 18,
    fontWeight: 'bold',
  },
  city: {
    fontSize: 16,
  },
  temp:{
    fontFamily: "CeraPro_Light"
  },
  details: {
    flexDirection: 'row',
    justifyContent: 'space-between',
  },
  grp: {
    flexDirection: "column",
    justifyContent: "center",
    alignContent: "center",
    alignItems: "center"
  },
  icondetails:{
    fontSize: 20,
    marginBottom: 5,
  }
});
