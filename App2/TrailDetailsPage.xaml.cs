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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace App2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TrailDetailsPage : Page
    {
        private List<Activity> activitiesList = new List<Activity>();
        public TrailDetailsPage()
        {
            this.InitializeComponent();
            
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Place clickedplace = (Place)e.Parameter;
            foreach (var  item in clickedplace.activities)
            {

                activitiesList.Add(item);
                Button activity = new Button();
                activity.Content = item.activity_type_name;
                activityList.Children.Add(activity);
               
            }
           placeDescription.Text =  activitiesList[0].description;
            
        }
    }
}
