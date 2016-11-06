namespace Coco.Core
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICocoSource<TDto>
    {
        Task<IEnumerable<TDto>> Retrieve();
    }
}