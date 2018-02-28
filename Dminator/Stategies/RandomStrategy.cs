// 
// This file is a part of the Dminator project.
// https://github.com/Orace
// 

using System;
using System.Collections.Generic;
using System.Linq;

namespace Dminator.Stategies
{
    public class RandomStrategy : IStrategy
    {
        private readonly Random _random;

        private RandomStrategy()
        {
            _random = new Random();
        }

        public static RandomStrategy Instance { get; } = new RandomStrategy();

        public IEnumerable<Choice> GetChoices(IMineGame mineGame, MineDetector mineDetector)
        {
            var l = mineGame.AllCells()
                .Where(c => !mineDetector.IsSureAMine(c) && mineGame.NeighboursMineCountAt(c) == null).ToList();
            var cellCoordinate = l[_random.Next(l.Count)];
            return new[] {new Choice(cellCoordinate, 1)};
        }
    }
}