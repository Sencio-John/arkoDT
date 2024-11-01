const apiKey = "dXelGAau-a3oj0R1KfC5FJXLYvRwh719FdifdEzPwVE"

export const mapHTML = `
    <!DOCTYPE html>
    <html>
    <head>
      <meta name="viewport" content="initial-scale=1.0, user-scalable=no" />
      <style>html, body { margin: 0; padding: 0; height: 100%; }</style>
      <script src="https://js.api.here.com/v3/3.1/mapsjs-core.js"></script>
      <script src="https://js.api.here.com/v3/3.1/mapsjs-service.js"></script>
      <script src="https://js.api.here.com/v3/3.1/mapsjs-ui.js"></script>
      <script src="https://js.api.here.com/v3/3.1/mapsjs-mapevents.js"></script>
    </head>
    <body>
      <div id="mapContainer" style="width: 100%; height: 100%;"></div>
      <script>
        var platform = new H.service.Platform({
          apikey: '${apiKey}'
        });

        var defaultLayers = platform.createDefaultLayers();
        var map = new H.Map(
          document.getElementById('mapContainer'),
          defaultLayers.vector.normal.map,
          { zoom: 10, center: { lat: 37.7749, lng: -122.4194 } } // Default to San Francisco
        );

        var behavior = new H.mapevents.Behavior(new H.mapevents.MapEvents(map));
        var ui = H.ui.UI.createDefault(map, defaultLayers);

        // Function to add a marker
        function addMarker(lat, lng, title) {
          var marker = new H.map.Marker({ lat: lat, lng: lng });
          marker.setData(title); // Store title or other info
          map.addObject(marker);

          // Add click event to the marker
          marker.addEventListener('tap', function(evt) {
            var data = evt.target.getData();
            window.ReactNativeWebView.postMessage(JSON.stringify({ lat, lng, title: data }));
          });
        }

        // Example marker
        addMarker(37.7749, -122.4194, "San Francisco");

        // Track user's location
        function trackLocation() {
          navigator.geolocation.watchPosition(
            function(position) {
              var lat = position.coords.latitude;
              var lng = position.coords.longitude;
              map.setCenter({ lat, lng });
              map.setZoom(14); // Zoom in to current location
            },
            function(error) {
              console.error(error);
            },
            { enableHighAccuracy: true, distanceFilter: 10 }
          );
        }

        // Start tracking
        trackLocation();
      </script>
    </body>
    </html>
  `;