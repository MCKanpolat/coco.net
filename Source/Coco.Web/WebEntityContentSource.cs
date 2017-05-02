namespace Coco.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using Coco.Core;
    using Coco.Web.Parsers;

    public class WebContentSource 

    public class WebEntityContentSource<TDto> : ICocoSource<TDto>
        where TDto : new()
    {
        private readonly string content;

        private readonly IWebEntityConfiguration configuration;

        public WebEntityContentSource(
            string content)
        {
            this.content = content;
            this.configuration = configuration;
        }

        public Task<IEnumerable<TDto>> Retrieve()
        {
            var dtos = new List<TDto>();

            var itemValueResolver = this.configuration.GetItemValueResolver();
            if (itemValueResolver == null)
            {
                throw new InvalidOperationException("No item configuration setup, use this.Item in your configuration");
            }

            var items = itemValueResolver.GetValues(this.content);

            var writeableProperties = typeof(TDto)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanWrite)
                .ToList();

            foreach (var itemContent in items)
            {
                var instance = new TDto();
                
                foreach (var property in writeableProperties)
                {
                    var resolver = this.configuration.GetPropertyValueResolver(property.Id());
                    if (resolver == null)
                    {
                        throw new InvalidOperationException($"No value resolver registered for {property.Id()}");
                    }
            
                    var value = resolver.GetValue(itemContent);
                    property.SetValue(instance, Convert.ChangeType(value, property.PropertyType), null);
                }

                dtos.Add(instance);
            }

            return Task.FromResult(dtos.AsEnumerable());
        }
    }
}