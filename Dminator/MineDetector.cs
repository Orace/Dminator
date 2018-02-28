// 
// This file is a part of the Dminator project.
// https://github.com/Orace
// 

using System.Linq;

namespace Dminator
{
    public class MineDetector
    {
        private readonly MineArray<bool> _mines;

        public MineDetector(IMineGame mineGame)
        {
            _mines = new MineArray<bool>(mineGame.LineCount, mineGame.ColumnCount);
            foreach (var point in mineGame.AllCells())
            {
                var undigedNeighbours = mineGame.GetNeighbours(point)
                    .Where(p => mineGame.NeighboursMineCountAt(p) == null).ToList();
                if (undigedNeighbours.Count == mineGame.NeighboursMineCountAt(point))
                    foreach (var undigedNeighbour in undigedNeighbours)
                        _mines[undigedNeighbour] = true;
            }
        }

        public bool IsSureAMine(CellCoordinate cellCoordinate)
        {
            return _mines[cellCoordinate];
        }
    }
}