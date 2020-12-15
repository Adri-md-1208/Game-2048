using System;
using System.Collections.Generic;
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
                for (int x = 0; x < row.Length; x++) // Pushing the elements of the row
                {
                    if ((x != row.Length) && (row[x].GetValue() == 0)) // Pushing the 0's to the right
                    {
                        List<Cell> firstPart = row.Take(row.Length - x).ToList();
                        List<Cell> secondPart = row.Reverse().Take(row.Length - x - 1).ToList();
                        row = firstPart.Append(row[x]).Concat(secondPart).ToArray();
                    }
                    if ((x != row.Length - 1) && (row[x].GetValue() == row[x + 1].GetValue())) // Adding equals
                    {
                        row[x] = new Cell(2 * row[x].GetValue());
                        row[x + 1] = new Cell(0);
                    }
                }

                for (int j = 0; j < row.Length; j++) // Accesing elements
                {
                    Cells[i, j] = row[j];
                }
            }
        }

        public void PushCellsRight()
        {
            for (int i = 0; i < BoardSize; i++)
            {
                Cell[] row = getRow(Cells, i);
                Array.Sort<Cell>(row);
                for (int j = 0; j < BoardSize; j++)
                {
                    Cells[i, j] = row[j];
                }
            }

        }

        public void PushCellsUp()
        {
            for (int i = 0; i < BoardSize; i++)
            {
                Cell[] row = getColumn(Cells, i);
                Array.Sort(row);
                for (int j = 0; j < BoardSize; j++)
                {
                    Cells[i, j] = row[j];
                }
            }

        }

        public void PushCellsDown()
        {
            for (int i = 0; i < BoardSize; i++)
            {
                Cell[] row = getRow(Cells, i);
                Array.Sort(row);
                for (int j = 0; j < BoardSize; j++)
                {
                    Cells[i, j] = row[j];
                }
            }

        }

        private Cell[] getRow(Cell[,] cells, int row) 
        {
            return Enumerable.Range(0, cells.GetLength(1))
                .Select(x => cells[row, x])
                .ToArray();
        }

        private Cell[] getColumn(Cell[,] cells, int column)
        {
            return Enumerable.Range(0, cells.GetLength(0))
                .Select(x => cells[x, column])
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