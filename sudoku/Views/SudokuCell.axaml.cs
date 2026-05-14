using System;
using Avalonia.Controls;
using Avalonia.VisualTree;

namespace Sudoku.Views
{
    public partial class SudokuCell : UserControl
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public SudokuCell()
        {
            InitializeComponent();
        }
    }
}
