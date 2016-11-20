namespace Coco.Web
{
    using System;

    public class PageDetails
    {
        public PageDetails(int pageSize, int startPage, int totalRecords)
        {
            this.PageSize = pageSize;
            this.StartPage = startPage;
            this.TotalRecords = totalRecords;
        }

        public int PageSize { get; }

        public int StartPage { get; }

        public int TotalRecords { get; }

        public int TotalPages
        {
            get
            {
                if (this.PageSize == 0 || this.TotalRecords == 0)
                {
                    return 0;
                }

                if (this.PageSize >= this.TotalRecords)
                {
                    return 1;
                }

                var numberOfPages = (double)this.TotalRecords / this.PageSize;
                return (int)Math.Ceiling(numberOfPages);
            }
        }
    }
}