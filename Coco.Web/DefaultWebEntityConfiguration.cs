namespace Coco.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Coco.Web.Parsers;

    public class DefaultWebEntityConfiguration<TDto> : IWebEntityConfiguration
    {
        private readonly IDictionary<string, IWebValueResolver<object>> propertyValueResolvers;

        private DefaultItemValueResolverBuilder itemValueResolverBuilder;

        private readonly IHtmlParser parser;

        public DefaultWebEntityConfiguration(IHtmlParser htmlParser = null)
        {
            this.parser = htmlParser ?? new DefaultHtmlParser();
            this.propertyValueResolvers = new Dictionary<string, IWebValueResolver<object>>();
        }

        protected IValueResolverBuilder<string> Item()
        {
            this.itemValueResolverBuilder = new DefaultItemValueResolverBuilder();
            return this.itemValueResolverBuilder;
        }

        protected IValueResolverBuilder<TProperty> Property<TProperty>(Expression<Func<TDto, TProperty>> propertySelector)
        {
            if (propertySelector == null)
            {
                throw new ArgumentNullException(nameof(propertySelector));
            }

            var expression = propertySelector.Body as MemberExpression;
            if (expression == null)
            {
                throw new ArgumentException("Expression is not a property.");
            }

            return new PropertyValueResolverBuilder<TProperty>(
                this.propertyValueResolvers, 
                expression.Member.Id());
        }

        public IWebValueResolver<object> GetPropertyValueResolver(string propertyId)
        {
            var resolver = this.propertyValueResolvers.ContainsKey(propertyId) 
                ? this.propertyValueResolvers[propertyId]
                : null;

            return resolver;
        }

        public IWebValueResolver<string> GetItemValueResolver()
        {
            return this.itemValueResolverBuilder?.ItemValueResolver;
        }
    }
}