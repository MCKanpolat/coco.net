namespace Coco.Core.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public class WebEntityConfiguration<TDto> : IWebEntityConfiguration
    {
        private readonly IDictionary<string, IPropertyValueResolver<object>> propertyValueResolvers;

        public WebEntityConfiguration()
        {
            this.propertyValueResolvers = new Dictionary<string, IPropertyValueResolver<object>>();
        }

        protected IValueResolverBuilder<string> Item()
        {
            return new DefaultItemValueResolverBuilder();
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

        public IPropertyValueResolver<object> GetResolver(string propertyId)
        {
            var resolver = this.propertyValueResolvers.ContainsKey(propertyId) 
                ? this.propertyValueResolvers[propertyId]
                : null;

            return resolver;
        }
    }
}