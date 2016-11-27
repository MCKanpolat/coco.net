namespace Coco.Web
{
    public class PagedPercentFactory
    {
        private readonly int totalPages;

        public PagedPercentFactory(int totalPages)
        {
            this.totalPages = totalPages;
        }

        public Percent CreatePercentFromCurrentPage(int currentPage)
        {
            if (this.totalPages == 0)
            {
                return new Percent(100.0);
            }

            if (currentPage >= this.totalPages)
            {
                return new Percent(100.0);
            }

            return new Percent((currentPage / (double)this.totalPages) * 100.0);
        }
    }
}