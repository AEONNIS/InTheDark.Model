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
        public int Length => Min <= Max ? Max - Min : Min - Max;
        public int Count => Length + 1;
        public int RandomValue => Min <= Max ? _random.Next(Min, Max + 1) : _random.Next(Max, Min + 1);

        public int GetRandomValue(int shiftLimits) => Min < Max
            ? _random.Next(Min + shiftLimits, Max - shiftLimits + 1)
            : _random.Next(Max + shiftLimits, Min - shiftLimits + 1);

        public int GetRandomValue(float relativeShiftLimits) => GetRandomValue(GetShiftLimits(relativeShiftLimits));

        public int GetRandomValueByCount(float relativeShiftLimits) => GetRandomValue(GetShiftLimitsByCount(relativeShiftLimits));

        public int GetShiftLimits(float relativeShiftLimits)
            => (int)MathF.Round(Length * relativeShiftLimits, MidpointRounding.AwayFromZero);

        public int GetShiftLimitsByCount(float relativeShiftLimits)
            => (int)MathF.Round(Count * relativeShiftLimits, MidpointRounding.AwayFromZero);
    }
}
