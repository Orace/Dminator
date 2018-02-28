// 
// This file is a part of the Dminator project.
// https://github.com/Orace
// 

using System.Collections.Generic;
using System.Linq;

namespace Dminator.Stategies
{
    public class CornerStrategy : IStrategy
    {
        private CornerStrategy()
        {
        }

        public static CornerStrategy Instance { get; } = new CornerStrategy();

        public IEnumerable<Choice> GetChoices(IMineGame mineGame, MineDetector mineDetector)
        {
            return GetCandidates(mineGame)
                .Where(c => mineGame.NeighboursMineCountAt(c) == null && !mineDetector.IsSureAMine(c))
                .Select(c => new Choice(c, 0.1)).ToList();
        }

        private static IEnumerable<CellCoordinate> GetCandidates(IMineGame mineGame)
        {
            var c = mineGame.ColumnCount - 1;
            var l = mineGame.LineCount - 1;

            yield return new CellCoordinate(0, 0);
            yield return new CellCoordinate(l, 0);
            yield return new CellCoordinate(l, c);
            yield return new CellCoordinate(0, c);
        }
    }
}