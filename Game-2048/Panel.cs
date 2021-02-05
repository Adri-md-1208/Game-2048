using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Game2048
{
    public class Panel : ICloneable
    {
        /// <summary>
        /// This class represents the board, which is binded with the grid
        /// through the GameManager
        /// The panel contents a matrix and some metadata like score or win status
        /// </summary>

        private short rows;
        private short columns;
        private int score;
        private bool win;
        private bool lose;
        private Stack<Panel> gameStates;


        // A List of Labels(Cells) representing the Grid
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

        // Score of the game
        public int Score
        {
            get => score;
            set
            {
                if (score > 0) score = value;
                else score = 0;
            }
        }

        public bool Win
        {
            get => win;
            set => win = value;
        }

        public bool Lose
        {
            get => lose;
            set => lose = value;
        }

        public Panel GetLastGameState()
        {
            try
            {
                return gameStates.Peek();
            }
            catch (InvalidOperationException)
            {
                return null;
            }

        }

        public void SetLastGameState(Panel state)
        {
            if (gameStates.Count == 2) gameStates.Pop();
            gameStates.Push(state);
        }

        public Panel()
        {
            score = 0;
            win = lose = false;
            gameStates = new Stack<Panel>(2);
        }

        public Panel(short BoardSize)
        {
            this.BoardSize = BoardSize;
            Cells = new Cell[BoardSize, BoardSize];
            score = 0;
            win = lose = false;
            gameStates = new Stack<Panel>(2);
        }

        public void MoveCellsUp()
        {
            for (int i = 0; i < BoardSize; i++)
            {
                Cell[] column = getColumn(Cells, i);

                // Push 0's to the bottom
                for (int x = 0; x < column.Length - 1; x++)
                {
                    if (column[x].GetValue() == 0)
                    {
                        column[x] = column[x + 1];
                        column[x + 1] = new Cell(0);
                    }
                }

                // Removing 0's from the middle
                for (int x = 0; x < column.Length - 1; x++)
                {
                    if (column[x].GetValue() == 0)
                    {
                        column[x] = column[x + 1];
                        column[x + 1] = new Cell(0);
                    }
                }

                // Looking for pairs
                for (int x = 0; x < column.Length - 1; x++)
                {
                    if (column[x].CompareTo(column[x + 1]) == 0)
                    {
                        column[x] = new Cell(column[x + 1].GetValue() * 2);
                        column[x + 1] = new Cell(0);
                        score += column[x].GetValue();
                    }
                }

                // Removing 0's from the middle again
                for (int x = 0; x < column.Length - 1; x++)
                {
                    if (column[x].GetValue() == 0)
                    {
                        column[x] = column[x + 1];
                        column[x + 1] = new Cell(0);
                    }
                }

                // Updating the data in the matrix
                for (int j = 0; j < column.Length; j++)
                {
                    Cells[j, i] = column[j];
                }
            }
        }

        public void MoveCellsDown()
        {
            for (int i = 0; i < BoardSize; i++)
            {
                Cell[] column = getColumn(Cells, i);

                // Push 0's to the left
                for (int x = column.Length - 1; x > 0; x--)
                {
                    if (column[x].GetValue() == 0)
                    {
                        column[x] = column[x - 1];
                        column[x - 1] = new Cell(0);
                    }
                }

                // Removing 0's from the middle
                for (int x = column.Length - 1; x > 0; x--)
                {
                    if (column[x].GetValue() == 0)
                    {
                        column[x] = column[x - 1];
                        column[x - 1] = new Cell(0);
                    }
                }

                // Looking for pairs
                for (int x = column.Length - 1; x > 0; x--)
                {
                    if (column[x].CompareTo(column[x - 1]) == 0)
                    {
                        column[x] = new Cell(column[x - 1].GetValue() * 2);
                        column[x - 1] = new Cell(0);
                        score += column[x].GetValue();
                    }
                }

                // Removing 0's from the middle again
                for (int x = column.Length - 1; x > 0; x--)
                {
                    if (column[x].GetValue() == 0)
                    {
                        column[x] = column[x - 1];
                        column[x - 1] = new Cell(0);
                    }
                }

                // Updating the data in the matrix
                for (int j = 0; j < column.Length; j++)
                {
                    Cells[j, i] = column[j];
                }
            }
        }

        public void MoveCellsLeft()
        {
            for (int i = 0; i < BoardSize; i++)
            {
                Cell[] row = getRow(Cells, i);

                // Push 0's to the right
                for (int x = 0; x < row.Length - 1; x++)
                {
                    if (row[x].GetValue() == 0)
                    {
                        row[x] = row[x + 1];
                        row[x + 1] = new Cell(0);
                    }
                }

                // Removing 0's from the middle
                for (int x = 0; x < row.Length - 1; x++)
                {
                    if (row[x].GetValue() == 0)
                    {
                        row[x] = row[x + 1];
                        row[x + 1] = new Cell(0);
                    }
                }

                // Looking for pairs
                for (int x = 0; x < row.Length - 1; x++)
                {
                    if (row[x].CompareTo(row[x + 1]) == 0)
                    {
                        row[x] = new Cell(row[x + 1].GetValue() * 2);
                        row[x + 1] = new Cell(0);
                        score += row[x].GetValue();
                    }
                }

                // Removing 0's from the middle again
                for (int x = 0; x < row.Length - 1; x++)
                {
                    if (row[x].GetValue() == 0)
                    {
                        row[x] = row[x + 1];
                        row[x + 1] = new Cell(0);
                    }
                }

                // Updating the data in the matrix
                for (int j = 0; j < row.Length; j++)
                {
                    Cells[i, j] = row[j];
                }
            }
        }

        public void MoveCellsRight()
        {
            for (int i = 0; i < BoardSize; i++)
            {
                Cell[] row = getRow(Cells, i);

                // Push 0's to the left
                for (int x = row.Length - 1; x > 0; x--)
                {
                    if (row[x].GetValue() == 0)
                    {
                        row[x] = row[x - 1];
                        row[x - 1] = new Cell(0);
                    }
                }

                // Removing 0's from the middle
                for (int x = row.Length - 1; x > 0; x--)
                {
                    if (row[x].GetValue() == 0)
                    {
                        row[x] = row[x - 1];
                        row[x - 1] = new Cell(0);
                    }
                }

                // Looking for pairs
                for (int x = row.Length - 1; x > 0; x--)
                {
                    if (row[x].CompareTo(row[x - 1]) == 0)
                    {
                        row[x] = new Cell(row[x - 1].GetValue() * 2);
                        row[x - 1] = new Cell(0);
                        score += row[x].GetValue();
                    }
                }

                // Removing 0's from the middle again
                for (int x = row.Length - 1; x > 0; x--)
                {
                    if (row[x].GetValue() == 0)
                    {
                        row[x] = row[x - 1];
                        row[x - 1] = new Cell(0);
                    }
                }

                // Updating the data in the matrix
                for (int j = 0; j < row.Length; j++)
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

        public void updateWinProperty()
        {
            // Traverse the matrix and checks if it have a 2048
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    if (Cells[i, j].GetValue() == 2048) win = true;
                }
            }
        }

        public void updateLoseProperty()
        {
            bool noZeros = true;
            // Traverse the matrix and checks if it have 0's
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    if (Cells[i, j].GetValue() == 0) noZeros = false;
                }
            }
            lose = noZeros;
        }

        public object Clone()
        {
            // Deep copy
            Panel copy = new Panel(BoardSize);
            copy.Cells = (Cell[,])this.Cells.Clone();
            copy.score = Int32.Parse(new String(this.score.ToString()));
            copy.win = this.Win;
            copy.lose = this.Lose;
            copy.gameStates = this.gameStates;
            return copy;
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