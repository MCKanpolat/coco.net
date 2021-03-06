﻿namespace Coco.Web
{
    using System;

    using Coco.Core.Web;
    using Coco.Core.Web.Resolvers;
    using Coco.Web.Parsers;

    public class DefaultItemValueResolverBuilder : IValueResolverBuilder<string>
    {
        private IWebValueResolver<string> itemValueResolver;
        
        public void InnerHtml(string cssSelection, IHtmlParser parser = null)
        {
            this.itemValueResolver = new CssInnerHtmlResolver<string>(cssSelection, parser);
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