// 
// This file is a part of the Dminator project.
// https://github.com/Orace
// 

using System.Collections.Generic;
using System.Linq;

namespace Dminator
{
    public static class MineGameEx
    {
        public static IEnumerable<CellCoordinate> AllCells(this IMineGame mineGame)
        {
            for (var l = 0; l < mineGame.LineCount; l++)
            for (var c = 0; c < mineGame.ColumnCount; c++)
                yield return new CellCoordinate(l, c);
        }

        public static IEnumerable<CellCoordinate> GetNeighbours(this IMineGame mineGame, CellCoordinate coordinate)
        {
            for (var dl = -1; dl <= 1; dl++)
            for (var dc = -1; dc <= 1; dc++)
            {
                if (dl == 0 && dc == 0)
                    continue;

                var l = coordinate.Line + dl;
                var c = coordinate.Column + dc;

                if (l >= 0 && l < mineGame.LineCount && c >= 0 && c < mineGame.ColumnCount)
                    yield return new CellCoordinate(l, c);
            }
        }

        public static bool IsGameFinished(this IMineGame mineGame)
        {
            var mines = new MineDetector(mineGame);
            return mineGame.AllCells().Where(c => mineGame.NeighboursMineCountAt(c) == null)
                .All(c => mines.IsSureAMine(c));
        }
    }
}