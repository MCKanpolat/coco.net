namespace Coco.Web
{
    public class Percent
    {
        private double value;

        public double Value
        {
            get
            {
                return this.value;
            }

            set
            {
                this.value = value;
                if (this.value < 0.0)
                {
                    this.value = 0.0;
                }
                else if (this.value > 100.0)
                {
                    this.value = 100.0;
                }
            }
        }

        public Percent(double percentComplete)
        {
            this.Value = percentComplete;
        }
    }
}