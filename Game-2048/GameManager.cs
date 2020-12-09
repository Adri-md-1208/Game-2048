using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Game2048
{
    static class GameManager
    {
        public static void InitializeGame(MainPanel panel, Grid grid)
        {
            panel.Cells = new Label[panel.BoardSize, panel.BoardSize];
            for (int i = 0; i < panel.BoardSize; i++)
                for (int j = 0; j < panel.BoardSize; j++)
                {
                    panel.Cells[i, j] = new Label();
                    Grid.SetColumn(panel.Cells[i, j], j);
                    Grid.SetRow(panel.Cells[i, j], i + 1);
                    grid.Children.Add(panel.Cells[i, j]);
                    panel.Cells[i, j].Content = (int)i * 10 + j;
                    panel.Cells[i, j].HorizontalContentAlignment = HorizontalAlignment.Center;
                    panel.Cells[i, j].VerticalContentAlignment = VerticalAlignment.Center;
                    panel.Cells[i, j].FontSize = 25;
                }
        }
    }
}
