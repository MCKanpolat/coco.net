namespace Coco.Web
{
    using System;

    using Coco.Core.Web;
    using Coco.Core.Web.Resolvers;

    public class DefaultItemValueResolverBuilder : IValueResolverBuilder<string>
    {
        private IWebValueResolver<string> itemValueResolver;
        
        public void InnerHtml(string cssSelection)
        {
            this.itemValueResolver = new CssInnerHtmlResolver<string>(cssSelection);
        }

        public void FromCss(string cssSelection)
        {
            this.itemValueResolver = new CssSelectionResolver<string>(cssSelection);
        }

        public void Resolve(Func<string, string> resolver)
        {
            this.itemValueResolver = new CustomResolver<string>(resolver);
        }

        public IWebValueResolver<string> ItemValueResolver => this.itemValueResolver;
    }
}