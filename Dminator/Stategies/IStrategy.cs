// 
// This file is a part of the Dminator project.
// https://github.com/Orace
// 

using System.Collections.Generic;

namespace Dminator.Stategies
{
    public interface IStrategy
    {
        IEnumerable<Choice> GetChoices(IMineGame mineGame, MineDetector mineDetector);
    }
}