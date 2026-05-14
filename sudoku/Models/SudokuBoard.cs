using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Sudoku.Models
{
    public class SudokuBoard
    {
        private int[,] _board = new int[9, 9];
        public IEnumerable<IEnumerable<int>> Board =>
                        Enumerable.Range(0, 9).Select(r => GetRow(r));
        public SudokuBoard() { }

        public IEnumerable<int> GetRow(int rowIndex)
        {
            for (int c = 0; c < 9; c++)
                yield return _board[rowIndex, c];
        }

        public IEnumerable<int> GetColumn(int colIndex)
        {
            for (int r = 0; r < 9; r++)
                yield return _board[r, colIndex];
        }

        public IEnumerable<int> GetBox(int index)
        {
            if (index < 0 || index >= 9)
                throw new ArgumentOutOfRangeException(nameof(index), "Box index must be 0-8");

            int startRow = (index / 3) * 3;
            int startCol = (index % 3) * 3;

            for (int r = 0; r < 3; r++)
                for (int c = 0; c < 3; c++)
                    yield return _board[startRow + r, startCol + c];
        }

        public int GetCell(int row, int column) => _board[row, column];

        public void SetCell(int row, int column, int value)
        {
            if (value < 0 || value > 9) throw new ArgumentException("Invalid Sudoku value");
            _board[row, column] = value;
        }
    }
}