namespace InTheDark.Model.Maths
{
    public readonly struct Range<T> where T : unmanaged
    {
        public Range(T min, T max)
        {
            Min = min;
            Max = max;
        }

        public T Min { get; }
        public T Max { get; }
    }
}
