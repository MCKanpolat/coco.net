namespace Coco.Web
{
    using System;

    public interface IValueResolverBuilder<in TData>
    {
        void InnerHtml(string cssSelection);
        
        void FromCss(string cssSelection);

        void Resolve(Func<string, TData> resolver);
    }
}