namespace Coco.Web
{
    using Coco.Core.Web;

    public interface INextPageCalculator
    {
        int GetNextPage(PageDetails pageDetails, int currentPage);
    }
}