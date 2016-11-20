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
        private readonly IHttpClientFactory clientFactory;

        private readonly Uri uri;

        private readonly IWebEntityConfiguration configuration;

        public WebUriSource(IHttpClientFactory clientFactory, Uri uri, IWebEntityConfiguration configuration)
        {
            if (clientFactory == null)
            {
                throw new ArgumentNullException(nameof(clientFactory));
            }

            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            this.clientFactory = clientFactory;
            this.uri = uri;
            this.configuration = configuration;
        } 

        public async Task<IEnumerable<TDto>> Retrieve()
        {
            using (var client = this.clientFactory.Create())
            {
                var response = await client.Get(this.uri);
                response.EnsureSuccessStatusCode();
                var content = await response.GetContent();
                var innerSource = new WebContentSource<TDto>(content, this.configuration);
                return await innerSource.Retrieve();
            }
        }
    }
}
