namespace Coco.Core
{
    public interface IFromSourceBuilder<TDto>
    {
        IRunner<TDto> Build();
    }
}