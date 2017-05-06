using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
        

        public MainPage()
        {
            this.InitializeComponent();
           
           
        }

       

        private  void  getLocationBtn_Click(object sender, RoutedEventArgs e)
        {
            myLocation.findLocation();
        }

        private void findTrailsBtn_Click(object sender, RoutedEventArgs e)
        {
            placeListView.ItemsSource = myLocation.placeList;
        }

        private void searchBtn_Click(object sender, RoutedEventArgs e)
        {
            
        }

      
      

        private void placeListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            
            this.Frame.Navigate(typeof(TrailDetailsPage), e.ClickedItem);
        }

        
    }
}
