using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using unirest_net.http;

//This class is used to get data from Trailapi.com
//It uses a nuget package called unirest to handle HTTP requests and responses
//The data is returned as Json and is then stored in a list of Place objects

namespace App2
{
    public class TrailApi
    {
        //this is the base URL for the api end point
        private const string ApiUri = "https://trailapi-trailapi.p.mashape.com/";
        //This is the header that is applied to the request. It is the access key
        private const string ApiKeyHeader = "X-Mashape-Key";
        private const string ApiKey = "w1arm0PQpcmsh1xRV8fMsuIrqjVhp1Hwa0ejsnaR0t2N6kpHHI";
        //This is the list of places that the Json data is converted to. This allows for access of all of the Place's attributes
        public ObservableCollection<Place> placeList = new ObservableCollection<Place>();

        //This is used to get a placelist based on the Latitude and Longitude of the users Location. A radius must also be supplied and will have effects on the results.
        public async Task GetApis(double lat, double lon, int radius)
        {
            var requestUrl = string.Format(
                "{0}?lat={1}&lon={2}&radius={3}",
                ApiUri, lat, lon, radius);

            var placeData =  Unirest.get(requestUrl)
                                      .header(ApiKeyHeader, ApiKey)
                                      .header("Accept", "application/json")
                                      .asJson<string>();
            var results = JsonConvert.DeserializeObject<RootObject>(placeData.Body);

            foreach (var place in results.places)
            {
                placeList.Add(place);
                //Console.WriteLine("name:" + place.name);
            }
        }

        //This is used to get a placelist based on the city and state search box. A radius must also be supplied but will have no effect on results.
        public async Task GetApisCityState(string city, string state, int radius)
        {


            var requestUrl = string.Format(
                "{0}?q[city_cont]={1}&q[state_cont]={2}&radius={3}",
                ApiUri, city, state, radius);

            var placeData = Unirest.get(requestUrl)
                                      .header(ApiKeyHeader, ApiKey)
                                      .header("Accept", "application/json")
                                      .asJson<string>();
            var results = JsonConvert.DeserializeObject<RootObject>(placeData.Body);

            foreach (var place in results.places)
            {
                placeList.Add(place);
                //Console.WriteLine("name:" + place.name);
            }


        }


    }

    //This is what the Json data converts into
    public class RootObject
    {
        public List<Place> places { get; set; }
    }


    //This is an Entity Data Model for a Place. Each place contains a list of activities
    public class Place
    {
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string name { get; set; }
        public object parent_id { get; set; }
        public int unique_id { get; set; }
        public string directions { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public object description { get; set; }
        public object date_created { get; set; }
        public List<object> children { get; set; }
        public List<Activity> activities { get; set; }
    }

  

    public class Attribs
    {
        public string length { get; set; }
        public string nightride { get; set; }
    }

    public class ActivityType
    {
        public string created_at { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string updated_at { get; set; }
    }

    //This is an Entity Data Model for a place's activities
    public class Activity
    {
        public string name { get; set; }
        public string unique_id { get; set; }
        public int place_id { get; set; }
        public int activity_type_id { get; set; }
        public string activity_type_name { get; set; }
        public string url { get; set; }
        public Attribs attribs { get; set; }
        public string description { get; set; }
        public double length { get; set; }
        public ActivityType activity_type { get; set; }
        public string thumbnail { get; set; }
        public object rank { get; set; }
        public double rating { get; set; }
    }
}
