namespace Coco.Web.Http
{
    using System.Threading.Tasks;

    public interface IHttpClientResponse
    {
        void EnsureSuccessStatusCode();

        Task<string> GetContent();
    }
}