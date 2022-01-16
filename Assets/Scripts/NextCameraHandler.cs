using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ZhaoXiaodan.Lab2
{

        public class NextCameraHandler
        {
            private CameraSwitcher cameraSwitcher;
            public NextCameraHandler(InputAction cameraAction, CameraSwitcher cameraSwitcher)
            {
                this.cameraSwitcher = cameraSwitcher;
                cameraAction.performed += nextCamera_performed;
                cameraAction.Enable();
            }

            private void nextCamera_performed(InputAction.CallbackContext obj)
            {
                cameraSwitcher.NextCamera();
            }
        }
    }

