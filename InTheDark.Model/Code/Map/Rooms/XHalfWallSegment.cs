using Leopotam.Ecs.Types;

namespace InTheDark.Model.Map
{
    public class XHalfWallSegment
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="length">May be negative.</param>
        /// <param name="parent"></param>
        public XHalfWallSegment(in Int2 start, int length, XHalfWall parent)
        {
            Start = start;
            End = new Int2(start.X + length, start.Y);
            Parent = parent;
        }

        public Int2 Start { get; }
        public Int2 End { get; }
        /// <summary>
        /// May be negative.
        /// </summary>
        public int Length => End.X - Start.X;
        public XHalfWall Parent { get; }
        public XHalfWallSegment Twin { get; private set; }

        public static void SetTwins(XHalfWallSegment aSegment, XHalfWallSegment bSegment)
        {
            aSegment.Twin = bSegment;
            bSegment.Twin = aSegment;
        }

        public bool Contains(int x) => Start.X <= x && x <= End.X;

        public (XHalfWallSegment FirstSegment, XHalfWallSegment SecondSegment) SplitAt(int x) =>
            (new XHalfWallSegment(Start, x - Start.X, Parent),
             new XHalfWallSegment(new Int2(x, Start.Y), End.X - x, Parent));
    }
}
