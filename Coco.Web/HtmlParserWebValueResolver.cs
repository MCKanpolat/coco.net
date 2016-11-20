namespace Coco.Web
{
    using System.Collections.Generic;
    using System.Linq;

    using Coco.Web.Parsers;

    public class HtmlParserWebValueResolver<TData> : IWebValueResolver<TData>
        where TData : class
    {
        private readonly IHtmlParser htmlParser;

        private readonly IWebValueResolver<TData> innerResolver;

        public HtmlParserWebValueResolver(IHtmlParser htmlParser, IWebValueResolver<TData> innerResolver)
        {
            this.htmlParser = htmlParser;
            this.innerResolver = innerResolver;
        }

        public TData GetValue(string content)
        {
            var value = this.innerResolver?.GetValue(content);
            if (value is string)
            {
                return this.htmlParser.Parse(value as string) as TData;
            }

            return value;
        }

        public IEnumerable<TData> GetValues(string content)
        {
            var values = this.innerResolver?.GetValues(content);
            if (values == null)
            {
                return Enumerable.Empty<TData>();
            }

            return values.Select(
                v =>
                    {
                        if (v is string)
                        {
                            return this.htmlParser.Parse(v as string) as TData;
                        }

                        return v;
                    });
        }
    }
}