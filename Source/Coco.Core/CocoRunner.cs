namespace Coco.Core
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal class CocoRunner<TDto> : IRunner<TDto>
    {
        private readonly ICocoSource<TDto> source;

        public CocoRunner(ICocoSource<TDto> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            this.source = source;
        }

        public Task<IEnumerable<TDto>> Retrieve()
        {
            return this.source.Retrieve();
        }
    }
}