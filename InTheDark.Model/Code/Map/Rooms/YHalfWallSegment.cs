using InTheDark.Model.Maths;
using Leopotam.Ecs.Types;

namespace InTheDark.Model.Map
{
    public class YHalfWallSegment : IHalfWallSegment
    {
        public YHalfWallSegment(in Int2 start, int count, DirectionSign direction, YHalfWall parent)
        {
            Start = start;
            var endY = direction == DirectionSign.Positive ? start.Y + (count - 1) : start.Y - (count - 1);
            End = new Int2(start.X, endY);
            Count = count;
            Direction = direction;
            Parent = parent;
        }

        public Int2 Start { get; }
        public Int2 End { get; }
        public IHalfWall Parent { get; }
        public int Count { get; }
        public DirectionSign Direction { get; }
        public IHalfWallSegment Twin { get; private set; }

        public void SetTwin(YHalfWallSegment yHalfWallSegment)
        {
            Twin = yHalfWallSegment;
            yHalfWallSegment.Twin = this;
        }

        public bool Contains(int point) => Start.Y <= point && point <= End.Y;

        public (IHalfWallSegment FirstSegment, IHalfWallSegment SecondSegment) SplitAt(int point)
        {
            var (firstCount, secondCount) = GetCountsWhenSplitAt(point);
            var firstSegment = new YHalfWallSegment(Start, firstCount, Direction, Parent as YHalfWall);
            var secondSegment = new YHalfWallSegment(new Int2(Start.X, point), secondCount, Direction, Parent as YHalfWall);

            return (firstSegment, secondSegment);
        }

        private (int FirstCount, int SecondCount) GetCountsWhenSplitAt(int point) =>
            (MathFast.Abs(point - Start.Y + 1), MathFast.Abs(End.Y - point + 1));
    }
}
