namespace Coco.UnitTests.Tests
{
    using System;
    using System.Data;

    using Coco.Core.Web;

    using Xunit;

    public class WebContentTests
    {
        [Fact]
        public async void Foo()
        {
            var webContent = new WebUriSource<GoogleResult>(
                new Uri("https://www.google.co.uk/search?q=trump"), 
                new GoogleResultConfiguration());

            var results = await webContent.Retrieve();
            Assert.NotNull(results);
        }
    }

    public class GoogleResultConfiguration : WebEntityConfiguration<GoogleResult>
    {
        public GoogleResultConfiguration()
        {
            this.Item().InnerHtml("div.g");
            this.Property(r => r.Title).InnerHtml("h3.r a");
            this.Property(r => r.Description).InnerHtml("span.st");
        }
    }

    public class GoogleResult
    {
        public string Title { get; set; }

        public string Description { get; set; }
    }
}