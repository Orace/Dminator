// 
// This file is a part of the Dminator project.
// https://github.com/Orace
// 

using System.Collections;
using System.Collections.Generic;

namespace Dminator
{
    public class MineArray<T> : IEnumerable<T>
    {
        private readonly T[,] _data;

        public MineArray(int lineCount, int columnCount)
        {
            _data = new T[lineCount, columnCount];
        }

        public T this[CellCoordinate coordinate]
        {
            get => _data[coordinate.Line, coordinate.Column];
            set => _data[coordinate.Line, coordinate.Column] = value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i < _data.GetLength(0); i++)
            for (var j = 0; j < _data.GetLength(1); j++)
                yield return _data[i, j];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}