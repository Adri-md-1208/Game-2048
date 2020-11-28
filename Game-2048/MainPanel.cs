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
    public class MainPanel
    {
        /// <summary>
        /// Implementation class for the main panel
        /// </summary>

        private short rows;
        private short columns;
        private Button[,] Btns;

        // BoardSize property sets the number of the rows and columns
        public short BoardSize
        {
            // For now, the only BoardSize allowed is 4 by 4
            get => rows;
            set
            {
                if (value > 4) rows = columns = value;
                else throw new InvalidBoardSizeException("Size must be positive and greater than 3");
            }
        }

            public MainPanel() { }
            public MainPanel(short BoardSize) { this.BoardSize = BoardSize; }

            private void InitializeGame(MainPanel mainPanel)
            {
                Btns = new Button[BoardSize, BoardSize];
                for (int i=0; i < this.BoardSize; i++)
                    for (int j=0; j < this.BoardSize; j++)
                    {
                        Btns[i, j] = new Button();
                        Grid.SetColumn(Btns[i, j], j);
                        Grid.SetRow(Btns[i, j], i + 1);
                        Main.Children.Add(Btns[i, j]); //TODO: Refactorize the class and link with the MainWindow
                    }
            }
        }

        [Serializable()]
        public class InvalidBoardSizeException : System.Exception
        {
            /// <summary>
            /// Exception for handling the size of the board
            /// </summary>

            public InvalidBoardSizeException() : base() { }
            public InvalidBoardSizeException(string message) : base(message) { }
            public InvalidBoardSizeException(string message, System.Exception inner) : base(message, inner) { }
            protected InvalidBoardSizeException(System.Runtime.Serialization.SerializationInfo info,
                System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        }
    }