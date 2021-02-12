using Leopotam.Ecs.Types;
using System;

namespace InTheDark.Model.Map
{
    // _? It is possible to make inheritance for the horizontal and vertical segment. 
    internal class HalfWallSegment
    {
        private HalfWallSegment _twin = null;

        // _* Есть возможность создать не только вертикальный/горизонтальный сегмент.
        internal HalfWallSegment(in Int2 start, in Int2 end, HalfWall parent)
        {
            Start = start;
            End = end;
            Parent = parent;
        }

        internal Int2 Start { get; }
        internal Int2 End { get; }
        internal HalfWall Parent { get; }
        internal HalfWallSegment Twin
        {
            get => _twin;
            set
            {
                _twin = value;
                value._twin = this;
            }
        }

        internal bool IsHorizontal => Start.Y == End.Y;
        internal int Length => Math.Abs(LengthNotAbsolute);
        private int LengthNotAbsolute => IsHorizontal ? Start.X - End.X : Start.Y - End.Y;

        internal bool Contains(in Int2 point) => IsHorizontal
            ? Start.X <= point.X && point.X <= End.X
            : Start.Y <= point.Y && point.Y <= End.Y;

        // М.б. передана точка, не принадлежащая сегменту.
        internal (HalfWallSegment FirstSegment, HalfWallSegment SecondSegment) SplitAt(in Int2 point)
        {
            var firstSegment = new HalfWallSegment(Start, point, Parent);
            var secondSegment = new HalfWallSegment(point, End, Parent);

            return (firstSegment, secondSegment);
        }
    }
}
