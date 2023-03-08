using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Scripts.Objects
{
    public class BrokenCup : MonoBehaviour
    {
        [field: SerializeField] 
        private MainMenu mainMenu;

        [field: SerializeField]
        private Renderer[] cupRenderer;
        
        private void Start()
        {
            cupRenderer = GetComponentsInChildren<Renderer>();
            
            for (int i = 0; i < cupRenderer.Length; i++)
            {
                cupRenderer[i].material.mainTexture = mainMenu.CupTexture[MainMenu.CurrentTexture];
            }
        }
    }
}