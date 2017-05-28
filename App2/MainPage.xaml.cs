using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += App_BackRequested;

            //PC customization
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.ApplicationView"))
            {

                var titleBar = ApplicationView.GetForCurrentView().TitleBar;
                var accentColor = new UISettings().GetColorValue(UIColorType.Accent);
                if (titleBar != null)
                {
                    titleBar.ButtonBackgroundColor = accentColor;
                    titleBar.ButtonForegroundColor = Colors.White;
                    titleBar.BackgroundColor = accentColor;
                    titleBar.ForegroundColor = Colors.White;
                }
            }

            //Mobile customization
            //if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            //{

            //    var statusBar = StatusBar.GetForCurrentView();
            //    if (statusBar != null)
            //    {
            //        statusBar.BackgroundOpacity = 1;
            //        statusBar.BackgroundColor = Colors.DarkBlue;
            //        statusBar.ForegroundColor = Colors.White;
            //    }
            //}
        }


        //This occurs when the search button is pressed. It gets the placelist and navigates to traillist.
        private async void findBtn_Click(object sender, RoutedEventArgs e)
        {
            loadingRing.IsActive = true;
            App.Api.placeList.Clear();
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
            App.Api.placeList.Clear();
            loadingRing.IsActive = true;
            await App.myLocation.findLocation();
            await App.Api.GetApis(App.myLocation.lat, App.myLocation.lon, 25);
            myFrame.Navigate(typeof(TrailList));
            loadingRing.IsActive = false;
        }

         private void App_BackRequested(object sender, Windows.UI.Core.BackRequestedEventArgs e)
        {
            Frame rootFrame = myFrame;
            if (rootFrame == null)
                return;

            // Navigate back if possible, and if the event has not 
            // already been handled .
            if (rootFrame.CanGoBack && e.Handled == false)
            {
                e.Handled = true;
                rootFrame.GoBack();
            }
        }

        
    }
}
