using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

namespace Bomberman.AI.States
{
    public class SearchForTarget: IState
    {
        private AIBombermanController aiBombermanController;

        public SearchForTarget(AIBombermanController aiBombermanController)
        {
            this.aiBombermanController = aiBombermanController;
        }

        public void Tick()
        {
            aiBombermanController.potentialTarget = ChooseOneOfTheNearestTargets();
        }
         private GameObject ChooseOneOfTheNearestTargets()
         {
             Debug.Log("searching for target");
            var powerUp = FindNearestPowerUp();
            if (powerUp != null && aiBombermanController.ComputePath(powerUp.transform.position))
                return powerUp;
            
            var wall = FindNearestDestructibleWall();
            if (wall != null && aiBombermanController.ComputePath(wall.transform.position))
                return wall;
            
            var player = FindNearestPlayer();
            if (player != null && aiBombermanController.ComputePath(player.transform.position))
                return player;
            
            
            return null;
        }
         
         private GameObject FindNearestDestructibleWall()
         {
             return GameObject.FindGameObjectsWithTag("DestructibleWall")
                     .OrderBy(v => Vector3.Distance(aiBombermanController.transform.position, v.transform.position))
                     .FirstOrDefault();
         }
         
        private GameObject FindNearestPowerUp()
        {
            return GameObject
                .FindGameObjectsWithTag("PowerUp")
                .OrderBy(v => Vector3.Distance(aiBombermanController.transform.position, v.transform.position))
                .FirstOrDefault();
        }
        
        private GameObject FindNearestPlayer()
        {
            return GameObject
                .FindGameObjectsWithTag("Player")
                .OrderBy(v => Vector3.Distance(aiBombermanController.transform.position, v.transform.position))
                .FirstOrDefault(a => a.name != aiBombermanController.gameObject.name);
        }

        public void OnEnter()
        {

        }

        public void OnExit()
        {
        }
    }
}