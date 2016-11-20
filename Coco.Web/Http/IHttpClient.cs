namespace Coco.Web.Http
{
    using System;
    using System.Threading.Tasks;

    using Coco.Core.Web;

    public interface IHttpClient : IDisposable
    {
        Task<IHttpClientResponse> Get(Uri uri);
    }
}