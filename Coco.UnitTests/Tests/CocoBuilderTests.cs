namespace Coco.UnitTests.Tests
{
    using System;

    using Coco.Core;
    using Coco.UnitTests.Models;

    using Ploeh.AutoFixture.Xunit2;

    using Xunit;

    public class CocoBuilderTests
    {
        [Theory]
        [AutoData]
        public void FromSourceWithNullShouldThrowException(CocoBuilder<Test> cocoBuilder)
        {
            Assert.Throws<ArgumentNullException>(() => cocoBuilder.FromSource(null));
        }

        [Fact]
        public async void RetrieveShouldReturnDtos()
        {
            var coco = new CocoBuilder<Test>().FromSource(new TestSource()).Build();
            var tests = await coco.Retrieve();
            Assert.NotNull(tests);
        }
    }
}
