using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ZhaoXiaodan.Lab2
{
    public class GridGraph<T> : IGridGraph<T>
    {
        private readonly T[,] internalGrid;

        public int NumberOfRows { get; private set; }
        public int NumberOfColumns { get; private set; }


        public GridGraph(int numberOfRows, int numberOfColumns)
        {
            NumberOfRows = numberOfRows;
            NumberOfColumns = numberOfColumns;

            internalGrid = new T[NumberOfRows, numberOfColumns];
        }

        public void SetCellValue(int row, int column, T dataValue)
        {
            internalGrid[row, column] = dataValue;
        }

        public T GetCellValue(int row, int column)
        {
            return internalGrid[row, column];
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (T cellData in internalGrid)
            {
                yield return cellData;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}
