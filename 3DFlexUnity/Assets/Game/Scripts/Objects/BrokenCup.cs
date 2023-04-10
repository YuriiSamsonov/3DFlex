 using Game.Scripts.ScriptableObjects;
using UnityEngine;

namespace Game.Scripts.Objects
{
    public class BrokenCup : MonoBehaviour
    {
        [field: SerializeField] 
        private CupRuntimeData cupRuntimeData;

        [field: SerializeField]
        private Renderer[] cupRenderer;
        
        private void Start()
        {
            cupRenderer = GetComponentsInChildren<Renderer>();
            
            for (int i = 0; i < cupRenderer.Length; i++)
                cupRenderer[i].material.mainTexture = cupRuntimeData.cupTexture;
        }
    }
}