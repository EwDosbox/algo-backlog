using System;
using Sudoku.Models;

namespace Sudoku.Models
{
    public class Sudoku
    {
        private SudokuBoard _givens;
        private SudokuBoard _curr;
        public Sudoku(SudokuBoard givens)
        {
            _givens = givens;
            _curr = new();

            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    _curr.SetCell(i, j, givens.GetCell(i, j));
        }
        public void UpdateCell(int row, int column, int newValue)
        {
            _curr.SetCell(row, column, newValue);
        }
    }
}
