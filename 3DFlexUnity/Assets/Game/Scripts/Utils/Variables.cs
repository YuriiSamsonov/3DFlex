using UnityEngine;

namespace Game.Scripts.Utils
{
    public static class Variables
    {
        /// <summary>
        /// "Untagged"
        /// </summary>
        public const string UntaggedTag = "Untagged";
        /// <summary>
        /// "Player"
        /// </summary>
        public const string PlayerTag = "Player";

        /// <summary>
        /// "EnemyBodyPart"
        /// </summary>
        public const string EnemyBodyPart = "EnemyBodyPart";

        /// <summary>
        /// 0
        /// </summary>
        public const int MainMenuSceneBuildIndex = 0;
        /// <summary>
        /// 1
        /// </summary>
        public const int MainSceneBuildIndex = 1;

        /// <summary>
        /// new WaitForSeconds(.5f)
        /// </summary>
        public static WaitForSeconds WaitForHalfASecond = new WaitForSeconds(.5f);
    }
}