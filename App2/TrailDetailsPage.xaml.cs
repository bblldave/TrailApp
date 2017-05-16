using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace App2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TrailDetailsPage : Page
    {
        
        
        public TrailDetailsPage()
        {
            this.InitializeComponent();
            
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var clickedplace = (Place)e.Parameter;
            activities.ItemsSource = clickedplace.activities;
            

            

            try
            {
                placeDescription.Text = clickedplace.activities[0].description;
            }
            catch (Exception)
            {

                placeDescription.Text = "No description available.";
            }



            try
            {
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.UriSource = new Uri(clickedplace.activities[0].thumbnail);
                placePic.Source = bitmapimage;

            }
            catch (Exception)
            {


            }

            BasicGeoposition trailPosition = new BasicGeoposition() { Latitude = clickedplace.lat, Longitude = clickedplace.lon };
            Geopoint trailCenter = new Geopoint(trailPosition);
            MapIcon trailPOI = new MapIcon { Location = trailCenter, NormalizedAnchorPoint = new Point(0.5, 1.0), Title = "Trail Location", ZIndex = 0 };
            trailMap.MapElements.Add(trailPOI);
            trailMap.Center = trailCenter;
            trailMap.Style = MapStyle.Road;
            trailMap.ZoomLevel = 15;
            trailMap.LandmarksVisible = true;





        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void activities_ItemClick(object sender, ItemClickEventArgs e)
        {
            Activity myActivity = e.ClickedItem as Activity;
            placeDescription.Text = myActivity.description;

            try
            {
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.UriSource = new Uri(myActivity.thumbnail);
                placePic.Source = bitmapimage;

            }
            catch (Exception)
            {


            }
        }
    }
}
