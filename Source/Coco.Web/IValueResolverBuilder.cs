namespace Coco.Web
{
    using System;

    using Coco.Web.Parsers;

    public interface IValueResolverBuilder<in TData>
    {
        void InnerHtml(string cssSelection, IHtmlParser parser = null);
        
        void FromCss(string cssSelection);

        void Resolve(Func<string, TData> resolver);
    }
}