namespace Coco.Core
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRunner<TDto>
    {
        Task<IEnumerable<TDto>> Retrieve();
    }
}