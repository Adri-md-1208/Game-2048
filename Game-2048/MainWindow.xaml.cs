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
        Button[,] Btns = new Button[4, 4];

        public MainWindow()
        {
            InitializeComponent();

            for (int i=0; i<4; i++)
                for (int j=0; j<4; j++)
                {
                    Btns[i, j] = new Button();
                    Btns[i, j].BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black")); // Black border
                    Grid.SetColumn(Btns[i, j], j);
                    Grid.SetRow(Btns[i, j], i+1);
                    Main.Children.Add(Btns[i, j]);
                }
        }
    }
}
