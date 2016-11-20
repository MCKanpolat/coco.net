namespace Coco.UnitTests.Configurations
{
    using Coco.UnitTests.Models;
    using Coco.Web;

    public class GoogleResultConfiguration : DefaultWebEntityConfiguration<GoogleResult>
    {
        public GoogleResultConfiguration()
        {
            this.Item().InnerHtml("div.g");
            this.Property(r => r.Title).InnerHtml("h3.r a");
            this.Property(r => r.Description).InnerHtml("span.st");
        }
    }
}