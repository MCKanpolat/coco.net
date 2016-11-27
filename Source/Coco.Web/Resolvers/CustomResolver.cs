namespace Coco.Core.Web.Resolvers
{
    using System;
    using System.Collections.Generic;

    using Coco.Web;

    public class CustomResolver<TData> : IWebValueResolver<TData>
    {
        private readonly Func<string, TData> resolver;

        public CustomResolver(Func<string, TData> resolver)
        {
            this.resolver = resolver;
        }

        public TData GetValue(string content)
        {
            return this.resolver(content);
        }

        public IEnumerable<TData> GetValues(string content)
        {
            throw new NotImplementedException();
        }
    }
}