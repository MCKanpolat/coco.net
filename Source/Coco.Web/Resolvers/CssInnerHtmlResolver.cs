namespace Coco.Core.Web.Resolvers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Coco.Web;
    using Coco.Web.Parsers;

    using Fizzler.Systems.HtmlAgilityPack;

    using HtmlAgilityPack;

    public class CssInnerHtmlResolver<TData> : IWebValueResolver<TData>
    {
        private readonly string cssSelector;

        private readonly IHtmlParser htmlParser;

        public CssInnerHtmlResolver(string cssSelector, IHtmlParser htmlParser)
        {
            this.cssSelector = cssSelector;
            this.htmlParser = htmlParser;
        }

        public TData GetValue(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return default(TData);
            }

            try
            {
                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(content);

                var htmlNode = htmlDocument.DocumentNode.QuerySelector(this.cssSelector);
                if (htmlNode == null)
                {
                    return default(TData);
                }

                var value = this.htmlParser.Parse(htmlNode.InnerHtml);
                return (TData)Convert.ChangeType(value, typeof(TData));
            }
            catch (Exception exception)
            {
                return default(TData);
            }            
        }

        public IEnumerable<TData> GetValues(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return Enumerable.Empty<TData>();
            }

            try
            {
                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(content);

                var htmlNodes = htmlDocument.DocumentNode.QuerySelectorAll(this.cssSelector);
                if (htmlNodes == null)
                {
                    return Enumerable.Empty<TData>();
                }

                return htmlNodes.Select(n => (TData)Convert.ChangeType(n.InnerHtml, typeof(TData)));
            }
            catch (Exception exception)
            {
                return Enumerable.Empty<TData>();
            }
        }
    }
}