using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
                    panel.Cells[i, j] = GenerateCell(2);
                    Grid.SetColumn(panel.Cells[i, j], j);
                    Grid.SetRow(panel.Cells[i, j], i + 1);
                    grid.Children.Add(panel.Cells[i, j]);
                    panel.Cells[i, j].Content = (int)i * 10 + j;
                    panel.Cells[i, j].HorizontalContentAlignment = HorizontalAlignment.Center;
                    panel.Cells[i, j].VerticalContentAlignment = VerticalAlignment.Center;
                    panel.Cells[i, j].FontSize = 25;
                }
        }

        public static Label GenerateCell(int value)
        {
            switch (value)
            {
                case 2:
                    return new Label() {
                        Background = Brushes.AntiqueWhite,
                        Padding = new Thickness(10)
                    };
                    break;
                case 4:
                    return new Label() { BorderBrush = Brushes.Red };
                    break;
                case 8:
                    return new Label() { BorderBrush = Brushes.Red };
                    break;
                case 16:
                    return new Label() { BorderBrush = Brushes.Red };
                    break;
                case 32:
                    return new Label();
                    break;
                case 64:
                    return new Label();
                    break;
                case 128:
                    return new Label();
                    break;
                case 256:
                    return new Label();
                    break;
                case 512:
                    return new Label();
                    break;
                case 1024:
                    return new Label();
                    break;
                case 2048:
                    return new Label();
                    break;
                default:
                    // Create exception
                    return new Label();
                    break;
            }

        }
    }
}
