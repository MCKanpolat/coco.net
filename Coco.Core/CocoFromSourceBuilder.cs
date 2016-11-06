namespace Coco.Core
{
    using System;

    internal class CocoFromSourceBuilder<TDto> : IFromSourceBuilder<TDto>
    {
        private readonly ICocoSource<TDto> source;

        public CocoFromSourceBuilder(ICocoSource<TDto> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            this.source = source;
        }

        public IRunner<TDto> Build()
        {
            return new CocoRunner<TDto>(this.source);
        }
    }
}