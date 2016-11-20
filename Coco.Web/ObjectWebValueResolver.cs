namespace Coco.Web
{
    using System.Collections.Generic;
    using System.Linq;

    public class ObjectWebValueResolver<TData> : IWebValueResolver<object>
    {
        private readonly IWebValueResolver<TData> webValueResolver;
        
        public ObjectWebValueResolver(IWebValueResolver<TData> webValueResolver)
        {
            this.webValueResolver = webValueResolver;
        }

        public object GetValue(string content)
        {
            return this.webValueResolver.GetValue(content);
        }

        public IEnumerable<object> GetValues(string content)
        {
            return this.webValueResolver.GetValues(content).OfType<object>();
        }
    }
}