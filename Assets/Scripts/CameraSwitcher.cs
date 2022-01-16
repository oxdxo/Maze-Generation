using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZhaoXiaodan.Lab2
{
    public class CameraSwitcher : MonoBehaviour
    {
        [SerializeField] private Camera[] cameras;
        [SerializeField] private Camera defaultCamera;
        private int index = 0;

        void Start()
        {
            for (int i = 0; i < cameras.Length; i++)
            {
                if (cameras[i] == defaultCamera)
                {
                    index = i;
                    defaultCamera.gameObject.SetActive(true);
                }
                else
                {
                    cameras[i].gameObject.SetActive(false);
                }
            }
        }

        public void NextCamera()
        {
            if (index < cameras.Length - 1)
            {
                cameras[index + 1].gameObject.SetActive(true);
                cameras[index].gameObject.SetActive(false);
                index += 1;
            }
            else
            {
                cameras[0].gameObject.SetActive(true);
                cameras[index].gameObject.SetActive(false);
                index = 0;
            }

        }
    }
}
