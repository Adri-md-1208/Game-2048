using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Game2048
{
    static class GameManager
    {
        /// <summary>
        /// The GameManager class interacts between the wpf controls and the element classes
        /// </summary>
        /// <param name="panel"> The panel that represents the game board. It will be connected 
        ///                      with the cells grid control </param>
        /// <param name="grid"> The cells grid connected with the panel object </param>
        /// <param name="label"> The score label or the win label depending the method who use it </param>
       
        public static void InitializeGame(MainPanel panel, Grid grid, Label win, Label lose)
        {
            // Fill the grid with empty labels (cells with value 0)
            for (int i = 0; i < panel.BoardSize; i++)
                for (int j = 0; j < panel.BoardSize; j++)
                {
                    Cell cell = new Cell();
                    PutCellInPanel(cell, panel, i, j);
                }
            // Reset the score and the win values
            panel.Score = 0;
            panel.Win = false;
            panel.Lose = false;
            ResetLabel(win);
            ResetLabel(lose);
        }

        public static void UpdateGame(MainPanel panel, Grid grid)
        {
            // This loop updates the content of every cell in the grid with
            // his corresponding value in the cells matrix
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
            // Spawns a 2 or 4 cell in random position
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

        public static void UpdateScore(MainPanel panel, Label label)
        {
            label.Content = panel.Score;

        }

        public static bool CheckForWin(MainPanel panel, Label label)
        {
            // If the user wins, shows a label
            if (panel.Win)
            {
                // Properties
                label.Background = Brushes.DarkSeaGreen;
                label.Content = "WINNER WINNER";
                label.FontFamily = new FontFamily("Consolas");
                label.FontSize = 50;
                label.HorizontalContentAlignment = HorizontalAlignment.Center;
                label.VerticalContentAlignment = VerticalAlignment.Center;
                // Dependency properties
                label.SetValue(Label.OpacityProperty, 0.8);
                label.SetValue(Panel.ZIndexProperty, 1);
            }

            return panel.Win;
        }

        public static bool CheckForLose(MainPanel panel, Label label)
        {
            // If the user lose, shows a label
            if (panel.Lose)
            {
                // Properties
                label.Background = Brushes.IndianRed;
                label.Content = "YOU LOSE";
                label.FontFamily = new FontFamily("Consolas");
                label.FontSize = 50;
                label.HorizontalContentAlignment = HorizontalAlignment.Center;
                label.VerticalContentAlignment = VerticalAlignment.Center;
                // Dependency properties
                label.SetValue(Label.OpacityProperty, 0.8);
                label.SetValue(Panel.ZIndexProperty, 1);
            }

            return panel.Lose;
        }

        private static void ResetLabel(Label label)
        {
            // Resets the win label content and hide it
            label.SetValue(Panel.ZIndexProperty, 0);
            label.Background = null;
            label.Content = null;
        }

        public static void UpdateLastState(MainPanel panel)
        {
            // Enqueue the previous status
            panel.State = new GameState(panel.Score, panel.Cells);
        }

        public static void LoadLastState(MainPanel panel)
        {
            // Load the previous state
            GameState previous = panel.State;
            panel.Score = previous.score;
            panel.Cells = previous.cells;
        }

        public static void MoveCells(MainPanel panel, String direction)
        {
            if (direction == "Up") panel.PushCellsUp();
            if (direction == "Down") panel.PushCellsDown();
            if (direction == "Left") panel.PushCellsLeft();
            if (direction == "Right") panel.PushCellsRight();
        }
    }
}
