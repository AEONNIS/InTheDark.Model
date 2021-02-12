namespace InTheDark.Model.Maths
{
    public readonly struct RangeFloat
    {
        private readonly Range<float> _range;

        public RangeFloat(float min, float max) => _range = new Range<float>(min, max);

        public float Min => _range.Min;
        public float Max => _range.Max;

        public float Split(float position) => Min + (Max - Min) * position;
    }
}
