using Avalonia;
using Avalonia.Controls;

namespace Sudoku.Views;

public partial class SudokuCellHint : UserControl
{
    public static readonly StyledProperty<string> HintValueProperty =
        AvaloniaProperty.Register<SudokuCellHint, string>(nameof(HintValue), defaultValue: "");

    public string HintValue
    {
        get => GetValue(HintValueProperty);
        set => SetValue(HintValueProperty, value);
    }

    public SudokuCellHint()
    {
        InitializeComponent();
    }
}