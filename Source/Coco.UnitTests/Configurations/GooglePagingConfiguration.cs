namespace Coco.UnitTests.Configurations
{
    using Coco.Web;

    public class GooglePagingConfiguration : IPagingConfiguration
    {
        public PageDetails GetPageDetails(string content)
        {
            return new PageDetails(10, 0, 30);
        }

        public int GetNextPage(PageDetails pageDetails, int currentPage)
        {
            return new IncrementPageByPageSizeCalculator().GetNextPage(pageDetails, currentPage);
        }

        public string PageSizeParamName => null;

        public string CurrentPageParamName => "start";
    }
}