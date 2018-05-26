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
        public static void GetPage(string ouputFileName, string page)
        {
            WebClient client = new WebClient();
            var fullPath = FileManager.CreateFile(ouputFileName);
            client.DownloadFileAsync(new Uri(page), fullPath);
        }

        public static int CountEqualLinks(WebPage currentPage, WebPage endPage)
        {
            return currentPage.Links.Intersect(endPage.Links).Count();
        }

        public static GetPriorityQueue()
        {
            var endPage = "";
            var priorityQueue = new PriorityQueue<WebPage, WebPage>(QueueComparer<WebPage>.Create((WebPage firstPage, WebPage secondPage) =>
            {
                if(firstPage.Content == endPage)
                {

                }
                return 1;
            }));
        }
    }
}
