namespace Coco.Core.Web.Resolvers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Fizzler.Systems.HtmlAgilityPack;

    using HtmlAgilityPack;

    public class CssInnerHtmlResolver<TData> : IWebValueResolver<TData>
    {
        private readonly string cssSelector;

        public CssInnerHtmlResolver(string cssSelector)
        {
            this.cssSelector = cssSelector;
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

                return (TData)Convert.ChangeType(htmlNode.InnerHtml, typeof(TData));
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