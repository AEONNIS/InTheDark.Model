using InTheDark.Model.Maths;
using Leopotam.Ecs.Types;

namespace InTheDark.Model.Map
{
    public class Room
    {
        public Room(in Int2 southWestAngle, in Int2 size)
        {
            South = new XHalfWall(southWestAngle, size.X, this);
            North = new XHalfWall(new Int2(southWestAngle.X, southWestAngle.Y + size.Y), size.X, this);
            West = new YHalfWall(southWestAngle, size.Y, this);
            East = new YHalfWall(new Int2(southWestAngle.X + size.X, southWestAngle.Y), size.Y, this);
        }

        public XHalfWall South { get; }
        public XHalfWall North { get; }
        public YHalfWall West { get; }
        public YHalfWall East { get; }
        public Int2 SouthWestAngle => South.Start;
        public Int2 SouthEastAngle => South.End;
        public Int2 NorthWestAngle => North.Start;
        public Int2 NorthEastAngle => North.End;
        public int Area => South.Length * West.Length;

        public (Room WestRoom, Room EastRoom) RandomlySplitByX(float relativeShiftLimits)
        {
            var (SouthPoint, NorthPoint) = GetSplitPointsByRandomX(relativeShiftLimits);
            Room westRoom = new Room(SouthWestAngle, NorthPoint - SouthWestAngle);
            Room eastRoom = new Room(SouthPoint, NorthEastAngle - SouthPoint);

            return (westRoom, eastRoom);
        }

        public (Room SouthRoom, Room NorthRoom) RandomlySplitByY(float relativeShiftLimits)
        {
            var (WestPoint, EastPoint) = GetSplitPointsByRandomY(relativeShiftLimits);
            Room southRoom = new Room(SouthWestAngle, EastPoint - SouthWestAngle);
            Room northRoom = new Room(WestPoint, NorthEastAngle - WestPoint);

            return (southRoom, northRoom);
        }

        private (Int2 SouthPoint, Int2 NorthPoint) GetSplitPointsByRandomX(float relativeShiftLimits)
        {
            int x = new RangeInt(South.Start.X, South.End.X).GetLimitedRandomValue(relativeShiftLimits);
            Int2 southPoint = new Int2(x, South.Start.Y);
            Int2 northPoint = new Int2(x, North.Start.Y);

            return (southPoint, northPoint);
        }

        private (Int2 WestPoint, Int2 EastPoint) GetSplitPointsByRandomY(float relativeShiftLimits)
        {
            int y = new RangeInt(West.Start.Y, West.End.Y).GetLimitedRandomValue(relativeShiftLimits);
            Int2 westPoint = new Int2(West.Start.X, y);
            Int2 eastPoint = new Int2(East.Start.X, y);

            return (westPoint, eastPoint);
        }
    }
}
