// 
// This file is a part of the Dminator project.
// https://github.com/Orace
// 

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Automation;

namespace Dminator
{
    public class MineGameProxy : IMineGame
    {
        private static readonly Regex CoordRegex = new Regex(@"Ligne (?<line>\d*), Colonne (?<column>\d*)");

        private readonly Dictionary<CellCoordinate, AutomationElement> _infos;
        private readonly Process _process;

        public MineGameProxy(Process process)
        {
            _process = process;
            var mainWindow = AutomationElement.RootElement.FindFirst(
                TreeScope.Children,
                new PropertyCondition(AutomationElement.ProcessIdProperty, process.Id));

            _infos = new Dictionary<CellCoordinate, AutomationElement>();

            var l = Console.CursorLeft;
            var i = 0;

            var possibleLines = mainWindow.FindAll(TreeScope.Children, Condition.TrueCondition);
            foreach (AutomationElement line in possibleLines)
            {
                Console.CursorLeft = l;
                Console.Write($" {++i}/{possibleLines.Count} ");

                foreach (AutomationElement element in line.FindAll(TreeScope.Children, Condition.TrueCondition))
                {
                    var name = element.Current.Name;
                    if (IsCell(name)) _infos.Add(GetCoords(name), element);
                }
            }

            ColumnCount = _infos.Keys.Select(tuple => tuple.Column).Max() + 1;
            LineCount = _infos.Keys.Select(tuple => tuple.Line).Max() + 1;
        }

        public int ColumnCount { get; }
        public int LineCount { get; }

        public bool DigAt(CellCoordinate coordinate)
        {
            var coords = _infos[coordinate].Current.BoundingRectangle;

            var xpos = (int) (coords.X + coords.Width / 2);
            var ypos = (int) (coords.Y + coords.Height / 2);

            for (var i = 0; i < 10; i++)
            {
                Win32.SetForegroundWindow(_process.MainWindowHandle);
                Win32.SetCursorPos(xpos, ypos);
                Win32.mouse_event(Win32.MouseeventfLeftdown, xpos, ypos, 0, 0);
                Win32.mouse_event(Win32.MouseeventfLeftup, xpos, ypos, 0, 0);
                Thread.Sleep(10);

                var name = _infos[coordinate].Current.Name;
                if (name.Contains("masqué")) continue;
                if (name.Contains("hors de danger")) return false;

                return true;
            }


            Console.WriteLine("Failed to click, content was: ");
            Console.WriteLine(_infos[coordinate].Current.Name);
            return true;
        }

        public int? NeighboursMineCountAt(CellCoordinate coordinate)
        {
            var name = _infos[coordinate].Current.Name;
            if (name.Contains("Aucune"))
                return 0;
            var index = name.IndexOf("Mine", StringComparison.InvariantCulture);
            return index < 0 ? (int?) null : int.Parse(name.Substring(index - 2, 1));
        }

        private static bool IsCell(string name)
        {
            return name.Contains("Carré");
        }

        private static CellCoordinate GetCoords(string name)
        {
            var match = CoordRegex.Match(name);
            var l = int.Parse(match.Groups["line"].Value);
            var c = int.Parse(match.Groups["column"].Value);
            return new CellCoordinate(l - 1, c - 1);
        }
    }
}