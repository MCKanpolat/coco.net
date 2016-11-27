namespace Coco.Web
{
    public interface IWebEntityConfiguration
    {
        IWebValueResolver<string> GetItemValueResolver();

        IWebValueResolver<object> GetPropertyValueResolver(string propertyId);
    }
}