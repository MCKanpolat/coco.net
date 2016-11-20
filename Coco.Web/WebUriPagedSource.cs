namespace Coco.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Coco.Core;
    using Coco.Web.Http;

    public class WebUriPagedSource<TDto> : ICocoSource<TDto>
        where TDto : new()
    {
        private readonly IHttpClientFactory clientFactory;

        private readonly Uri rootUri;

        private readonly IWebEntityConfiguration entityConfiguration;

        private readonly IPagingConfiguration pagingConfiguration;

        public WebUriPagedSource(
            IHttpClientFactory clientFactory,
            Uri rootUri, 
            IWebEntityConfiguration entityConfiguration,
            IPagingConfiguration pagingConfiguration)
        {
            this.clientFactory = clientFactory;
            this.rootUri = rootUri;
            this.entityConfiguration = entityConfiguration;
            this.pagingConfiguration = pagingConfiguration;
        }

        public async Task<IEnumerable<TDto>> Retrieve()
        {
            string firstPageContent;

            using (var client = this.clientFactory.Create())
            {
                var response = await client.Get(this.rootUri);
                response.EnsureSuccessStatusCode();
                firstPageContent = await response.GetContent();
            }

            if (string.IsNullOrWhiteSpace(firstPageContent))
            {
                return Enumerable.Empty<TDto>();
            }

            var pageDetails = this.pagingConfiguration.GetPageDetails(firstPageContent);
            if (pageDetails.TotalRecords == 0)
            {
                return Enumerable.Empty<TDto>();
            }

            var results = new List<TDto>();
            var currentPage = 1;          
            var currentRecord = this.pagingConfiguration.GetNextPage(pageDetails, pageDetails.StartPage);

            while (currentRecord != -1)
            {
                var pageSizeFragment =
                    string.IsNullOrWhiteSpace(this.pagingConfiguration.PageSizeParamName)
                        ? null
                        : $"{this.pagingConfiguration.PageSizeParamName}={pageDetails.PageSize}";

                var currentPageFragment =
                    string.IsNullOrWhiteSpace(this.pagingConfiguration.CurrentPageParamName)
                        ? null
                        : $"{(string.IsNullOrWhiteSpace(pageSizeFragment) ? null : "&")}{this.pagingConfiguration.CurrentPageParamName}={currentRecord}";

                var uri = $"{this.rootUri}&{pageSizeFragment}{currentPageFragment}";

                var source = new WebUriSource<TDto>(this.clientFactory, new Uri(uri), this.entityConfiguration);
                var items = await source.Retrieve();
                results.AddRange(items);              

                currentPage++;               
                currentRecord = this.pagingConfiguration.GetNextPage(pageDetails, currentRecord);
            }

            return results;
        }
    }
}