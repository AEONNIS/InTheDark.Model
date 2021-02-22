using InTheDark.Model.Maths;
using Leopotam.Ecs.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace InTheDark.Model.Map
{
    public class RoomsRegion
    {
        private static readonly Random _random = new Random(DateTime.Now.Millisecond);
        private static readonly int _axisLength = Enum.GetValues(typeof(Axis)).Length;

        private readonly List<Room> _rooms;
        private readonly Int2 _size;

        public RoomsRegion(List<Room> rooms, Int2 size)
        {
            _rooms = rooms;
            _size = size;
        }

        public ReadOnlyCollection<Room> Rooms => _rooms.AsReadOnly();

        // Pass configuration in one parameter
        public static RoomsRegion CreateRandom(in Int2 size, in Int2 offset, int splitsNumber, float limitingWallSplitting)
        {
            RoomsRegion roomsRegion = new RoomsRegion(new List<Room>(splitsNumber + 1), size);
            roomsRegion._rooms.Add(new Room(offset, size));

            for (int i = 0; i < splitsNumber; i++)
                roomsRegion.RandomSplit(limitingWallSplitting);

            return roomsRegion;
        }

        private void RandomSplit(float limitingWallSplitting)
        {
            int i = GetLargestRoomIndex();
            Room room = _rooms[i];
            Axis splitDirection = GetSplitDirection(room);
            var (FirstRoom, SecondRoom) = splitDirection == Axis.X
                ? room.RandomlySplitByX(limitingWallSplitting)
                : room.RandomlySplitByY(limitingWallSplitting);
            _rooms[i] = FirstRoom;
            _rooms.Insert(i + 1, SecondRoom);
        }

        private int GetLargestRoomIndex()
        {
            int result = 0;
            int largestArea = _rooms[result].Area;

            for (int i = 1; i < _rooms.Count; i++)
            {
                int area = _rooms[i].Area;

                if (area > largestArea)
                {
                    result = i;
                    largestArea = area;
                }
            }

            return result;
        }

        private static Axis GetSplitDirection(Room room)
        {
            if (room.South.Length > room.West.Length)
                return Axis.X;
            else if (room.South.Length < room.West.Length)
                return Axis.Y;
            else
                return (Axis)_random.Next(0, _axisLength);
        }
    }
}
