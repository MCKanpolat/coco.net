namespace Coco.UnitTests.Models
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Coco.Core;

    public class TestSource : ICocoSource<Test>
    {
        public async Task<IEnumerable<Test>> Retrieve()
        {
            var tests = new List<Test> { new Test { Name = "Test" } };
            return await Task.FromResult(tests);
        }
    }
}