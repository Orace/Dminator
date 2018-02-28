// 
// This file is a part of the Dminator project.
// https://github.com/Orace
// 

using System;
using System.Diagnostics;
using System.Threading;
using Dminator.Stategies;

namespace Dminator
{
    public static class Test
    {
        private const string MineSweeperPath = @"C:\Program Files\Microsoft Games\Minesweeper\MineSweeper.exe";

        private static void Main()
        {
            var process = GetMineSweeper();
            if (process == null)
            {
                Console.WriteLine("Ready to exit...");
                Console.ReadKey();
                return;
            }

            var chooser = new Chooser();
            //chooser.Register(CornerStrategy.Instance);
            chooser.Register(DumbStrategy.Instance);
            chooser.Register(SafeStrategy.Instance);
            chooser.Register(RandomStrategy.Instance);

            for (;;)
            {
                Console.Write("Scanning... ");
                var mineGame = new MineGameCache(new MineGameProxy(process));
                Console.WriteLine("[done]");

                Console.WriteLine("Ready. Press a key...");
                Console.ReadKey();

                for (;;)
                {
                    var bestChoice = chooser.GetBestChoice(mineGame);
                    if (mineGame.DigAt(bestChoice))
                    {
                        Console.WriteLine("KABOUM. Press a key...");
                        Console.ReadKey();
                        break;
                    }

                    if (mineGame.IsGameFinished())
                    {
                        Console.WriteLine("Looks like a win. Press a key...");
                        Console.ReadKey();
                        break;
                    }
                }
            }
        }

        private static Process GetMineSweeper()
        {
            var processes = Process.GetProcessesByName("MineSweeper");
            if (processes.Length == 1)
                return processes[0];

            Console.Write("Can't find MineSweeper, try to start it...");
            Process.Start(MineSweeperPath);
            Thread.Sleep(2000);

            processes = Process.GetProcessesByName("MineSweeper");
            if (processes.Length != 1)
            {
                Console.WriteLine("[failed]");
                return null;
            }

            Console.WriteLine("[done]");
            return processes[0];
        }
    }
}