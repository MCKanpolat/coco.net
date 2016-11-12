namespace Coco.Core.Web
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class WebUriSource<TDto> : ICocoSource<TDto>
        where TDto : new()
    {
        private readonly Uri uri;

        private readonly IWebEntityConfiguration configuration;

        public WebUriSource(Uri uri, IWebEntityConfiguration configuration)
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
        } 

        public async Task<IEnumerable<TDto>> Retrieve()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(this.uri);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var innerSource = new WebContentSource<TDto>(content, this.configuration);
                return await innerSource.Retrieve();
            }
        }
    }
}
