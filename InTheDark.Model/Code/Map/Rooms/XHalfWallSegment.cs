using InTheDark.Model.Maths;
using Leopotam.Ecs.Types;

namespace InTheDark.Model.Map
{
    public class XHalfWallSegment : IHalfWallSegment
    {
        public XHalfWallSegment(in Int2 start, int length, DirectionSign direction, XHalfWall parent)
        {
            Start = start;
            End = direction == DirectionSign.Positive ? new Int2(start.X + (length - 1), start.Y) : new Int2(start.X - (length - 1), start.Y);
            Length = length;
            Direction = direction;
            Parent = parent;
        }

        public Int2 Start { get; }
        public Int2 End { get; }
        public int Length { get; }
        public DirectionSign Direction { get; }
        public IHalfWall Parent { get; }
        public IHalfWallSegment Twin { get; private set; }

        public void SetTwin(XHalfWallSegment xHalfWallSegment)
        {
            Twin = xHalfWallSegment;
            xHalfWallSegment.Twin = this;
        }

        public bool Contains(int position) => Start.X <= position && position <= End.X;

        public (IHalfWallSegment FirstSegment, IHalfWallSegment SecondSegment) SplitAt(int position)
        {
            var firstLength = 
        }
    }
}
