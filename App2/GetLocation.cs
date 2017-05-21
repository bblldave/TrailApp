using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace App2
{
    public class GetLocation
    {
        TrailApi Api = new TrailApi();
        public List<Place> placeList = new List<Place>();
        public bool locationFound;
        public double lat { get; set; }
        public double lon { get; set; }

        //This class is used to get the users location. It uses the devices GPS if available.
        
        
        public async Task findLocation()
        {
            //This is used to request access to the Devices GPS
            var accessStatus = await Geolocator.RequestAccessAsync();

            //This switch statement is used to perform actions based on what the GPS Request status is. Still need to develop exceptions for Unspecified and 
            // Denied status
            switch (accessStatus)
            {
                case GeolocationAccessStatus.Unspecified:
                    
                    break;
                case GeolocationAccessStatus.Allowed:
                    
                    Geolocator geolocator = new Geolocator { DesiredAccuracyInMeters = 10 };
                    Geoposition pos = await geolocator.GetGeopositionAsync();
                    lat = pos.Coordinate.Point.Position.Latitude;
                    lon = pos.Coordinate.Point.Position.Longitude;
                    locationFound = true;
                    break;
                case GeolocationAccessStatus.Denied:
                    
                    break;
                default:
                    break;
            }
        }

       

        
    }
}
