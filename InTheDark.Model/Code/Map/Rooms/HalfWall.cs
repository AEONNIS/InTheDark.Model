using InTheDark.Model.Components;
using InTheDark.Model.Maths;
using Leopotam.Ecs.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace InTheDark.Model.Map
{
    internal class HalfWall
    {
        private readonly List<HalfWallSegment> _segments = new List<HalfWallSegment>();

        // Есть возможность создать не только вертикальную/горизонтальную стену.
        internal HalfWall(in Int2 start, in Int2 end, Room room)
        {
            _segments.Add(new HalfWallSegment(start, end, this));
            Room = room;
        }

        internal ReadOnlyCollection<HalfWallSegment> Segments => _segments.AsReadOnly();
        internal Room Room { get; }

        internal Int2 Start => _segments[0].Start;
        internal Int2 End => _segments[_segments.Count - 1].End;
        internal bool IsHorizontal => Start.Y == End.Y;
        internal int Length => Math.Abs(LengthNotAbsolute);
        private int LengthNotAbsolute => IsHorizontal ? Start.X - End.X : Start.Y - End.Y;

        // You need to use when splitting
        internal void SplitIntoSegmentsAt(float position)
        {
            var point = GetPointIn(position);
            var i = GetSegmentIndexIn(point);
            var (FirstSegment, SecondSegment) = _segments[i].SplitAt(point);
            _segments[i] = FirstSegment;
            _segments.Insert(i + 1, SecondSegment);
        }

        internal Int2 GetPointIn(float position) => IsHorizontal
            ? new Int2(new RangeInt(Start.X, End.X).Split(position), Start.Y)
            : new Int2(Start.X, new RangeInt(Start.Y, End.Y).Split(position));

        internal List<ForegroundTileComponent> GetTiles()
        {
            var result = new List<ForegroundTileComponent>();

            if (IsHorizontal)
            {
                for (int x = Start.X; x <= End.X; x++)
                    result.Add(new ForegroundTileComponent { Position = new Int2(x, Start.Y), Id = ForegroundTileId.Wall });
            }
            else
            {
                for (int y = Start.Y; y <= End.Y; y++)
                    result.Add(new ForegroundTileComponent { Position = new Int2(Start.X, y), Id = ForegroundTileId.Wall });
            }

            return result;
        }

        private int GetSegmentIndexIn(Int2 point) => _segments.FindIndex(segment => segment.Contains(point));
    }
}
