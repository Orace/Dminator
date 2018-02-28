// 
// This file is a part of the Dminator project.
// https://github.com/Orace
// 

namespace Dminator
{
    public struct Choice
    {
        public Choice(CellCoordinate coordinate, double mineProbability)
        {
            Coordinate = coordinate;
            MineProbability = mineProbability;
        }

        public CellCoordinate Coordinate { get; }
        public double MineProbability { get; }
    }
}