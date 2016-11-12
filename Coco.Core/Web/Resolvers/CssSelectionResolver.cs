namespace Coco.Core.Web.Resolvers
{
    using System;

    public class CssSelectionResolver<TData> : IPropertyValueResolver<TData>
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

        public TData GetMe(string content)
        {
            throw new NotImplementedException("remove");
        }
    }
}