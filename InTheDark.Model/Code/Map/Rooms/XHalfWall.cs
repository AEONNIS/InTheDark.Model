using InTheDark.Model.Maths;
using Leopotam.Ecs.Types;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace InTheDark.Model.Map
{
    public class XHalfWall
    {
        private readonly List<XHalfWallSegment> _segments = new List<XHalfWallSegment>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="length">May be negative.</param>
        /// <param name="room"></param>
        public XHalfWall(in Int2 start, int length, Room room)
        {
            _segments.Add(new XHalfWallSegment(start, length, this));
            Room = room;
        }

        public Int2 Start => _segments[0].Start;
        public Int2 End => _segments[^1].End;
        /// <summary>
        /// May be negative.
        /// </summary>
        public int Length => _segments.Select(segment => segment.Length).Sum();
        public Room Room { get; }
        public ReadOnlyCollection<XHalfWallSegment> Segments => _segments.AsReadOnly();

        public void RandomlySplitIntoSegments(float relativeShiftLimits)
        {
            int x = new RangeInt(Start.X, End.X).GetLimitedRandomValue(relativeShiftLimits);
            int i = GetSegmentIndexIn(x);
            var (FirstSegment, SecondSegment) = _segments[i].SplitAt(x);
            _segments[i] = FirstSegment;
            _segments.Insert(i + 1, SecondSegment);
        }

        private int GetSegmentIndexIn(int x) => _segments.FindIndex(segment => segment.Contains(x));
    }
}
