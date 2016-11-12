namespace Coco.Core.Web.Resolvers
{
    using System;

    public class CustomResolver<TData> : IPropertyValueResolver<TData>
    {
        private readonly Func<string, TData> resolver;

        public CustomResolver(Func<string, TData> resolver)
        {
            this.resolver = resolver;
        }

        public TData GetMe(string content)
        {
            return this.resolver(content);
        }
    }
}