using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ZhaoXiaodan.Lab2
{
    public class CreateMazeLevel : MonoBehaviour
    {
        [SerializeField] public CreateAbstractMaze abstractMaze;
        [SerializeField] public GridGameObjectFactory gridFactory;

        public void Build()
        {
            abstractMaze.Build();
            var maze = abstractMaze.mazeGraph;
            var mazeAdaptor = new MazeToOccupancyGtaphAdaptor(maze);
            var occupancyGraph = mazeAdaptor.build();
            gridFactory.createPrefab(occupancyGraph);
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(CreateMazeLevel))]
    public class CreateMazeLevelEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            CreateMazeLevel create = (CreateMazeLevel)target;
            base.OnInspectorGUI();
            if(GUILayout.Button("Generate Maze"))
            {
                create.Build();
            }
        }
    }
#endif
}

