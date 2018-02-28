// 
// This file is a part of the Dminator project.
// https://github.com/Orace
// 

namespace Dminator
{
    public interface IMineGame
    {
        int ColumnCount { get; }
        int LineCount { get; }
        bool DigAt(CellCoordinate coordinate);
        int? NeighboursMineCountAt(CellCoordinate coordinate);
    }
}