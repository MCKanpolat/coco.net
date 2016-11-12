namespace Coco.Core.Web.Resolvers
{
    using System;

    using Fizzler.Systems.HtmlAgilityPack;

    using HtmlAgilityPack;

    public class CssInnerHtmlResolver<TData> : IPropertyValueResolver<TData>
    {
        private readonly string cssSelector;

        public CssInnerHtmlResolver(string cssSelector)
        {
            this.cssSelector = cssSelector;
        }

        public TData GetMe(string content)
        {
            try
            {
                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(content);
                var htmlNode = htmlDocument.DocumentNode.QuerySelector(this.cssSelector);
                return (TData)Convert.ChangeType(htmlNode.InnerHtml, typeof(TData));
            }
            catch (Exception exception)
            {
                return default(TData);
            }            
        }
    }
}