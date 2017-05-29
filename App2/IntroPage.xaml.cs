using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.System.UserProfile;
using System.Diagnostics;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace App2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class IntroPage : Page
    {
        // Declare class functions.
        private int GetRadius()
        {
            int radius = 0;
            
            switch (cboRadius.SelectedIndex)
            {
                case 0:
                    radius = 25;
                    break;
                case 1:
                    radius = 50;
                    break;
                case 2:
                    radius = 100;
                    break;
            }

            return radius;
        }         // Checks which radius the user selected, converts it, and returns it as an integer.

        public IntroPage()
        {
            this.InitializeComponent();
        }

        // On page load, disable the controls until user makes the appropriate selections.
        private void pgeIntro_Loaded(object sender, RoutedEventArgs e)
        {
            // Use this function to write to the output console for debugging.
            // Debug.WriteLine(cboRadius.SelectedIndex);

            // Disable the controls.
            rdoMyLocation.IsEnabled = false;
            rdoOtherLocation.IsEnabled = false;
            txtCity.IsEnabled = false;
            txtState.IsEnabled = false;
            btnStart.IsEnabled = false;
        }

        // When user selects a radius, the radio buttons are enabled.
        private void cboRadius_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            rdoMyLocation.IsEnabled = true;
            rdoOtherLocation.IsEnabled = true;
        }

        // When user clicks the start button, check which radio button is selected, and find the location.
        // Navigates to the MainPage and displays the TrailListPage. 
        private async void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (rdoMyLocation.IsChecked == true)
            {
                await App.myLocation.findLocation();
                await App.Api.GetApis(App.myLocation.lat, App.myLocation.lon, GetRadius());
                this.Frame.Navigate(typeof(MainPage));
            }
            else if (rdoOtherLocation.IsChecked == true)
            {
                await App.myLocation.findLocation();
                await App.Api.GetApisCityState(txtCity.Text, txtState.Text, GetRadius());
                this.Frame.Navigate(typeof(MainPage));
            }
        }

        private void rdoMyLocation_Checked(object sender, RoutedEventArgs e)
        {
            txtCity.IsEnabled = false;
            txtState.IsEnabled = false;
            btnStart.IsEnabled = true;
        }

        private void rdoOtherLocation_Checked(object sender, RoutedEventArgs e)
        {
            txtCity.IsEnabled = true;
            txtState.IsEnabled = true;

            if (txtCity.Text == "" || txtState.Text == "")
            {
                btnStart.IsEnabled = false;
            }
            else
                btnStart.IsEnabled = true;

        }

        private void txtCity_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtCity.Text == "" || txtState.Text == "")
                btnStart.IsEnabled = false;
            else
                btnStart.IsEnabled = true;
        }

        private void txtState_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtCity.Text == "" || txtState.Text == "")
                btnStart.IsEnabled = false;
            else
                btnStart.IsEnabled = true;
        }
    }
}
