﻿using System;
using System.Windows;
using System.Windows.Input;

namespace Game2048
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainPanel panel = new MainPanel(4); // 4x4 game

        public MainWindow()
        {
            InitializeComponent();
            GameManager.InitializeGame(panel, cellsGrid);
            GameManager.UpdateGame(panel, cellsGrid);
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            GameManager.InitializeGame(panel, cellsGrid);
            GameManager.SpawnCell(panel, cellsGrid);
        }

        // Event that manage the user input (arrow keys)
        private void grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                panel.PushCellsUp();
                GameManager.SpawnCell(panel, cellsGrid);
                GameManager.UpdateGame(panel, cellsGrid);
            }

            if (e.Key == Key.Down)
            {
                panel.PushCellsDown();
                GameManager.SpawnCell(panel, cellsGrid);
                GameManager.UpdateGame(panel, cellsGrid);
            }

            if (e.Key == Key.Left)
            {
                panel.PushCellsLeft();
                GameManager.SpawnCell(panel, cellsGrid);
                GameManager.UpdateGame(panel, cellsGrid);
            }

            if (e.Key == Key.Right)
            {
                panel.PushCellsRight();
                GameManager.SpawnCell(panel, cellsGrid);
                GameManager.UpdateGame(panel, cellsGrid);
            }
        }
    }
}
