// 
// This file is a part of the Dminator project.
// https://github.com/Orace
// 

using System;

namespace Dminator
{
    public struct CellCoordinate : IEquatable<CellCoordinate>
    {
        public CellCoordinate(int line, int column)
        {
            Line = line;
            Column = column;
        }

        public int Column { get; }
        public int Line { get; }

        public bool Equals(CellCoordinate other)
        {
            return Column == other.Column && Line == other.Line;
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            return obj is CellCoordinate coordinate && Equals(coordinate);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Column * 397) ^ Line;
            }
        }

        public override string ToString()
        {
            return $"({Line}, {Column})";
        }
    }
}