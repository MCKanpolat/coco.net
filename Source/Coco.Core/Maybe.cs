namespace Coco.Core
{
    using System.Collections.Generic;
    using System.Linq;

    public class Maybe<T>
    {
        private readonly IEnumerable<T> values;

        public Maybe()
        {
            this.values = new T[0];
        }

        public Maybe(T value)
        {
            this.values = new[] { value };
        }

        public T FirstOrDefault()
        {
            return this.values.FirstOrDefault();
        }
    }
}