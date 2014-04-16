using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;

namespace ICMeterDataCollector
{
    class WeatherData
    {
        public WeatherData()
        {
            GetWeather();
        }

        private void GetWeather()
        {
            var syncClient = new WebClient();
            var content = syncClient.DownloadString(Properties.Settings.Default.OpenWeatherURI);

            var v = JsonConvert.DeserializeObject<ICMeterAccessTokenTemplate.RootObject>(content);

            //Debuging and DEV
            if (Properties.Settings.Default.Debug)
            {
                Console.WriteLine("Token Claimed!");
                Console.ReadKey();
            }
        }

    }
}
