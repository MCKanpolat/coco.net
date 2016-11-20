namespace Coco.Web
{
    using Coco.Core.Web;

    public class IncrementPageByPageSizeCalculator : INextPageCalculator
    {
        public int GetNextPage(PageDetails pageDetails, int currentPage)
        {
            return currentPage + pageDetails.PageSize > pageDetails.TotalRecords ? -1 : currentPage + pageDetails.PageSize;
        }
    }
}