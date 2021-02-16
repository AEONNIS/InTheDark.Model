using InTheDark.Model.Maths;
using Leopotam.Ecs.Types;

namespace InTheDark.Model.Map
{
    public class XHalfWallSegment : IHalfWallSegment
    {
        public XHalfWallSegment(in Int2 start, int count, DirectionSign direction, XHalfWall parent)
        {
            Start = start;
            var endX = direction == DirectionSign.Positive ? start.X + (count - 1) : start.X - (count - 1);
            End = new Int2(endX, start.Y);
            Count = count;
            Direction = direction;
            Parent = parent;
        }

        public Int2 Start { get; }
        public Int2 End { get; }
        public int Count { get; }
        public DirectionSign Direction { get; }
        public IHalfWall Parent { get; }
        public IHalfWallSegment Twin { get; private set; }

        public void SetTwin(XHalfWallSegment xHalfWallSegment)
        {
            Twin = xHalfWallSegment;
            xHalfWallSegment.Twin = this;
        }

        public bool Contains(int point) => Start.X <= point && point <= End.X;

        public (IHalfWallSegment FirstSegment, IHalfWallSegment SecondSegment) SplitAt(int point)
        {
            var (firstCount, secondCount) = GetCountsWhenSplitAt(point);
            var firstSegment = new XHalfWallSegment(Start, firstCount, Direction, Parent as XHalfWall);
            var secondSegment = new XHalfWallSegment(new Int2(point, Start.Y), secondCount, Direction, Parent as XHalfWall);

            return (firstSegment, secondSegment);
        }

        private (int FirstCount, int SecondCount) GetCountsWhenSplitAt(int point) =>
            (MathFast.Abs(point - Start.X + 1), MathFast.Abs(End.X - point + 1));
    }
}
