// 
// This file is a part of the Dminator project.
// https://github.com/Orace
// 

using System.Collections.Generic;
using System.Linq;

namespace Dminator.Stategies
{
    public class DumbStrategy : IStrategy
    {
        private DumbStrategy()
        {
        }

        public static DumbStrategy Instance { get; } = new DumbStrategy();

        public IEnumerable<Choice> GetChoices(IMineGame mineGame, MineDetector mineDetector)
        {
            var result = new MineArray<double>(mineGame.LineCount, mineGame.ColumnCount);

            foreach (var coordinate in mineGame.AllCells())
            {
                var contant = mineGame.NeighboursMineCountAt(coordinate);
                if (contant == null)
                    continue;

                result[coordinate] = double.PositiveInfinity;

                var neighbours = mineGame.GetNeighbours(coordinate).ToList();
                var unclickedNeighbours =
                    neighbours.Where(coords => mineGame.NeighboursMineCountAt(coords) == null).ToList();
                if (unclickedNeighbours.Count == 0)
                    continue;

                var score = contant.Value / unclickedNeighbours.Count;
                foreach (var c in unclickedNeighbours) result[c] += score;
            }

            var bestScore = result.Min();
            var candidates = mineGame.AllCells().Where(coords => Equals(result[coords], bestScore));

            return candidates.Select(c => new Choice(c, 0.2)).ToList();
        }
    }
}