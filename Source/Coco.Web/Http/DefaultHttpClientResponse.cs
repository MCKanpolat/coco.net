namespace Coco.Web.Http
{
    using System.Net.Http;
    using System.Threading.Tasks;

    using Coco.Core.Web;

    public class DefaultHttpClientResponse : IHttpClientResponse
    {
        private readonly HttpResponseMessage response;

        public DefaultHttpClientResponse(HttpResponseMessage response)
        {
            this.response = response;
        }

        public void EnsureSuccessStatusCode()
        {
            this.response.EnsureSuccessStatusCode();
        }

        public async Task<string> GetContent()
        {
            return await this.response.Content.ReadAsStringAsync();
        }
    }
}