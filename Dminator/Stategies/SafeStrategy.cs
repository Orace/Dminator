// 
// This file is a part of the Dminator project.
// https://github.com/Orace
// 

using System.Collections.Generic;
using System.Linq;

namespace Dminator.Stategies
{
    public class SafeStrategy : IStrategy
    {
        private SafeStrategy()
        {
        }

        public static SafeStrategy Instance { get; } = new SafeStrategy();

        public IEnumerable<Choice> GetChoices(IMineGame mineGame, MineDetector mineDetector)
        {
            return new Core(mineGame, mineDetector).Choices;
        }

        private class Core
        {
            public Core(IMineGame mineGame, MineDetector mineDetector)
            {
                Choices = FindSafePlace(mineGame, mineDetector).ToList();
            }

            public IReadOnlyList<Choice> Choices { get; }

            private static IEnumerable<Choice> FindSafePlace(IMineGame mineGame, MineDetector mineDetector)
            {
                foreach (var point in mineGame.AllCells())
                {
                    var neighbours = mineGame.GetNeighbours(point).ToList();
                    var minesCount1 = neighbours.Count(mineDetector.IsSureAMine);
                    var minesCount2 = mineGame.NeighboursMineCountAt(point);
                    if (minesCount1 == minesCount2)
                        foreach (var coordinate in neighbours.Where(
                            p => mineGame.NeighboursMineCountAt(p) == null && !mineDetector.IsSureAMine(p)))
                            yield return new Choice(coordinate, 0);
                }
            }
        }
    }
}