using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Game2048
{
    static class GameManager
    {
        public static void InitializeGame(MainPanel panel, Grid grid)
        {
            for (int i = 0; i < panel.BoardSize; i++)
                for (int j = 0; j < panel.BoardSize; j++)
                {
                    Cell cell = new Cell();
                    PutCellInPanel(cell, panel, i, j);
                }
        }

        public static void UpdateGame(MainPanel panel, Grid grid)
        {
            // This loop updates the values of all cells
            for (int i = 0; i < panel.BoardSize; i++)
                for (int j = 0; j < panel.BoardSize; j++)
                {
                    grid.Children
                        .Cast<UIElement>()
                        .First(cell => Grid.GetRow(cell) == i && Grid.GetColumn(cell) == j)
                        .SetValue(ContentControl.ContentProperty, panel.Cells[i, j].GetValue());
                    grid.Children
                        .Cast<UIElement>()
                        .First(cell => Grid.GetRow(cell) == i && Grid.GetColumn(cell) == j)
                        .SetValue(Control.BackgroundProperty, panel.Cells[i, j].GetColor());
                }
            // This loop checks for 0's cells and dont show it
            for (int i = 0; i < panel.BoardSize; i++)
                for (int j = 0; j < panel.BoardSize; j++)
                {
                    if ((Int32)grid.Children
                        .Cast<UIElement>()
                        .First(cell => Grid.GetRow(cell) == i && Grid.GetColumn(cell) == j)
                        .GetValue(ContentControl.ContentProperty)
                        == 0)
                        grid.Children
                            .Cast<UIElement>()
                            .First(cell => Grid.GetRow(cell) == i && Grid.GetColumn(cell) == j)
                            .SetValue(ContentControl.ContentProperty, null);
                }
        }
        
        public static void PutCellInPanel(Cell cell, MainPanel panel, int x, int y)
        {
            panel.Cells[x, y] = cell;
        }

        public static void SpawnCell(MainPanel panel, Grid grid)
        {
            var rng = new Random();
            int x, y;
            do
            {
                x = rng.Next(panel.BoardSize);
                y = rng.Next(panel.BoardSize);
            } while (panel.Cells[x, y].GetValue() != 0);

            Cell cell = new Cell(2);
            int TwoOrFour = rng.Next(4);

            if (TwoOrFour == 3) // 25% probability of spawning a 4
            {
                cell = new Cell(4);
            }
            

            PutCellInPanel(cell, panel, x, y);
            UpdateGame(panel, grid);
        } 
    }
}
