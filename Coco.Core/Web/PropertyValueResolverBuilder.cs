namespace Coco.Core.Web
{
    using System;
    using System.Collections.Generic;

    using Coco.Core.Web.Resolvers;

    public class PropertyValueResolverBuilder<TData> : IValueResolverBuilder<TData>
    {
        private readonly string propertyId;

        private readonly IDictionary<string, IPropertyValueResolver<object>> propertyValueResolvers;

        public PropertyValueResolverBuilder(IDictionary<string, IPropertyValueResolver<object>> propertyValueResolvers, string propertyId)
        {
            if (propertyValueResolvers == null)
            {
                throw new ArgumentNullException(nameof(propertyValueResolvers));
            }

            if (string.IsNullOrWhiteSpace(propertyId))
            {
                throw new ArgumentNullException(nameof(propertyId));
            }

            this.propertyValueResolvers = propertyValueResolvers;
            this.propertyId = propertyId;
        }

        public void InnerHtml(string cssSelector)
        {
            this.AddPropertyValueResolver(
                this.propertyId,
                new FooBar<TData>(new CssInnerHtmlResolver<TData>(cssSelector)));
        }

        public void FromCss(string cssSelection)
        {
            this.AddPropertyValueResolver(this.propertyId, new FooBar<TData>(new CssSelectionResolver<TData>(cssSelection)));
        }

        public void Resolve(Func<string, TData> resolver)
        {
            this.AddPropertyValueResolver(this.propertyId, new FooBar<TData>(new CustomResolver<TData>(resolver)));
        }

        private void AddPropertyValueResolver(string propertyId, IPropertyValueResolver<object> propertyValueResolver)
        {
            if (this.propertyValueResolvers.ContainsKey(propertyId))
            {
                throw new InvalidOperationException($"Property {propertyId} already has a value resolver registered");
            }

            this.propertyValueResolvers.Add(propertyId, propertyValueResolver);
        }
    }
}