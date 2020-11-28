using System;
using System.Collections.Generic;
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
        public MainWindow()
        {
            InitializeComponent();
            MainPanel panel = new MainPanel() { BoardSize = 4 };
            InitializeGame(panel);

        }

        public void InitializeGame(MainPanel panel)
        {
            panel.Btns = new Button[panel.BoardSize, panel.BoardSize];
            for (int i = 0; i < panel.BoardSize; i++)
                for (int j = 0; j < panel.BoardSize; j++)
                {
                    panel.Btns[i, j] = new Button();
                    Grid.SetColumn(panel.Btns[i, j], j);
                    Grid.SetRow(panel.Btns[i, j], i + 1);
                    Main.Children.Add(panel.Btns[i, j]);
                    panel.Btns[i, j].Content = (int)i * 10 + j;
                }
        }
    }
}
