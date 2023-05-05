using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Utils;

namespace Bomberman.AI.States
{
    public class SearchForTarget : IState
    {
        private readonly AIBombermanController aIBombermanController;
        private NavMeshPath navMeshPath = new NavMeshPath();
        private NavMeshAgent navMeshAgent;

        public SearchForTarget(AIBombermanController aiBombermanController, NavMeshAgent navMeshAgent)
        {
            this.aIBombermanController = aiBombermanController;
            this.navMeshAgent = navMeshAgent;
        }

        public void Tick()
        {
            aIBombermanController.potentialTarget = ChooseOneOfTheNearestTargets();
        }

        private GameObject ChooseOneOfTheNearestTargets()
        {
            var powerUp = FindNearestPowerUp();
            var player = FindNearestPlayer();
            var wall = FindNearestDestructibleWall();

            if (powerUp != null && IsTargetReachable(powerUp.transform.position))
                return powerUp;
            if (player != null && IsTargetReachable(player.transform.position))
                return player;
            if (wall != null && IsTargetReachable(wall.transform.position))
                return wall;

            return null;
        }

        [CanBeNull]
        private GameObject FindNearestDestructibleWall()
        {
            return GameObject
                .FindGameObjectsWithTag("DestructibleWall")
                .OrderBy(v => Vector3.Distance(aIBombermanController.transform.position, v.transform.position))
                .FirstOrDefault();
        }

        [CanBeNull]
        private GameObject FindNearestPowerUp()
        {
            return GameObject
                .FindGameObjectsWithTag("PowerUp")
                .OrderBy(v => Vector3.Distance(aIBombermanController.transform.position, v.transform.position))
                .FirstOrDefault();
        }

        [CanBeNull]
        private GameObject FindNearestPlayer()
        {
            return GameObject
                .FindGameObjectsWithTag("Player")
                .OrderBy(v => Vector3.Distance(aIBombermanController.transform.position, v.transform.position))
                .FirstOrDefault();
        }

        bool IsTargetReachable(Vector3 target)
        {
            navMeshAgent.CalculatePath(target, navMeshPath);
            if (navMeshPath.status != NavMeshPathStatus.PathComplete)
            {
                return false;
            }
            else
            {
                return true;
            }


            return false;
        }

        public void OnEnter()
        {
        }

        public void OnExit()
        {
        }
    }
}