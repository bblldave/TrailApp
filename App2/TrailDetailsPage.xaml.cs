using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

//This page is used to show the details for the selected place.

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
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            var clickedplace = (Place)e.Parameter;
            activities.ItemsSource = clickedplace.activities;

            if (activities.Items[0] != null)
            {
                activities.SelectedItem = activities.Items[0];
            }
            else
            {
                activities.SelectedItem = activities.Items[0];
            }
            
            trailName.Text = clickedplace.name;

            //This section is used to display the trails description. It is set to the first indexed activity.
            try
            {
                placeDescription.Text = clickedplace.activities[0].description;
            }
            catch (Exception)
            {

                placeDescription.Text = "No description available.";
            }


            //This section is used to take the trails thumbnail anc display it.
            try
            {
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.UriSource = new Uri(clickedplace.activities[0].thumbnail);
                placePic.Source = bitmapimage;

            }
            catch (Exception)
            {
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.UriSource = new Uri("ms-appx:///Assets/NoImage.jpg");
                placePic.Source = bitmapimage;

            }

            //This section is center the map on the trails location.
            BasicGeoposition trailPosition = new BasicGeoposition() { Latitude = clickedplace.lat, Longitude = clickedplace.lon };
            Geopoint trailCenter = new Geopoint(trailPosition);
            MapIcon trailPOI = new MapIcon { Location = trailCenter, NormalizedAnchorPoint = new Point(0.5, 1.0), Title = "Trail Location", ZIndex = 0 };
            trailMap.MapElements.Add(trailPOI);
            trailMap.Center = trailCenter;
            trailMap.Style = MapStyle.Road;
            trailMap.ZoomLevel = 15;
            trailMap.LandmarksVisible = true;
        }

        

        //This is used to allow the user to select different activities. It will display info about the selected activity.
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
