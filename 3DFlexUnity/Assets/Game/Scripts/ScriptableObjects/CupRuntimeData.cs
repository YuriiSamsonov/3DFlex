using UnityEngine;

namespace Game.Scripts.ScriptableObjects
{
    /// <summary>
    /// Instance of this scriptable object will be responsible for delivering the cup texture between the main menu and
    /// gameplay scenes. There should be only one instance of this scriptable object.
    /// </summary>
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