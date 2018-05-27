using PriorityQueue;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikiRacer.Helpers;

namespace WikiRacer
{
    public class WikiRacer
    {
        private PriorityQueue<int, List<WebPage>> priorityQueue;
        private WebPage endPage;

        public WikiRacer(WebPage endPage)
        {
            this.endPage = endPage;
            this.priorityQueue = new PriorityQueue<int, List<WebPage>>(new DescendingComparer<int>());
        }

        public List<WebPage> GetLadder(WebPage startPage)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var result = new List<List<WebPage>>();
            var ladder = new List<WebPage>() { startPage };

            priorityQueue.Enqueue(CountEqualLinks(ladder.Last(), endPage), ladder);

            while(!priorityQueue.IsEmpty)
            {
                var currentLadder = priorityQueue.Dequeue().Value;
                var currentPage = currentLadder.Last();

                if (IsEndPageInCurrentLinks(currentLadder))
                {
                    currentLadder.Add(endPage);
                    return currentLadder;
                }

                foreach(var link in currentPage.Links)
                {
                    var viewedLinks = new HashSet<string>();

                    if(!priorityQueue.IsEmpty && IsEndPageInCurrentLinks(priorityQueue.Peek()))
                        return priorityQueue.Peek();

                    if (!viewedLinks.Contains(link))
                    {
                        viewedLinks.Add(link);
                        var currentLadderCopy = new List<WebPage>(currentLadder);
                        var neighbourPage = new WebPage(WebPageManager.GetPageToString(link), link);
                        currentLadderCopy.Add(neighbourPage);
                        priorityQueue.Enqueue(CountEqualLinks(currentLadderCopy.Last(), endPage), currentLadderCopy);
                    }                    
                }

                if (stopWatch.Elapsed.Seconds > 100)
                    return null;
            }
            return null;
        }

        private int CountEqualLinks(WebPage currentPage, WebPage endPage)
        {
            return currentPage.Links.Intersect(endPage.Links).Count();
        }

        private bool IsEndPageInCurrentLinks(List<WebPage> currentPageLadder)
        {
            return currentPageLadder.Exists(x => x.Name == endPage.Name);
        }
    }
}
