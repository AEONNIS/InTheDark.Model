using System;

namespace InTheDark.Model.Maths
{
    public readonly struct RangeInt
    {
        private readonly static Random _random = new Random(DateTime.Now.Millisecond);
        private readonly Range<int> _range;

        public RangeInt(int min, int max) => _range = new Range<int>(min, max);

        public int Min => _range.Min;
        public int Max => _range.Max;
        public int Length => Max - Min;
        public int RandomValue => Min < Max ? _random.Next(Min, Max + 1) : _random.Next(Max, Min + 1);

        public int GetLimitedRandomValue(int shiftLimits) => Min < Max
            ? _random.Next(Min + shiftLimits, Max - shiftLimits + 1)
            : _random.Next(Max + shiftLimits, Min - shiftLimits + 1);

        public int GetLimitedRandomValue(float relativeShiftLimits) => GetLimitedRandomValue(GetShiftLimits(relativeShiftLimits));

        private int GetShiftLimits(float relativeShiftLimits) =>
            (int)MathF.Abs(MathF.Round(Length * relativeShiftLimits, MidpointRounding.AwayFromZero));
    }
}
