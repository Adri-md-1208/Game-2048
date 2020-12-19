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
            GameManager.InitializeGame(panel, cellsGrid, winLabel);
            GameManager.UpdateGame(panel, cellsGrid);
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            // When the user clicks the start button, the game will be restarted
            GameManager.InitializeGame(panel, cellsGrid, winLabel);
            GameManager.UpdateScore(panel, Score);
            GameManager.SpawnCell(panel, cellsGrid);
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            // Event that manage the user input (arrow keys)
            // In every movement, the loop will update the score label, 
            // check if user wins and spawn a new cell
            // Then the game state is updated

            if (e.Key == Key.Up)
            {
                panel.PushCellsUp();
                GameManager.UpdateScore(panel, Score);
                GameManager.CheckForWin(panel, winLabel);
                GameManager.SpawnCell(panel, cellsGrid);
                GameManager.UpdateGame(panel, cellsGrid);
            }

            if (e.Key == Key.Down)
            {
                panel.PushCellsDown();
                GameManager.UpdateScore(panel, Score);
                GameManager.CheckForWin(panel, winLabel);
                GameManager.SpawnCell(panel, cellsGrid);
                GameManager.UpdateGame(panel, cellsGrid);
            }

            if (e.Key == Key.Left)
            {
                panel.PushCellsLeft();
                GameManager.UpdateScore(panel, Score);
                GameManager.CheckForWin(panel, winLabel);
                GameManager.SpawnCell(panel, cellsGrid);
                GameManager.UpdateGame(panel, cellsGrid);
            }

            if (e.Key == Key.Right)
            {
                panel.PushCellsRight();
                GameManager.UpdateScore(panel, Score);
                GameManager.CheckForWin(panel, winLabel);
                GameManager.SpawnCell(panel, cellsGrid);
                GameManager.UpdateGame(panel, cellsGrid);
            }
        }
    }
}
