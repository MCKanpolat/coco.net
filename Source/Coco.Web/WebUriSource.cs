namespace Coco.Web
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Coco.Core;
    using Coco.Web.Http;

    public class WebUriSource<TDto> : ICocoSource<TDto>
        where TDto : new()
    {
        private readonly Uri uri;

        private readonly IWebEntityConfiguration configuration;

        private readonly IHttpClientFactory clientFactory;

        public WebUriSource(
            Uri uri, 
            IWebEntityConfiguration configuration,
            IHttpClientFactory clientFactory = null)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            this.uri = uri;
            this.configuration = configuration;
            this.clientFactory = clientFactory ?? new DefaultHttpClientFactory();
        }

        public async Task<IEnumerable<TDto>> Retrieve()
        {
            using (var client = this.clientFactory.Create())
            {
                var response = await client.Get(this.uri);
                response.EnsureSuccessStatusCode();
                var content = await response.GetContent();
                var innerSource = new WebEntityContentSource<TDto>(content, this.configuration);
                return await innerSource.Retrieve();
            }
        }
    }
}
