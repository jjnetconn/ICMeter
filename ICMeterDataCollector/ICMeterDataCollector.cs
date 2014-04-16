using System;

namespace ICMeterDataCollector
{
    class ICMeterDataCollector
    {
        public static WeatherData owData;
        public static ICMeterTokenHandler icToken;
        public static ICMeterDataHandler icData;

        static void Main(string[] args)
        {

            owData = new WeatherData();
            icToken = new ICMeterTokenHandler();
            icData = new ICMeterDataHandler();
        }   
    }
}
