using System;
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

        private MainPanel panel = new MainPanel(4);

        public MainWindow()
        {
            // Starts a new game when opens
            InitializeComponent();
            GameManager.InitializeGame(panel, cellsGrid, winLabel, loseLabel);
            GameManager.UpdateGame(panel, cellsGrid);
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            // When the user clicks the start button, the game will be restarted
            GameManager.InitializeGame(panel, cellsGrid, winLabel, loseLabel);
            GameManager.UpdateScore(panel, scoreLabel);
            GameManager.UpdateLastState(ref panel); // Initial state
            GameManager.SpawnCell(panel, cellsGrid);
            GameManager.UpdateLastState(ref panel); // Fill the queue
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            // Event that manage the user input (arrow keys)
            // In every movement, the loop will update the score label, 
            // check if user wins and spawn a new cell
            // Then the game state is updated

            if (e.Key == Key.Up)
            {
                GameManager.UpdateGame(panel, cellsGrid);
                GameManager.UpdateLastState(ref panel);
                GameManager.MoveCells(panel, "Up");
                GameManager.UpdateScore(panel, scoreLabel);
                if (!((GameManager.CheckForWin(panel, winLabel)) | (GameManager.CheckForLose(panel, loseLabel))))
                    // Not spawns if the player either win or lose
                    GameManager.SpawnCell(panel, cellsGrid);
            }

            if (e.Key == Key.Down)
            {
                GameManager.UpdateGame(panel, cellsGrid);
                GameManager.UpdateLastState(ref panel);
                GameManager.MoveCells(panel, "Down");
                GameManager.UpdateScore(panel, scoreLabel);
                if (!((GameManager.CheckForWin(panel, winLabel)) | (GameManager.CheckForLose(panel, loseLabel))))
                    // Not spawns if the player either win or lose
                    GameManager.SpawnCell(panel, cellsGrid);
            }

            if (e.Key == Key.Left)
            {
                GameManager.UpdateGame(panel, cellsGrid);
                GameManager.UpdateLastState(ref panel);
                GameManager.MoveCells(panel, "Left");
                GameManager.UpdateScore(panel, scoreLabel);
                if (!((GameManager.CheckForWin(panel, winLabel)) | (GameManager.CheckForLose(panel, loseLabel))))
                    // Not spawns if the player either win or lose
                    GameManager.SpawnCell(panel, cellsGrid);
            }

            if (e.Key == Key.Right)
            {
                GameManager.UpdateGame(panel, cellsGrid);
                GameManager.UpdateLastState(ref panel);
                GameManager.MoveCells(panel, "Right");
                GameManager.UpdateScore(panel, scoreLabel);
                if (!((GameManager.CheckForWin(panel, winLabel)) | (GameManager.CheckForLose(panel, loseLabel))))
                    GameManager.SpawnCell(panel, cellsGrid);
            }
        }

        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            GameManager.LoadLastState(ref panel);
            // If the last state is the initial, reset the game
            if (panel == null)
            {
                panel = new MainPanel(4);
                GameManager.InitializeGame(panel, cellsGrid, winLabel, loseLabel);
            }
            GameManager.UpdateGame(panel, cellsGrid);
            GameManager.UpdateScore(panel, scoreLabel);
        }
    }
}
