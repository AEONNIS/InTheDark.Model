using InTheDark.Model.Maths;
using Leopotam.Ecs.Types;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace InTheDark.Model.Map
{
    public class YHalfWall : IHalfWall
    {
        private readonly List<YHalfWallSegment> _segments = new List<YHalfWallSegment>();

        public YHalfWall(in Int2 start, int count, DirectionSign direction, Room room)
        {
            _segments.Add(new YHalfWallSegment(start, count, direction, this));
            Direction = direction;
            Room = room;
        }

        public Int2 Start => _segments[0].Start;
        public Int2 End => _segments[^1].End;
        public int Count => _segments.Select(segment => segment.Count).Sum() - _segments.Count + 1;
        public DirectionSign Direction { get; }
        public Room Room { get; }
        public ReadOnlyCollection<IHalfWallSegment> Segments => _segments.Select(segment => segment as IHalfWallSegment).ToList().AsReadOnly();

        // You need to use when splitting
        public void SplitIntoSegmentsAtRandom(float relativeShiftLimits)
        {
            var point = new RangeInt(Start.Y, End.Y).GetRandomValueByCount(relativeShiftLimits);
            var i = GetSegmentIndexIn(point);
            var (FirstSegment, SecondSegment) = _segments[i].SplitAt(point);
            _segments[i] = FirstSegment as XHalfWallSegment;
            _segments.Insert(i + 1, SecondSegment as XHalfWallSegment);
        }

        private int GetSegmentIndexIn(int point) => _segments.FindIndex(segment => segment.Contains(point));
    }
}
