namespace Coco.Web
{
    using Coco.Core.Web;

    public interface IPagingConfiguration
    {
        PageDetails GetPageDetails(string content);

        int GetNextPage(PageDetails pageDetails, int currentPage);

        string PageSizeParamName { get; }

        string CurrentPageParamName { get; }
    }
}