using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZhaoXiaodan.Lab2
{
    public class GridGameObjectFactory : MonoBehaviour
    {
        [SerializeField] [Range(1,5)]private int hightOfWall;
        [SerializeField] private Transform occupiedPrefab; //The path
        [SerializeField] private Transform unoccupiedPrefab; //The wall
        [SerializeField] private Transform parentForNewObjects;

        public void createPrefab(GridGraph<bool> maze)
        {
            for (int i = 0; i < maze.NumberOfRows; i++)
            {
                for(int j = 0; j< maze.NumberOfColumns; j++)
                {
                    bool state = maze.GetCellValue(i, j);
                    
                    if (state)
                    {
                        var occupiedPosition = new Vector3((-maze.NumberOfRows / 2 + i), 0, (-maze.NumberOfColumns / 2 + j));
                        var occupied = Instantiate(occupiedPrefab, parentForNewObjects) as Transform;
                        occupied.position = occupiedPosition;
                    }
                    else
                    {
                        for(int n = 0; n < hightOfWall; n++)
                        {
                            var unoccupiedPosition = new Vector3((-maze.NumberOfRows / 2 + i), n, (-maze.NumberOfColumns / 2 + j));
                            var unoccupied = Instantiate(unoccupiedPrefab, parentForNewObjects) as Transform;
                            unoccupied.position = unoccupiedPosition;
                        }
                    }
                }
            }
        }

    }
}
