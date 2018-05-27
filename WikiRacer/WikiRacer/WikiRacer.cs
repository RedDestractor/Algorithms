using PriorityQueue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikiRacer.Helpers;

namespace WikiRacer
{
    public class WikiRacer
    {
        private PriorityQueue<int, WebPage> priorityQueue;
        private WebPage endPage;

        public WikiRacer(WebPage endPage)
        {
            this.endPage = endPage;
            var priorityQueue = new PriorityQueue<int, WebPage>(new DescendingComparer<int>());
        }

        public List<WebPage> GetLadder(WebPage startPage, List<WebPage> ladder)
        {
            var result = new List<WebPage>();
            priorityQueue.Enqueue(CountEqualLinks(startPage, endPage), startPage);

            while(!priorityQueue.IsEmpty)
            {
                var currentPage = priorityQueue.Dequeue().Value;

                //var links = currentPage.Value.Links;

                if(IsEndPageInCurrentLinks(currentPage))
                {
                    result.Add(endPage);
                    return result;
                }

                foreach(var link in currentPage.Links)
                {
                    var neighbourPage = new WebPage(WebPageManager.GetPageToString(link));
                    //result.Add(new WebPage {   }
                }

                return new List<WebPage>();
            }

        }

        private int CountEqualLinks(WebPage currentPage, WebPage endPage)
        {
            return currentPage.Links.Intersect(endPage.Links).Count();
        }

        private bool IsEndPageInCurrentLinks(WebPage currentPage)
        {
            return currentPage.Links.Exists(x => x == endPage.Name);
        }
    }
}
