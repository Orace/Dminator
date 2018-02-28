// 
// This file is a part of the Dminator project.
// https://github.com/Orace
// 

using System;
using System.Collections.Generic;
using System.Linq;
using Dminator.Stategies;

namespace Dminator
{
    internal class Chooser
    {
        private readonly Random _random;
        private readonly ISet<IStrategy> _strategies;

        public Chooser()
        {
            _random = new Random();
            _strategies = new HashSet<IStrategy>();
        }

        public void Register(IStrategy strategy)
        {
            _strategies.Add(strategy);
        }

        public CellCoordinate GetBestChoice(IMineGame mineGame)
        {
            var mineDetector = new MineDetector(mineGame);
            var bestChoices = _strategies.SelectMany(s => s.GetChoices(mineGame, mineDetector))
                .OrderBy(c => c.MineProbability).GroupBy(c => c.MineProbability).First().ToList();
            return bestChoices[_random.Next(bestChoices.Count)].Coordinate;
        }
    }
}