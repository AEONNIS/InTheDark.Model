using Leopotam.Ecs.Types;

namespace InTheDark.Model.Map
{
    public class YHalfWallSegment
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="length">May be negative.</param>
        /// <param name="parent"></param>
        public YHalfWallSegment(in Int2 start, int length, YHalfWall parent)
        {
            Start = start;
            End = new Int2(start.X, start.Y + length);
            Parent = parent;
        }

        public Int2 Start { get; }
        public Int2 End { get; }
        /// <summary>
        /// May be negative.
        /// </summary>
        public int Length => End.Y - Start.Y;
        public YHalfWall Parent { get; }
        public YHalfWallSegment Twin { get; private set; }

        public static void SetTwin(YHalfWallSegment aSegment, YHalfWallSegment bSegment)
        {
            aSegment.Twin = bSegment;
            bSegment.Twin = aSegment;
        }

        public bool Contains(int y) => Start.Y <= y && y <= End.Y;

        public (YHalfWallSegment FirstSegment, YHalfWallSegment SecondSegment) SplitAt(int y) =>
            (new YHalfWallSegment(Start, y - Start.Y, Parent),
             new YHalfWallSegment(new Int2(Start.X, y), End.Y - y, Parent));
    }
}
