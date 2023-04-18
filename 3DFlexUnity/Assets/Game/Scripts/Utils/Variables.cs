using UnityEngine;

namespace Game.Scripts.Utils
{
    public static class Variables
    {
        public const string UntaggedTag = "Untagged";
        public const string PlayerTag = "Player";
        public const string EnemyBodyPart = "EnemyBodyPart";
        
        public const int MainMenuSceneBuildIndex = 0;
        public const int MainSceneBuildIndex = 1;
        
        public static WaitForSeconds WaitForHalfASecond = new WaitForSeconds(.5f);
    }
}