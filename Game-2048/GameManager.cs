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

        public static void InitializeGame(Panel panel, Grid grid, Label win, Label lose)
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
            HideLabel(win);
            HideLabel(lose);
        }

        public static void UpdateBoard(Panel panel, Grid grid)
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

        public static void PutCellInPanel(Cell cell, Panel panel, int x, int y)
        {
            panel.Cells[x, y] = cell;
        }

        public static void SpawnCell(Panel panel, Grid grid)
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
            UpdateBoard(panel, grid);
        }

        public static void UpdateScoreLabel(Panel panel, Label label)
        {
            label.Content = panel.Score;

        }

        public static bool CheckForWin(Panel panel) => panel.Win;

        public static void UpdateWinLabel(Panel panel, Label label)
        {
            // If the user wins, shows a label
            if (CheckForWin(panel))
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
                label.SetValue(System.Windows.Controls.Panel.ZIndexProperty, 1);
            }
        }

        public static bool CheckForLose(Panel panel) => panel.Lose;

        public static void UpdateLoseLabel(Panel panel, Label label)
        {
            // If the user lose, shows a label
            if (CheckForLose(panel))
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
                label.SetValue(System.Windows.Controls.Panel.ZIndexProperty, 1);
            }
        }

        public static bool CheckIfPlayerWonOrLost(Panel panel)
        {
            bool playerWin = (CheckForWin(panel));
            bool playerLose = (CheckForLose(panel));
            return playerWin || playerLose;
        }

        private static void HideLabel(Label label)
        {
            // Resets the win label content and hide it
            label.SetValue(System.Windows.Controls.Panel.ZIndexProperty, 0);
            label.Background = null;
            label.Content = null;
        }

        public static void SetLastGameState(ref Panel panel)
        {
            // Enqueue the last state
            Panel last = (Panel)panel.Clone();
            panel.SetLastGameState(last);
        }

        public static void GetLastGameState(ref Panel panel)
        {
            // Load the previous state
            panel = panel.GetLastGameState();
        }

        public static void MoveCells(Panel panel, String direction)
        {
            if (direction == "Up") panel.MoveCellsUp();
            if (direction == "Down") panel.MoveCellsDown();
            if (direction == "Left") panel.MoveCellsLeft();
            if (direction == "Right") panel.MoveCellsRight();
        }

        public static void UpdateWinAndLoseProperties(Panel panel)
        {
            panel.updateWinProperty();
            panel.updateLoseProperty();
        }

    }
}