using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.VisualTree;
using Avalonia.Interactivity;
using Sudoku.Models;
using System.Linq;
using System;

namespace Sudoku.Views
{
    public partial class MainWindow : Window
    {
        private Models.Sudoku _sudoku;
        private int _selectedNumber = 0;

        public MainWindow()
        {
            InitializeComponent();
            _sudoku = new Models.Sudoku(new SudokuBoard());

            this.Loaded += (s, e) => LinkCells();
        }

        private void LinkCells()
        {
            var boxes = MainGrid.Children.OfType<SudokuBox>().ToList();

            for (int b = 0; b < 9; b++)
            {
                var cells = boxes[b].FindDescendantOfType<UniformGrid>()
                                   .Children.OfType<SudokuCell>().ToList();

                int startRow = (b / 3) * 3;
                int startCol = (b % 3) * 3;

                for (int i = 0; i < 9; i++)
                {
                    cells[i].Row = startRow + (i / 3);
                    cells[i].Column = startCol + (i % 3);

                    cells[i].PointerPressed += Cell_Click;
                }
            }
        }

        private void KeyboardButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                if (int.TryParse(btn.Content?.ToString(), out int val))
                {
                    _selectedNumber = val;
                }
                else if (btn.Content?.ToString() == "")
                {
                    _selectedNumber = 0;
                }
            }
        }

        private void Cell_Click(object? sender, Avalonia.Input.PointerPressedEventArgs e)
        {
            if (sender is SudokuCell cell)
            {
                _sudoku.UpdateCell(cell.Row, cell.Column, _selectedNumber);

                cell.CellValue.Text = _selectedNumber.ToString();
            }
        }
    }
}
