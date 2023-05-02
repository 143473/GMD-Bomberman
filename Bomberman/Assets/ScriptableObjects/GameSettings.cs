using UnityEditor;
using UnityEngine;

namespace TRYINGSTUFFOUT.CursesV2.ScriptableObjects
{
    [CreateAssetMenu]
    public class GameSettings : ScriptableObject
    {
        public int numberOfHumanPlayers = 1;
        public int numberOfAIPlayers = 0;
        public StageSet stageSet = StageSet.Regular;
        public int playerLivesToStartWith = 3;
        public float stageTimerInSeconds = 180;
        public HumanKeyboardLayout player1Layout = HumanKeyboardLayout.WASD;
        public HumanKeyboardLayout player2Layout = HumanKeyboardLayout.Arrows;
        
        public enum StageSet
        {
            Regular,
            Ice
        }

        public enum HumanKeyboardLayout
        {
            WASD,
            Arrows
        }
    }
}