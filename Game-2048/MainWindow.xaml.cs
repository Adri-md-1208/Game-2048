using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Game2048
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        /// <summary>
        /// This class is the messager between the xaml structure and the c# logic
        /// It uses the GameManager class for encapsulation
        /// The xaml structure is done for a 4x4 game, more options would come in future relases
        /// </summary>

        private Panel panel = new Panel(4);

        public MainWindow()
        {
            // Starts a new game when opens
            InitializeComponent();
            GameManager.InitializeGame(panel, cellsGrid, winLabel, loseLabel);
            GameManager.UpdateBoard(panel, cellsGrid);
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            // When the user clicks the start button, the game will be restarted
            GameManager.InitializeGame(panel, cellsGrid, winLabel, loseLabel);
            GameManager.UpdateScoreLabel(panel, scoreLabel);
            GameManager.SetLastGameState(ref panel); // Push the initial state into the gameStates stack
            GameManager.SpawnCell(panel, cellsGrid);
            GameManager.SetLastGameState(ref panel);
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            // Event that manage the user input (arrow keys)
            // In every movement, the loop will update the score label, 
            // stack the previous game state, checks if user wins or lose and spawns a new cell
            // Then the game board is updated

            if (!GameManager.CheckIfPlayerWonOrLost(panel))
            // Only can move if either has not won or has not lost
            {
                List<Key> arrowKeys =  new List<Key>() { Key.Up, Key.Down, Key.Left, Key.Right };
                if (arrowKeys.Contains(e.Key))
                {
                    GameManager.SetLastGameState(ref panel);
                    switch (e.Key)
                    {
                        case (Key.Up): 
                            GameManager.MoveCells(panel, "Up");
                            break;
                        case (Key.Down):
                            GameManager.MoveCells(panel, "Down");
                            break;
                        case (Key.Left):
                            GameManager.MoveCells(panel, "Left");
                            break;
                        case (Key.Right):
                            GameManager.MoveCells(panel, "Right");
                            break;
                    }
                    GameManager.UpdateWinAndLoseProperties(panel);
                    GameManager.UpdateScoreLabel(panel, scoreLabel);
                    if (!GameManager.CheckIfPlayerWonOrLost(panel))
                        GameManager.SpawnCell(panel, cellsGrid);
                    if (GameManager.CheckForWin(panel))
                        GameManager.UpdateWinLabel(panel, winLabel);
                    if (GameManager.CheckForLose(panel))
                        GameManager.UpdateLoseLabel(panel, winLabel);
                    GameManager.UpdateBoard(panel, cellsGrid);
                }
                
            }
        }

        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            if (!GameManager.CheckIfPlayerWonOrLost(panel))
            {
                GameManager.GetLastGameState(ref panel);
                // If the game has not started, reset the game
                if (panel == null)
                {
                    panel = new Panel(4);
                    GameManager.InitializeGame(panel, cellsGrid, winLabel, loseLabel);
                }
                GameManager.UpdateBoard(panel, cellsGrid);
                GameManager.UpdateScoreLabel(panel, scoreLabel);
            }
        }
    }
}