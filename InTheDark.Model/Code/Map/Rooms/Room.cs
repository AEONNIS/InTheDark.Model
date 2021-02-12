using InTheDark.Model.Components;
using Leopotam.Ecs.Types;
using System.Collections.Generic;

namespace InTheDark.Model.Map
{
    internal class Room
    {
        internal Room(in Int2 southWest, in Int2 northEast)
        {
            var angles = new Angles(southWest, northEast);
            South = new HalfWall(angles.SouthWest, angles.SouthEast, this);
            East = new HalfWall(angles.SouthEast, angles.NorthEast, this);
            North = new HalfWall(angles.NorthEast, angles.NorthWest, this);
            West = new HalfWall(angles.NorthWest, angles.SouthWest, this);
        }

        internal HalfWall South { get; }
        internal HalfWall East { get; }
        internal HalfWall North { get; }
        internal HalfWall West { get; }

        internal int Area => South.Length * East.Length;

        internal (Room WestRoom, Room EastRoom) SplitIntoWestEastAt(float position)
        {
            var southPoint = South.GetPointIn(position);
            var northPoint = North.GetPointIn(1 - position);
            var westRoom = new Room(South.Start, northPoint);
            var eastRoom = new Room(southPoint, North.Start);
            westRoom.East.Segments[0].Twin = eastRoom.West.Segments[0];

            return (westRoom, eastRoom);
        }

        internal (Room SouthRoom, Room NorthRoom) SplitIntoSouthNorthAt(float position)
        {
            var eastPoint = East.GetPointIn(position);
            var westPoint = West.GetPointIn(1 - position);
            var southRoom = new Room(South.Start, eastPoint);
            var northRoom = new Room(westPoint, North.Start);
            southRoom.North.Segments[0].Twin = northRoom.South.Segments[0];

            return (southRoom, northRoom);
        }

        internal List<ForegroundTileComponent> GetTiles()
        {
            // Convenient mechanism for moving to the next wall.
            var result = South.GetTiles();
            result.RemoveAt(0);
            var eastTiles = East.GetTiles();
            eastTiles.RemoveAt(0);
            var northTiles = North.GetTiles();
            northTiles.RemoveAt(0);
            var westTiles = West.GetTiles();
            westTiles.RemoveAt(0);

            result.AddRange(eastTiles);
            result.AddRange(northTiles);
            result.AddRange(westTiles);

            return result;
        }

        private readonly struct Angles
        {
            internal Angles(in Int2 southWest, in Int2 northEast)
            {
                SouthWest = southWest;
                SouthEast = new Int2(northEast.X, southWest.Y);
                NorthEast = northEast;
                NorthWest = new Int2(southWest.X, northEast.Y);
            }

            internal Int2 SouthWest { get; }
            internal Int2 SouthEast { get; }
            internal Int2 NorthEast { get; }
            internal Int2 NorthWest { get; }
        }
    }
}
