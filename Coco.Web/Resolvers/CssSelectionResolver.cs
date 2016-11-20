namespace Coco.Core.Web.Resolvers
{
    using System;
    using System.Collections.Generic;

    using Coco.Web;

    public class CssSelectionResolver<TData> : IWebValueResolver<TData>
    {
        private readonly string cssSelection;

        public CssSelectionResolver(string cssSelection)
        {
            if (string.IsNullOrWhiteSpace(cssSelection))
            {
                throw new ArgumentNullException(nameof(cssSelection));
            }

            this.cssSelection = cssSelection;
        }

        public TData GetValue(string content)
        {
            throw new NotImplementedException("remove");
        }

        public IEnumerable<TData> GetValues(string content)
        {
            throw new NotImplementedException("remove");
        }
    }
}