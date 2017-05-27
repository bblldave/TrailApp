using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//This page is used to display a list of places. I still need to make it look more pleasing.



// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace App2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TrailList : Page
    {
        public TrailList()
        {
            this.InitializeComponent();
            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            //This is used to set the ListView's items to the placelist.
            DataTemplate myTemplate = new DataTemplate();
            placeListView.ItemsSource = App.Api.placeList;
            
            


            BasicGeoposition mapCenter = new BasicGeoposition() { Latitude = App.Api.placeList[0].lat, Longitude = App.Api.placeList[0].lon };
            Geopoint trailCenter = new Geopoint(mapCenter);
            pointMap.Center = trailCenter;
            pointMap.Style = MapStyle.Road;
            pointMap.ZoomLevel = 10;
            pointMap.LandmarksVisible = true;


            foreach (var item in App.Api.placeList)
            {
                BasicGeoposition trailPosition = new BasicGeoposition() { Latitude = item.lat, Longitude = item.lon };
                Geopoint trailPoint = new Geopoint(trailPosition);
                MapIcon trailPOI = new MapIcon { Location = trailPoint, NormalizedAnchorPoint = new Point(0.5, 1.0), Title = item.name, ZIndex = 0 };
                //trailPOI.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/pushpin.png"));
                pointMap.MapElements.Add(trailPOI);
            }
        }

        //This is used to navigate the frame to the TrailDetailPage and take the selected place with it.
        private void placeListView_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Place myPlace = placeListView.SelectedItem as Place;
            Frame.Navigate(typeof(TrailDetailsPage),myPlace);
            
        }
    }
}
