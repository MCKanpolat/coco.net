namespace Coco.Web.Http
{
    using Coco.Core.Web;

    public class DefaultHttpClientFactory : IHttpClientFactory
    {
        private readonly bool carryOverCookies;

        public DefaultHttpClientFactory(bool carryOverCookies = false)
        {
            this.carryOverCookies = carryOverCookies;
        }

        public IHttpClient Create()
        {
            return new DefaultHttpClient(this.carryOverCookies);
        }
    }
}
