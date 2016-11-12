namespace Coco.Core.Web
{
    public interface IWebEntityConfiguration
    {
        IPropertyValueResolver<object> GetResolver(string propertyId);
    }
}