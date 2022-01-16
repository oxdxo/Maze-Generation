using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ZhaoXiaodan.Lab2.CreateAbstractMaze;

namespace ZhaoXiaodan.Lab2
{
    public class MazeToOccupancyGtaphAdaptor
    {
        GridGraph<Direction> mazeGraph;

        public MazeToOccupancyGtaphAdaptor(GridGraph<Direction> mazeGraph)
        {
            this.mazeGraph = mazeGraph;
        }

        // Start is called before the first frame update
        public GridGraph<bool> build()
        {
            GridGraph<bool> occupancyGraph = new GridGraph<bool>(mazeGraph.NumberOfRows * 3, mazeGraph.NumberOfColumns * 3);
            for (int i = 0; i < occupancyGraph.NumberOfRows; i++)
            {
                for (int j = 0; j < occupancyGraph.NumberOfColumns; j++)
                {
                    occupancyGraph.SetCellValue(i, j, false); // set all as wall
                }
            }

            for (int i = 0; i < mazeGraph.NumberOfRows; i++)
            {
                for (int j = 0; j < mazeGraph.NumberOfColumns; j++)
                {
                    Debug.Log("i " + i);
                    Debug.Log("j " + j);
                    Direction cell = mazeGraph.GetCellValue(i, j);
                    Debug.Log("Direction " + cell);
                    int x = i * 3;
                    int y = j * 3;
                    // initial all as wall[]
                    // [][][]
                    // [][][]
                    // [][][]

                    occupancyGraph.SetCellValue(x + 1, y + 1, true);
                    // set the middle as floor{}
                    // [][][]
                    // []{}[]
                    // [][][]

                    Debug.Log("Set " + (x + 1) + (y + 1) + " As True");

                    if (!cell.HasFlag(Direction.RIGHT))
                    {
                        occupancyGraph.SetCellValue(x + 2, y + 1, true);
                        // set the right as floor{}
                        // [][][]
                        // []{}{}
                        // [][][]
                        Debug.Log("Right Set " + (x + 2) + " " + (y + 1) + " As True");
                    }

                    if (!cell.HasFlag(Direction.LEFT))
                    {
                        occupancyGraph.SetCellValue(x, y + 1, true);
                        // set the left as floor{}
                        // [][][]
                        // {}{}[]
                        // [][][]
                        Debug.Log("Left Set " + (x) + " " + (y + 1) + " As True");
                    }

                    if (!cell.HasFlag(Direction.TOP))
                    {
                        occupancyGraph.SetCellValue(x + 1, y + 2, true);
                        // set the top as floor{}
                        // []{}[]
                        // []{}[]
                        // [][][]
                        Debug.Log("Top Set " + (x + 1) + " " + (y + 2) + " As True");
                    }

                    if (!cell.HasFlag(Direction.BOTTOM))
                    {
                        occupancyGraph.SetCellValue(x + 1, y, true);
                        // set the bottom as floor{}
                        // [][][]
                        // []{}[]
                        // []{}[]
                        Debug.Log("Bottom Set " + (x + 1) + " " + (y) + " As True");
                    }
                }
            }

            return occupancyGraph;
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
