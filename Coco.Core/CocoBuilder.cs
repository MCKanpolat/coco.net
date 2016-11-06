namespace Coco.Core
{
    using System;

    public class CocoBuilder<TDto>
    {        
        public IFromSourceBuilder<TDto> FromSource(ICocoSource<TDto> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new CocoFromSourceBuilder<TDto>(source);
        }
    }
}
