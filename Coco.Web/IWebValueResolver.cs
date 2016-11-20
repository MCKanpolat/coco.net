namespace Coco.Web
{
    using System.Collections.Generic;

    public interface IWebValueResolver<out TData>
    {        
        TData GetValue(string content);

        IEnumerable<TData> GetValues(string content);
    }
}