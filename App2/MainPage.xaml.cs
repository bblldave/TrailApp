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

//This page is used to display the main frame. This includes the navigation menu and the search bar. The frame is navigated to display other pages while
//keeping the navigation bars active.

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

       

    
        //This occurs when the search button is pressed. It gets the placelist and navigates to traillist.
        private async void findBtn_Click(object sender, RoutedEventArgs e)
        {
            loadingRing.IsActive = true;
            await App.Api.GetApisCityState(cityTxtBox.Text, stateTxtBox.Text, 25);
            myFrame.Navigate(typeof(TrailList));
            loadingRing.IsActive = false;
            

        }

        //This occurs when the hamburger button is pressed and opens the navigation panel
        private void hamburgerBtn_Click(object sender, RoutedEventArgs e)
        {
            mySplitPanel.IsPaneOpen = !mySplitPanel.IsPaneOpen;
        }


        //This occurs when the get location button is pressed and gets the placelist then navigates to traillist page.
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
