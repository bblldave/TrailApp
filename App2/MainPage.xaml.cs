using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        TrailApi Api = new TrailApi();
        GetLocation myLocation = new GetLocation();
        public bool locationstatus;
        

        public MainPage()
        {
            this.InitializeComponent();

        }

       

        private void findTrailsBtn_Click(object sender, RoutedEventArgs e)
        {
            
            //App.Api.GetApis(App.myLocation.lat, App.myLocation.lon, 25);

            //placeListView.ItemsSource = Api.placeList;
            
        }

        private async void findBtn_Click(object sender, RoutedEventArgs e)
        {
            loadingRing.IsActive = true;
            await App.Api.GetApisCityState(cityTxtBox.Text, stateTxtBox.Text, 25);
            myFrame.Navigate(typeof(TrailList));
            loadingRing.IsActive = false;
            //Api.GetApisCityState(City.Text, State.Text, 25);
            //placeListView.ItemsSource = Api.placeList;

        }

        private void hamburgerBtn_Click(object sender, RoutedEventArgs e)
        {
            mySplitPanel.IsPaneOpen = !mySplitPanel.IsPaneOpen;
        }

        private async void GetLocation_TappedAsync(object sender, TappedRoutedEventArgs e)
        {
            
            loadingRing.IsActive = true;
            await App.myLocation.findLocation();
            await App.Api.GetApis(App.myLocation.lat, App.myLocation.lon, 25);
            myFrame.Navigate(typeof(TrailList));
            loadingRing.IsActive = false;
        }

        
    }
}
