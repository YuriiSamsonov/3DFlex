using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Scripts.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Data/CreateCupRuntimeData", fileName = "new CupRuntimeData")]
    public class CupRuntimeData : ScriptableObject
    {
        [field: SerializeField]
        public Texture2D cupTexture;
    }
}