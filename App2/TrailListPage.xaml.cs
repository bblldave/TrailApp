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
            //This is used to set the ListView's items to the placelist.
            placeListView.ItemsSource = App.Api.placeList;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
        }

        //This is used to navigate the frame to the TrailDetailPage and take the selected place with it.
        private void placeListView_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Place myPlace = placeListView.SelectedItem as Place;
            Frame.Navigate(typeof(TrailDetailsPage),myPlace);
            
        }
    }
}
