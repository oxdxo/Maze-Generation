using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ZhaoXiaodan.Lab2
{
    public class CreateAbstractMaze : MonoBehaviour
    {
        enum GenerationAlgorithm { RecursiveBack, HuntAndKill, BinaryTree }

        [SerializeField] private int width;
        [SerializeField] private int height;
        [SerializeField] private int entranceColumn;
        [SerializeField] private int exitColumn;
        [SerializeField] private int randomSeed;
        [SerializeField] private GenerationAlgorithm generationAlgorithm;

        public GridGraph<Direction> mazeGraph;

        [Flags]
        public enum Direction
        {
            BOTTOM = 1,   // 0001
            RIGHT = 2,  // 0010
            TOP = 4,     // 0100
            LEFT = 8,   // 1000

            VISITED = 128, // 1000 0000

        }

        // Start is called before the first frame update
        public void Build()
        {
            mazeGraph = new GridGraph<Direction>(width, height);
            Direction initial = Direction.BOTTOM | Direction.RIGHT | Direction.TOP | Direction.LEFT;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    mazeGraph.SetCellValue(i, j, initial); //initial all
                }
            }
            mazeGraph.SetCellValue(entranceColumn, height - 1, Direction.BOTTOM | Direction.RIGHT | Direction.LEFT); //Create the entrance
            mazeGraph.SetCellValue(exitColumn, 0, Direction.RIGHT | Direction.TOP | Direction.LEFT); //Create the exit

            if(generationAlgorithm == GenerationAlgorithm.RecursiveBack)
            {
                RecursiveBackTracker();
            }
            else if (generationAlgorithm == GenerationAlgorithm.HuntAndKill)
            {
                HuntAndKill();
            } else
            {
                BinaryTree();
            }
        }

        private void RecursiveBackTracker()
        {
            var random = new System.Random(randomSeed);
            var currentPath = new Stack<Vector2Int>();
            var startNode = new Vector2Int { x = random.Next(0, width), y = random.Next(0, height) };

            List<Vector2Int> neighbors = new List<Vector2Int>(4);

            Direction d = mazeGraph.GetCellValue(startNode.x, startNode.y);
            mazeGraph.SetCellValue(startNode.x, startNode.y, d |= Direction.VISITED);
            currentPath.Push(startNode);

            while (currentPath.Count > 0)
            {
                Vector2Int currentNode = currentPath.Pop();
                neighbors = getUnvisitedNeighbors (currentNode);
                Debug.Log("currentNode "+ currentNode);

                if (neighbors.Count > 0)
                {
                    currentPath.Push(currentNode);
                    var randIndex = random.Next(0, neighbors.Count);
                    var randNeighbor = neighbors[randIndex];
                    removeWall(currentNode, randNeighbor);
                    Direction neighborD = mazeGraph.GetCellValue(randNeighbor.x, randNeighbor.y);
                    mazeGraph.SetCellValue(randNeighbor.x, randNeighbor.y, neighborD |= Direction.VISITED); 
                    currentPath.Push(randNeighbor);
                }

            }

        }


        private void HuntAndKill()
        {
            bool courseComplete = false;
            var random = new System.Random(randomSeed);
            var currentNode = new Vector2Int { x = random.Next(0, width), y = random.Next(0, height) };

            Direction d = mazeGraph.GetCellValue(currentNode.x, currentNode.y);
            mazeGraph.SetCellValue(currentNode.x, currentNode.y, d |= Direction.VISITED);

            while (!courseComplete)
            {
                var neighbors = getUnvisitedNeighbors(currentNode);
                while (neighbors.Count > 0)
                {
                    var randIndex = random.Next(0, neighbors.Count);
                    var randNeighbor = neighbors[randIndex];
                    removeWall(currentNode, randNeighbor);
                    currentNode = randNeighbor;
                    d = mazeGraph.GetCellValue(currentNode.x, currentNode.y);
                    mazeGraph.SetCellValue(currentNode.x, currentNode.y, d |= Direction.VISITED);
                    neighbors = getUnvisitedNeighbors(currentNode);
                }

                courseComplete = true;
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        neighbors = getVisitedNeighbors(new Vector2Int(i, j));
                        if (!mazeGraph.GetCellValue(i, j).HasFlag(Direction.VISITED) && neighbors.Count > 0)
                        {
                            courseComplete = false;
                            currentNode = new Vector2Int(i, j);
                            var randIndex = random.Next(0, neighbors.Count);
                            var randNeighbor = neighbors[randIndex];
                            removeWall(currentNode, randNeighbor);
                            d = mazeGraph.GetCellValue(currentNode.x, currentNode.y);
                            mazeGraph.SetCellValue(currentNode.x, currentNode.y, d |= Direction.VISITED);
                        }

                    }
                }

            }
        }

        private void BinaryTree()
        {
            var random = new System.Random(randomSeed);
            
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    var randIndex = random.Next(0, 2);
                    bool goUp = true;
                    if (randIndex == 0) { goUp = false; }

                    if(i == width - 1)
                    {
                        goUp = true;
                    }else if(j == height -1)
                    {
                        goUp = false;
                    }

                    var currentNode = new Vector2Int(i, j);
                    if (goUp)
                    {
                        if (j + 1 < height)
                        {
                            removeWall(currentNode, new Vector2Int(i, j + 1));
                        }
                    }
                    else
                    {
                        if (i + 1 < width)
                        {
                            removeWall(currentNode, new Vector2Int(i + 1, j));
                        }
                    }
                }
            }
            mazeGraph.SetCellValue(width - 1, height - 1, Direction.RIGHT | Direction.TOP);

        }

        private List<Vector2Int> getVisitedNeighbors(Vector2Int currentNode)
        {
            List<Vector2Int> neighbors = new List<Vector2Int>();

            if (currentNode.x > 0) // Left
            {
                if (mazeGraph.GetCellValue(currentNode.x - 1, currentNode.y).HasFlag(Direction.VISITED))
                {
                    neighbors.Add(new Vector2Int(currentNode.x - 1, currentNode.y));
                }
            }

            if (currentNode.x < width - 1) //Right
            {
                if (mazeGraph.GetCellValue(currentNode.x + 1, currentNode.y).HasFlag(Direction.VISITED))
                {
                    neighbors.Add(new Vector2Int(currentNode.x + 1, currentNode.y));
                }
            }

            if (currentNode.y > 0) // Down
            {
                if (mazeGraph.GetCellValue(currentNode.x, currentNode.y - 1).HasFlag(Direction.VISITED))
                {
                    neighbors.Add(new Vector2Int(currentNode.x, currentNode.y - 1));
                }
            }

            if (currentNode.y < height - 1) // Up
            {
                if (mazeGraph.GetCellValue(currentNode.x, currentNode.y + 1).HasFlag(Direction.VISITED))
                {
                    neighbors.Add(new Vector2Int(currentNode.x, currentNode.y + 1));
                }
            }

            return neighbors;
        }


        private List<Vector2Int> getUnvisitedNeighbors(Vector2Int currentNode)
        {
            List<Vector2Int> neighbors = new List<Vector2Int>();

            if(currentNode.x > 0) // Left
            {
                if (!mazeGraph.GetCellValue(currentNode.x - 1, currentNode.y).HasFlag(Direction.VISITED))
                {
                    neighbors.Add(new Vector2Int(currentNode.x - 1,currentNode.y));
                }
            }

            if(currentNode.x < width - 1) //Right
            {
                if (!mazeGraph.GetCellValue(currentNode.x + 1, currentNode.y).HasFlag(Direction.VISITED))
                {
                    neighbors.Add(new Vector2Int(currentNode.x + 1, currentNode.y));
                }
            }

            if(currentNode.y > 0) // Down
            {
                if (!mazeGraph.GetCellValue(currentNode.x, currentNode.y - 1).HasFlag(Direction.VISITED))
                {
                    neighbors.Add(new Vector2Int(currentNode.x, currentNode.y - 1));
                }
            }

            if(currentNode.y < height - 1) // Up
            {
                if (!mazeGraph.GetCellValue(currentNode.x, currentNode.y + 1).HasFlag(Direction.VISITED))
                {
                    neighbors.Add(new Vector2Int(currentNode.x, currentNode.y + 1));
                }
            }

            return neighbors;
        }

        private void removeWall(Vector2Int currentNode, Vector2Int neighbor)
        {
            Direction currentD = mazeGraph.GetCellValue(currentNode.x, currentNode.y);
            Direction neighborD = mazeGraph.GetCellValue(neighbor.x, neighbor.y);

            // Remove the wall between Neighbor and CurrentNode
            if (neighbor.x == currentNode.x)
            {
                if (neighbor.y > currentNode.y) // Neighbor is at the top of CurrentNode
                {
                    mazeGraph.SetCellValue(currentNode.x, currentNode.y, currentD &= ~Direction.TOP);
                    mazeGraph.SetCellValue(neighbor.x, neighbor.y, neighborD &= ~Direction.BOTTOM);
                }
                else    // Neighbor is at the bottom of CurrentNode
                {
                    mazeGraph.SetCellValue(currentNode.x, currentNode.y, currentD &= ~Direction.BOTTOM);
                    mazeGraph.SetCellValue(neighbor.x, neighbor.y, neighborD &= ~Direction.TOP);
                }
            }
            else if (neighbor.x > currentNode.x) // Neighbor is at the right of CurrentNode
            {
                mazeGraph.SetCellValue(currentNode.x, currentNode.y, currentD &= ~Direction.RIGHT);
                mazeGraph.SetCellValue(neighbor.x, neighbor.y, neighborD &= ~Direction.LEFT);
            }
            else // Neighbor is at the left of CurrentNode
            {
                mazeGraph.SetCellValue(currentNode.x, currentNode.y, currentD &= ~Direction.LEFT);
                mazeGraph.SetCellValue(neighbor.x, neighbor.y, neighborD &= ~Direction.RIGHT);
            }
        }
    }
}
