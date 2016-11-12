namespace Coco.Core.Web
{
    public class FooBar<TData> : IPropertyValueResolver<object>
    {
        private readonly IPropertyValueResolver<TData> propertyValueResolver;
        
        public FooBar(IPropertyValueResolver<TData> propertyValueResolver)
        {
            this.propertyValueResolver = propertyValueResolver;
        }

        public object GetMe(string content)
        {
            return this.propertyValueResolver.GetMe(content);
        }        
    }
}