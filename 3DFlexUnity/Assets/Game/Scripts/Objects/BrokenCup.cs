using Game.Scripts.ScriptableObjects;
using UnityEngine;

namespace Game.Scripts.Objects
{
    public class BrokenCup : MonoBehaviour
    {
        /// <summary>
        /// Data to store cup texture in runtime.
        /// </summary>
        [field: SerializeField, Tooltip("Data to store cup texture in runtime.")] 
        private CupRuntimeData cupRuntimeData;

        /// <summary>
        /// Render array of cup parts.
        /// </summary>
        [field: SerializeField, Tooltip("Render array of cup parts.")]
        private Renderer[] cupRenderer;
        
        private void Start()
        {
            cupRenderer = GetComponentsInChildren<Renderer>();
            
            for (int i = 0; i < cupRenderer.Length; i++)
                cupRenderer[i].material.mainTexture = cupRuntimeData.cupTexture;
        }
    }
}