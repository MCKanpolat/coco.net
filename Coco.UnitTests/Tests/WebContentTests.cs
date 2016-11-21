namespace Coco.UnitTests.Tests
{
    using System;

    using Coco.UnitTests.Configurations;
    using Coco.UnitTests.Models;
    using Coco.Web;
    using Coco.Web.Http;

    using Xunit;

    public class WebContentTests
    {
        [Fact]
        public async void UriSourceReturnsItems()
        {
            var source = new WebUriSource<GoogleResult>(
                new Uri("https://www.google.co.uk/search?q=trump"), 
                new GoogleResultConfiguration());

            var results = await source.Retrieve();
            Assert.NotNull(results);
        }

        [Fact]
        public async void PagedUriSourceReturnsItems()
        {
            var source = new WebUriPagedSource<GoogleResult>(                
                new Uri("https://www.google.co.uk/search?q=trump"),
                new GoogleResultConfiguration(), 
                new GooglePagingConfiguration());

            var results = await source.Retrieve();
            Assert.NotNull(results);
        }
    }
}