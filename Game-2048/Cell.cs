﻿using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace Game2048
{
    /// <summary>
    /// Cells are the basic unit of the MainPanel
    /// Are conected with the labels through to the GameManager class
    /// </summary>

    public class Cell : IComparable<Cell>
    {
        private int value;
        private SolidColorBrush color;

        public Cell()
        {
            value = 0;
            color = Brushes.AntiqueWhite;
        }

        public Cell(int value)
        {
            this.value = value;
            switch (value)
            {
                case 2:
                    color = Brushes.PowderBlue;
                    break;
                case 4:
                    color = Brushes.SkyBlue;
                    break;
                case 8:
                    color = Brushes.SteelBlue;
                    break;
                case 16:
                    color = Brushes.DarkSeaGreen;
                    break;
                case 32:
                    color = Brushes.MediumSeaGreen;
                    break;
                case 64:
                    color = Brushes.ForestGreen;
                    break;
                case 128:
                    color = Brushes.Firebrick;
                    break;
                case 256:
                    color = Brushes.DarkRed;
                    break;
                case 512:
                    color = Brushes.Peru;
                    break;
                case 1024:
                    color = Brushes.SaddleBrown;
                    break;
                case 2048:
                    color = Brushes.Black;
                    break;
                default:
                    this.value = 0;
                    color = Brushes.AntiqueWhite;
                    break;
            }
        }

        public int GetValue() => value;
        public SolidColorBrush GetColor() => color;

        // Allow cells to be compared with other cells
        public int CompareTo(Cell other)
        {
            if (this.GetValue() > other.GetValue()) return 1;
            if (this.GetValue() < other.GetValue()) return -1;
            else return 0;
        }
    }
}
