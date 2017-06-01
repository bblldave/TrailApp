using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Services.Maps;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;

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

        //Used to geocode users city and state to allow for the radius to work when searched
        public async Task<Geopoint> Geocode(string valueEntered)
        {
            MapService.ServiceToken = "IE6CWqw8LSOJGl3FtWcG~WkKUYZtgj10OaIQPrdi0sA~AllFo50ybXUhWttLxOaFCdhjyPr1MaV2 - qn4 - v9ZusCJYpqNPEO6slQCIAW4C1NH";
            BasicGeoposition queryHint = new BasicGeoposition();
            queryHint.Latitude = 47.643;
            queryHint.Longitude = -122.131;
            Geopoint hintPoint = new Geopoint(queryHint);
            MapLocationFinderResult result =
         await MapLocationFinder.FindLocationsAsync(
                           valueEntered,
                           hintPoint,
                           3);
            var resultCount = result.Locations.Count;


            if (result.Status == MapLocationFinderStatus.Success)
            {
                BasicGeoposition newLocation = new BasicGeoposition();
                newLocation.Latitude = result.Locations[0].Point.Position.Latitude;
                newLocation.Longitude = result.Locations[0].Point.Position.Longitude;
                Geopoint geoPoint = new Geopoint(newLocation);
                MapAddress mapAddress = result.Locations[0].Address;
                return geoPoint;
                
            }
            return hintPoint;
        }

       

        
    }
}
