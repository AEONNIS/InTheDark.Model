using InTheDark.Model.Maths;
using Leopotam.Ecs.Types;

namespace InTheDark.Model.Map
{
    public interface IHalfWallSegment
    {
        public Int2 Start { get; }
        public Int2 End { get; }
        public IHalfWall Parent { get; }
        public int Length { get; }
        public DirectionSign Direction { get; }
        public IHalfWallSegment Twin { get; }
    }
}
