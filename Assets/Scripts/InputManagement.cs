using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZhaoXiaodan.Lab2
{
    public class InputManagement : MonoBehaviour
    {
        [SerializeField] private CameraSwitcher cameraSwitcher;

        private Input inputScheme;

        private void Awake()
        {
            inputScheme = new Input();
        }

        private void OnEnable()
        {
            var _ = new QuitHandler(inputScheme.Maze.Quit);
            var nextCameraHandler = new NextCameraHandler(inputScheme.Maze.CameraSwitch, this.cameraSwitcher);
        }
    }
}
