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
using System.Net.Http;

using Newtonsoft.Json;
using System.Threading.Tasks;
using Windows.UI.Core;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace App2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewsPage : Page
    {
        public NewsPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            newsGrid.ItemsSource = await GetHikingNews();
        }

        public async Task<List<NewsResult>> GetHikingNews()
        {
            var results = new List<NewsResult>();
            var webClient = new HttpClient();
            webClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "f503d1c5394c48dda397a1da5f6b9642");
            byte[] searchResults = await webClient.GetByteArrayAsync("https://api.cognitive.microsoft.com/bing/v5.0/news/search?q=hiking&mkt=en-us");
            var serializer = new JsonSerializer();
            using (var stream = new MemoryStream(searchResults))
            using (var reader = new StreamReader(stream))
            using (var jsonReader = new JsonTextReader(reader))
            {
                results = serializer.Deserialize<NewsSearch>(jsonReader).NewsResult;
            }
            return results;
        }

        
    }
}
