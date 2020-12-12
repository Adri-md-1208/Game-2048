using System;
using System.Linq;
using System.Windows;

namespace Game2048
{
    public class MainPanel
    {
        /// <summary>
        /// Implementation class for the main panel
        /// </summary>
        
        private short rows;
        private short columns;

        // A List of Labels representing the Grid
        public Cell[,] Cells { get; set; }

        // BoardSize property sets the number of the rows and columns
        public short BoardSize
        {
            get => rows;
            set
            {
                if (value > 3) rows = columns = value;
                else throw new InvalidBoardSizeException("Size must be positive and greater than 3");
            }
        }

        public MainPanel() { }
        public MainPanel(short BoardSize) 
        { 
            this.BoardSize = BoardSize;
            Cells = new Cell[BoardSize, BoardSize];
        }

        public void PushCellsLeft()
        {
            for (int i = 0; i < BoardSize; i++) // Accesing rows
            {
                Cell[] row = getRow(Cells, i);
                row.OrderBy<Cell, int>()
            }
        }

        private Cell[] getRow(Cell[,] cells, int row) 
        {
            return Enumerable.Range(0, cells.GetLength(1)) // Rows = 1st dim [*,]
                .Select(x => cells[x, row])
                .ToArray();
        }

        private Cell[] getColumn(Cell[,] cells, int column)
        {
            return Enumerable.Range(0, cells.GetLength(0)) // Columns = 0 dim [,*]
                .Select(x => cells[column, x])
                .ToArray();
        }
    }

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