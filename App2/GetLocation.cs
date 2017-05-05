using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Controls;

namespace App2
{
    public class GetLocation
    {
        TrailApi Api = new TrailApi();
        public List<Button> placeButtons = new List<Button>();
        public double lat { get; set; }
        public double lon { get; set; }
        
        public async void findLocation()
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

                    Api.GetApis(pos.Coordinate.Point.Position.Latitude, pos.Coordinate.Point.Position.Longitude, 25);

                    foreach (var place in Api.placeList)
                    {

                        Button mybutton = new Button();
                        mybutton.Content = place.name;
                        placeButtons.Add(mybutton);
                        

                    }
                    break;
                case GeolocationAccessStatus.Denied:
                    
                    break;
                default:
                    break;
            }
        }

        
    }
}
