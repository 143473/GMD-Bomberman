using System.Collections.Generic;
using Bomberman.AI.StatesV2.ControllerV2;
using UnityEngine;

namespace Bomberman.AI.StatesV2
{
    public class CheckForBombs : IState
    {
        private AIControllerV2 aiControllerV2;
        private Helper helper;
        private List<(int x, int y)> bombs;
        private int[,] stageGrid;

        public CheckForBombs(AIControllerV2 aiControllerV2)
        {
            helper = new Helper();
            this.aiControllerV2 = aiControllerV2;
        }

        public void Tick()
        {
            
        }

        public void OnEnter()
        {
            Debug.Log("Checking");
            aiControllerV2.checkedForBombs = false;
            
            stageGrid = aiControllerV2.GetGridx().GetGrid();
            bombs = helper.SearchGridFor(Gridx.Legend.Bomb, stageGrid);
        }

        public void OnExit()
        {
            aiControllerV2.checkedForBombs = true;
        }
    }
}