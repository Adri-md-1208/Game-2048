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
        public MainWindow()
        {
            InitializeComponent();
            MainPanel panel = new MainPanel() { BoardSize = 4 };
            InitializeGame(panel);

        }

        public void InitializeGame(MainPanel panel)
        {
            panel.Cells = new Label[panel.BoardSize, panel.BoardSize];
            for (int i = 0; i < panel.BoardSize; i++)
                for (int j = 0; j < panel.BoardSize; j++)
                {
                    panel.Cells[i, j] = new Label();
                    Grid.SetColumn(panel.Cells[i, j], j);
                    Grid.SetRow(panel.Cells[i, j], i + 1);
                    Main.Children.Add(panel.Cells[i, j]);
                    panel.Cells[i, j].Content = (int)i * 10 + j;
                    panel.Cells[i, j].HorizontalContentAlignment = HorizontalAlignment.Center;
                    panel.Cells[i, j].VerticalContentAlignment = VerticalAlignment.Center;
                    panel.Cells[i, j].FontSize = 25;
                    panel.Cells[i, j].Background = System.Drawing.Color.IndianRed;

                }
        }
    }
}
