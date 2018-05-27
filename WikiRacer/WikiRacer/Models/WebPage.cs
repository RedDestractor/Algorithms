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
        public string Content { get; private set; }
        public List<string> Links { get; private set; }
        public string Name { get; private set; }

        public WebPage(string content, string Url)
        {
            Content = content;

            var parser = new HtmlParser();
            var document = parser.Parse(content);

            Links = document.Links
                .Where(l => l as IHtmlAnchorElement != null)
                .Select(l => (IHtmlAnchorElement)l)
                .Where(l => l.PathName.StartsWith("/wiki/"))
                .Select(x => "https://en.wikipedia.org" + x.PathName)
                .ToList();

            Name = Url;
        }
    }
}
