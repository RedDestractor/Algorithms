using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiRacer
{
    public class WebPage
    {
        public string Content { get; set; }
        public List<string> Links { get; set; }
        public string Name { get; set; }

        public WebPage(string content, string Url)
        {
            Content = content;

            var parser = new HtmlParser();
            var document = parser.Parse(content);

            Links = document.Links
                .Select(l => (IHtmlAnchorElement)l)
                .Where(l => l.PathName.StartsWith("/wiki/"))
                .Select(x => x.ToString())
                .ToList();

            Name = Url;
        }
    }
}
