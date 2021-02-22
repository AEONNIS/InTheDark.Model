using InTheDark.Model.Maths;
using Leopotam.Ecs.Types;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace InTheDark.Model.Map
{
    public class YHalfWall
    {
        private readonly List<YHalfWallSegment> _segments = new List<YHalfWallSegment>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="length">May be negative.</param>
        /// <param name="room"></param>
        public YHalfWall(in Int2 start, int length, Room room)
        {
            _segments.Add(new YHalfWallSegment(start, length, this));
            Room = room;
        }

        public Int2 Start => _segments[0].Start;
        public Int2 End => _segments[^1].End;
        /// <summary>
        /// May be negative.
        /// </summary>
        public int Length => _segments.Select(segment => segment.Length).Sum();
        public Room Room { get; }
        public ReadOnlyCollection<YHalfWallSegment> Segments => _segments.AsReadOnly();

        public void RandomlySplitIntoSegments(float relativeShiftLimits)
        {
            int y = new RangeInt(Start.Y, End.Y).GetLimitedRandomValue(relativeShiftLimits);
            int i = GetSegmentIndexIn(y);
            var (FirstSegment, SecondSegment) = _segments[i].SplitAt(y);
            _segments[i] = FirstSegment;
            _segments.Insert(i + 1, SecondSegment);
        }

        private int GetSegmentIndexIn(int y) => _segments.FindIndex(segment => segment.Contains(y));
    }
}
