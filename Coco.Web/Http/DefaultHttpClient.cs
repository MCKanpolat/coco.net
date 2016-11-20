namespace Coco.Web.Http
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Coco.Core.Web;

    public class DefaultHttpClient : IHttpClient
    {
        private readonly bool carryOnCookies;

        private readonly HttpClient client;

        private readonly HttpClientHandler httpHandler;

        private readonly CookieContainer cookieContainer;

        private readonly IList<string> previousCookies;

        public DefaultHttpClient(bool carryOnCookies = false)
        {
            this.carryOnCookies = carryOnCookies;
            this.previousCookies = new List<string>();

            this.cookieContainer = new CookieContainer();
            this.httpHandler = new HttpClientHandler { CookieContainer = this.cookieContainer };
            this.client = new HttpClient(this.httpHandler);
        }

        private void CopyCookiesToContainer(
            Uri baseAddress,
            IEnumerable<string> cookies,
            CookieContainer container)
        {
            foreach (var cookieValue in cookies)
            {
                var cookieParts = cookieValue.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                var firstCookieParts = cookieParts[0].Split(
                    new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

                container.Add(
                    baseAddress,
                    new Cookie(firstCookieParts[0], firstCookieParts.Length > 1 ? firstCookieParts[1] : string.Empty));
            }
        }

        public async Task<IHttpClientResponse> Get(Uri uri)
        {
            if (this.carryOnCookies)
            {
                var baseUrl = $"{uri.Scheme}://{uri.Authority}";
                this.CopyCookiesToContainer(new Uri(baseUrl), this.previousCookies, this.cookieContainer);
            }

            var response = await this.client.GetAsync(uri);

            if (this.carryOnCookies)
            {
                this.StoreResponseCookies(response, this.previousCookies);
            }

            return new DefaultHttpClientResponse(response);
        }

        private void StoreResponseCookies(HttpResponseMessage response, IList<string> cookies)
        {
            // If any Set-Cookie in the response, then store the cookies for the next request
            var cookieHeader = response.Headers.FirstOrDefault(kvp => kvp.Key == "Set-Cookie");
            if (cookieHeader.Key == null)
            {
                return;
            }

            foreach (var responseCookieValue in cookieHeader.Value)
            {
                if (!cookies.Contains(responseCookieValue))
                {
                    cookies.Add(responseCookieValue);
                }
            }
        }

        public void Dispose()
        {
            this.client.Dispose();
            this.httpHandler.Dispose();
        }
    }
}