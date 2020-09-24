using System.Net;
using Newtonsoft.Json; 
namespace Dark_Sky_Comparisons
{
    public class DarkSkyData     {         //JSON Properties         public string latitude { get; set; }         public string longitude { get; set; }         public string timezone { get; set; }         public Currently currently { get; set; }         public int offset { get; set; }

        //Custom Properties
        public string cityName;          //Custom Function - Pull Forecast Data         public DarkSkyData pullForecast(string url)         {             using (WebClient wc = new WebClient())             {                 var json = wc.DownloadString(url);                 DarkSkyData forecastData = JsonConvert.DeserializeObject<DarkSkyData>(json);                 return forecastData;             }         }     } 
	//Class for Data     public class Currently     {         public int time { get; set; }         public string summary { get; set; }         public double temperature { get; set; }         public double apparentTemperature { get; set; }         public double humidity { get; set; }         public double windSpeed { get; set; }         public int uvIndex { get; set; }     } }