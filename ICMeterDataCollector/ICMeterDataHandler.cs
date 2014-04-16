using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;

namespace ICMeterDataCollector
{
    class ICMeterDataHandler
    {
        public ICMeterDataHandler()
        {
            this.BoxIDs = StageBoxIDs();
            GetData();
        }

        private int[] BoxIDs;
        private string FromDate;
        private string ToDate;
        private ArrayList ResultData = new ArrayList();

        private void GetData()
        {
            DateTime fromDate = DateTime.Today;
            DateTime toDate = fromDate.AddDays(1);
            this.FromDate = fromDate.ToString(Properties.Settings.Default.DateTimeFormat);
            this.ToDate = toDate.ToString(Properties.Settings.Default.DateTimeFormat);

            if (Properties.Settings.Default.Debug) { 
                Console.WriteLine("FromDate: " + this.FromDate);
                Console.WriteLine("ToDate: " + this.ToDate);
            }

            for (int i = 0; i < this.BoxIDs.Length; i++)
            {
                string tmpURI = "http://app.ic-meter.com/icm/api/measurements/1.0/days/range/"+this.BoxIDs[i]+"?fromDate="+this.FromDate+"&toDate="+this.ToDate+"&access_token="+ICMeterDataCollector.icToken.Token.access_token;

                if(Properties.Settings.Default.Debug){
                    Console.WriteLine(tmpURI);
                }

                var syncClient = new WebClient();
                var content = syncClient.DownloadString(tmpURI);
                ResultData.Add(JsonConvert.DeserializeObject<ICMeterIndoorTemplate.RootObject>(content));
            }
            
            if (Properties.Settings.Default.Debug)
            {
                Console.ReadKey();
            }
 
        }

        private int[] StageBoxIDs()
        {
            string[] BoxIDs = Properties.Settings.Default.BoxIDs.Split(';');
            int[] BoxIdInts =  new int[BoxIDs.Length];

            for (int i = 0; i < BoxIDs.Length; i++)
            {
                try { 
                    BoxIdInts[i] = Convert.ToInt32(BoxIDs[i]);
                    }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return BoxIdInts;
        }

    }
}
