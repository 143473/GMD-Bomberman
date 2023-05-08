using System.Linq;
using UnityEngine;
using Utils;

namespace Bomberman.AI.States
{
    public class Unstuck:IState
    {
        private AIBombermanController aiBombermanController;
        private int[,] stageGrid;

        public Unstuck(AIBombermanController aiBombermanController)
        {
            this.aiBombermanController = aiBombermanController;
        }

        public void Tick()
        {

            stageGrid = aiBombermanController.GetGrid().GetGrid();
            var a = aiBombermanController
                .GetFreeNeighbors(aiBombermanController.transform.position)
                .FirstOrDefault(a => stageGrid[a.x, a.y] == 0);
            var aVector = new Vector3(a.x, 0, a.y);
            aiBombermanController.transform.position = aVector;
            aiBombermanController.isStuck = false;
            // float step =
            //     (aiBombermanController.gameObject.GetComponent<FinalBombermanStats>()
            //         .GetNumericStat(Stats.Speed) - 3) * Time.deltaTime;
            //         
            // aiBombermanController.transform.LookAt(aVector);
            // aiBombermanController.transform.Rotate(0, 180, 0);
            //         
            // aiBombermanController.transform.position =
            //     Vector3.MoveTowards(aiBombermanController.transform.position
            //         ,aVector, step);
            // if (Vector3.Distance(aiBombermanController.transform.position,
            //         aVector) < 0.01)
            // {
            //
            // }
        }

        public void OnEnter()
        {
           


            

        }

        public void OnExit()
        {
            
        }
    }
}