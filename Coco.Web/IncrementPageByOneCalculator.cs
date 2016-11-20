namespace Coco.Web
{
    using Coco.Core.Web;

    public class IncrementPageByOneCalculator : INextPageCalculator
    {
        public int GetNextPage(PageDetails pageDetails, int currentPage)
        {
            return currentPage >= pageDetails.TotalPages ? -1 : currentPage + 1;
        }
    }
}