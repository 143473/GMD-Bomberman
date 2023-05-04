using System;
using UnityEngine;
using UnityEngine.AI;

namespace Bomberman.AI
{
    public class NavigationBaker : MonoBehaviour
    {

        public NavMeshSurface surface;
        public NavMeshSurface[] surfaces;

        // Use this for initialization
        private void Awake()
        {
            StageManager.onStageCreation2 += BakeIt;
        }

        private void FixedUpdate()
        {
            if (Input.anyKeyDown)
            {
                // foreach (var surface in surfaces)
                // {
                //     surface.BuildNavMesh();
                // }
                surface.BuildNavMesh();
            }
        }
        // Use this for initialization
        void BakeIt(GameObject stage)
        {
            surfaces = stage.GetComponentsInChildren<NavMeshSurface>();
            foreach (var surface in surfaces)
            {
                this.surface = surface;
            }
            surface.BuildNavMesh();
        }
    }
}