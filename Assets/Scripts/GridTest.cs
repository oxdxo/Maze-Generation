using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static ZhaoXiaodan.Lab2.CreateAbstractMaze;

namespace ZhaoXiaodan.Lab2
{
    public class GridTest : MonoBehaviour
    {
        // Start is called before the first frame update

        GridGraph<bool> b;
        GridGraph<Direction> d;
        
        void Start()
        {
            b = new GridGraph<bool>(2, 3);
            d = new GridGraph<Direction>(2, 3);
            b.SetCellValue(0, 0, false);
            b.SetCellValue(0, 1, true);
            b.SetCellValue(1, 0, false);
            b.SetCellValue(1, 2, true);
            d.SetCellValue(0, 0, Direction.RIGHT);
            d.SetCellValue(0, 2, Direction.BOTTOM);
            d.SetCellValue(1, 0, Direction.LEFT);
            d.SetCellValue(1, 1, Direction.TOP);
        }

        // Update is called once per frame
        void Update()
        {
            int i = 0;
            foreach (var data in b)
            {
                Debug.Log("GridTest Bool " + i);
                Debug.Log("GridTest " + data);
                i++;
            }
            i = 0;
            foreach (var data in d)
            {
                Debug.Log("GridTest Direction " + i);
                Debug.Log("GridTest " + data);
                i++;
            }
        }
    }
}
