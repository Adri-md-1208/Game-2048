using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
    }
}
