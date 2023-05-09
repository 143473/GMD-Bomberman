using System.Collections.Generic;
using System.Linq;
using Bomberman.AI.StatesV2.ControllerV2;
using UnityEngine;

namespace Bomberman.AI.StatesV2
{
    public class SearchTargetPriority : IState
    {
        private AIControllerV2 aiControllerV2;
        private int[,] stageGrid;
        private Gridx gridx;
        private Helper helper;
        private List<(int x, int y)> list;

        public SearchTargetPriority(AIControllerV2 aiControllerV2)
        {
            this.aiControllerV2 = aiControllerV2;
            helper = new Helper();
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            Debug.Log("Searching");

            var player = GameObject
                .FindGameObjectsWithTag("Player")
                .OrderBy(v => Vector3.Distance(aiControllerV2.transform.position, v.transform.position))
                .FirstOrDefault(a => a.name != aiControllerV2.gameObject.name);

            aiControllerV2.searching = false;

            gridx = aiControllerV2.GetGridx();
            stageGrid = aiControllerV2.GetGridx().GetGrid();
            list = helper.SearchGridFor(Gridx.Legend.DWall, stageGrid);

            var currV = aiControllerV2.transform.position;
            gridx.GetXY(currV, out int x, out int y);
            
            var target = list
                .OrderBy(a => Vector3.Distance(currV, new Vector3(a.x, 0, a.y)))
                .FirstOrDefault(); 
            list.Remove(target);
            // aiControllerV2.ComputePath(new Vector3(target.x, 0, target.y));
            aiControllerV2.ComputePath(player.transform.position);
            
            // aiControllerV2.targetType = helper.GetTypeWithValue(stageGrid[target.x, target.y]);
        }

        public void OnExit()
        {
            list.Clear();
            aiControllerV2.searching = true;
        }
    }
}