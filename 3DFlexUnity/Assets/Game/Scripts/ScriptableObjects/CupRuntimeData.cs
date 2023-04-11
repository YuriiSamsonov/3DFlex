using UnityEngine;

namespace Game.Scripts.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Data/CreateCupRuntimeData", fileName = "new CupRuntimeData")]
    public class CupRuntimeData : ScriptableObject
    {
        /// <summary>
        /// Desired and saved cup texture.
        /// </summary>
        [field: SerializeField]
        public Texture2D cupTexture;
    }
}