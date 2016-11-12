namespace Coco.Core.Web
{
    public interface IPropertyValueResolver<out TData>
    {
        TData GetMe(string content);
    }
}