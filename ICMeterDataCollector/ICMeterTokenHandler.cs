using System;
using System.Net;
using Newtonsoft.Json;

namespace ICMeterDataCollector
{
    class ICMeterTokenHandler
    {
        public bool IsTokenExpired { get { return isTokenExpired; } }
        public ICMeterAccessTokenTemplate.RootObject Token { get { return token; } }

        private bool isTokenExpired;
        private ICMeterAccessTokenTemplate.RootObject token;

        public ICMeterTokenHandler()
        {
            this.isTokenExpired = true;
            ClaimToken();
            CheckToken();
        }

        public void ClaimToken()
        {
            var syncClient = new WebClient();
            var content = syncClient.DownloadString(Properties.Settings.Default.ICMenterTokenURI);

            this.token = JsonConvert.DeserializeObject<ICMeterAccessTokenTemplate.RootObject>(content);

            //Debuging and DEV
            if (Properties.Settings.Default.Debug)
            {
                Console.WriteLine("Token Claimed!");
                Console.ReadKey();
            }   
        }

        public void CheckToken()
        {
            if (this.token.expires_in == 0)
            {
                isTokenExpired = true;
            }
            else
            {
                if (Properties.Settings.Default.Debug) { 
                    Console.WriteLine("Token Expires in: " + this.token.expires_in + " Sec.");
                    Console.ReadKey();
                }
            }
        }
    }
}
