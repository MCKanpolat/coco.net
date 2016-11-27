namespace Coco.Web
{
    using System;
    using System.Collections.Generic;

    using Coco.Core.Web.Resolvers;
    using Coco.Web.Parsers;

    public class PropertyValueResolverBuilder<TData> : IValueResolverBuilder<TData>
    {
        private readonly string propertyId;

        private readonly IDictionary<string, IWebValueResolver<object>> propertyValueResolvers;

        public PropertyValueResolverBuilder(IDictionary<string, IWebValueResolver<object>> propertyValueResolvers, string propertyId)
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

        public void InnerHtml(string cssSelector, IHtmlParser parser = null)
        {
            this.AddPropertyValueResolver(
                this.propertyId,
                new ObjectWebValueResolver<TData>(
                    new CssInnerHtmlResolver<TData>(
                        cssSelector,
                        parser ?? new DefaultHtmlParser())));
        }

        public void FromCss(string cssSelection)
        {
            this.AddPropertyValueResolver(
                this.propertyId, 
                new ObjectWebValueResolver<TData>(
                    new CssSelectionResolver<TData>(cssSelection)));
        }

        public void Resolve(Func<string, TData> resolver)
        {
            this.AddPropertyValueResolver(
                this.propertyId, 
                new ObjectWebValueResolver<TData>(
                    new CustomResolver<TData>(resolver)));
        }

        private void AddPropertyValueResolver(
            string propertyId,
            IWebValueResolver<object> webValueResolver)
        {
            if (this.propertyValueResolvers.ContainsKey(propertyId))
            {
                throw new InvalidOperationException($"Property {propertyId} already has a value resolver registered");
            }

            this.propertyValueResolvers.Add(propertyId, webValueResolver);
        }
    }
}