using InTheDark.Model.Maths;
using Leopotam.Ecs.Types;

namespace InTheDark.Model.Map
{
    // Is an interface required?
    public interface IHalfWallSegment
    {
        public Int2 Start { get; }
        public Int2 End { get; }
        public IHalfWall Parent { get; }
        public int Count { get; }
        public DirectionSign Direction { get; }
        public IHalfWallSegment Twin { get; }

        public bool Contains(int point);

        public (IHalfWallSegment FirstSegment, IHalfWallSegment SecondSegment) SplitAt(int point);
    }
}
