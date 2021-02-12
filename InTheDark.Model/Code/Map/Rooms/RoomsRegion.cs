using InTheDark.Model.Components;
using Leopotam.Ecs.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace InTheDark.Model.Map
{
    internal class RoomsRegion
    {
        private static readonly Random _random = new Random(DateTime.Now.Millisecond);
        private static readonly int _mapGuideLength = Enum.GetValues(typeof(MapGuide)).Length;

        private readonly List<Room> _rooms;
        private readonly Int2 _size;

        internal RoomsRegion(List<Room> rooms, Int2 size)
        {
            _rooms = rooms;
            _size = size;
        }

        internal ReadOnlyCollection<Room> Rooms => _rooms.AsReadOnly();

        // Pass configuration in one parameter
        internal static RoomsRegion CreateRandom(in Int2 size, in Int2 offset, int _splitsNumber, float limitingWallSplitting)
        {
            var roomsRegion = new RoomsRegion(new List<Room>(_splitsNumber + 1), size);
            roomsRegion._rooms.Add(new Room(offset, offset + size - new Int2(1, 1)));

            for (int i = 0; i < _splitsNumber; i++)
                roomsRegion.RandomSplit(limitingWallSplitting);

            return roomsRegion;
        }

        private static MapGuide GetSplitDirection(Room room)
        {
            if (room.South.Length > room.East.Length)
                return MapGuide.WestEast;
            else if (room.South.Length < room.East.Length)
                return MapGuide.SouthNorth;
            else
                return (MapGuide)_random.Next(0, _mapGuideLength);
        }

        private static float GetRandomSplitPosition(float limitingWallSplitting)
            => (float)(_random.NextDouble() * (1 - limitingWallSplitting) + limitingWallSplitting);

        internal List<ForegroundTileComponent> GetTiles()
        {

        }

        private void RandomSplit(float limitingWallSplitting)
        {
            var i = GetLargestRoomIndex();
            var room = _rooms[i];
            var splitDirection = GetSplitDirection(room);
            (Room FirstRoom, Room SecondRoom) = splitDirection == MapGuide.SouthNorth
                ? room.SplitIntoSouthNorthAt(GetRandomSplitPosition(limitingWallSplitting))
                : room.SplitIntoWestEastAt(GetRandomSplitPosition(limitingWallSplitting));
            _rooms[i] = FirstRoom;
            _rooms.Insert(i + 1, SecondRoom);
        }

        private int GetLargestRoomIndex()
        {
            int result = 0;
            int area = _rooms[result].Area;

            for (int i = 1; i < _rooms.Count; i++)
            {
                if (_rooms[i].Area > area)
                    result = i;
            }

            return result;
        }
    }
}
