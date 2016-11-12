namespace Coco.Core.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    public class WebContentSource<TDto> : ICocoSource<TDto>
        where TDto : new()
    {
        private readonly string content;

        private readonly IWebEntityConfiguration configuration;

        public WebContentSource(string content, IWebEntityConfiguration configuration)
        {
            this.content = content;
            this.configuration = configuration;
        }

        public Task<IEnumerable<TDto>> Retrieve()
        {
            var items = new List<TDto>();

            var writeableProperties = typeof(TDto)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanWrite);

            foreach (var property in writeableProperties)
            {
                var resolver = this.configuration.GetResolver(property.Id());
                if (resolver == null)
                {
                    throw new InvalidOperationException($"No value resolver registered for {property.Id()}");
                }

                var instance = new TDto();
                var value = resolver.GetMe(this.content);
                property.SetValue(instance, Convert.ChangeType(value, property.PropertyType), null);
                items.Add(instance);
            }

            return Task.FromResult(items.AsEnumerable());
        }
    }
}