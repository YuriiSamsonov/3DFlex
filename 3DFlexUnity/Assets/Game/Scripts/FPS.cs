using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Scripts
{
    public class FPS : MonoBehaviour
    {
        [field: SerializeField] 
        private int fps = 60;

        private void OnValidate()
        {
            Application.targetFrameRate = fps;
        }
    }
}