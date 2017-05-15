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
        
        
        public async Task findLocation()
        {
            var accessStatus = await Geolocator.RequestAccessAsync();

            switch (accessStatus)
            {
                case GeolocationAccessStatus.Unspecified:
                    
                    break;
                case GeolocationAccessStatus.Allowed:
                    
                    Geolocator geolocator = new Geolocator { DesiredAccuracyInMeters = 10 };
                    Geoposition pos = await geolocator.GetGeopositionAsync();
                    lat = pos.Coordinate.Point.Position.Latitude;
                    lon = pos.Coordinate.Point.Position.Longitude;
                    

                    
                    

                    //foreach (var place in Api.placeList)
                    //{
                    //    placeList.Add(place);
                    //}

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
