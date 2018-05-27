using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using PriorityQueue;
using WikiRacer.Helpers;

namespace WikiRacer
{
    public static class WebPageManager
    {
        public static string GetPageToString(string Uri)
        {
            WebClient client = new WebClient();
            return client.DownloadString(new Uri(Uri));
        }
    }
}
