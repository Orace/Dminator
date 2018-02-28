// 
// This file is a part of the Dminator project.
// https://github.com/Orace
// 

using System.Collections.Generic;

namespace Dminator
{
    public class MineGameCache : IMineGame
    {
        private readonly MineArray<int?> _cache;
        private readonly IMineGame _subject;

        public MineGameCache(IMineGame subject)
        {
            _subject = subject;

            ColumnCount = subject.ColumnCount;
            LineCount = subject.LineCount;

            _cache = new MineArray<int?>(LineCount, ColumnCount);
        }

        public int ColumnCount { get; }
        public int LineCount { get; }

        public bool DigAt(CellCoordinate coordinate)
        {
            var result = _subject.DigAt(coordinate);
            var visitedCells = new HashSet<CellCoordinate>();
            UpdateCache(visitedCells, coordinate);
            return result;
        }

        public int? NeighboursMineCountAt(CellCoordinate coordinate)
        {
            return _cache[coordinate];
        }

        private void UpdateCache(ISet<CellCoordinate> visitedCells, CellCoordinate coordinate)
        {
            if (!visitedCells.Add(coordinate)) return;

            var neighboursMineCount = _subject.NeighboursMineCountAt(coordinate);
            _cache[coordinate] = neighboursMineCount;
            if (neighboursMineCount == 0)
                foreach (var neighbour in this.GetNeighbours(coordinate))
                    UpdateCache(visitedCells, neighbour);
        }
    }
}