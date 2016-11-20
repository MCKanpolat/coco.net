namespace Coco.Web.Http
{
    public interface IHttpClientFactory
    {
        IHttpClient Create();
    }
}