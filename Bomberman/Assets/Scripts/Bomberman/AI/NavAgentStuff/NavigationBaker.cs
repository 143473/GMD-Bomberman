using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Bomberman.AI
{
    public class NavigationBaker : MonoBehaviour
    {

        public NavMeshSurface surface;
        public NavMeshSurface[] surfaces;
        public bool doneUpdating = true;

        // Use this for initialization
        private void Awake()
        {
            StageManager.onStageCreation2 += BakeIt;
        }

        private void FixedUpdate()
        {
            if(Input.anyKeyDown)
                StartCoroutine( UpdateNavMeshAsyncCoroutine());
        }
        
        public IEnumerator UpdateNavMeshAsyncCoroutine()
        {
            var operation = surface.UpdateNavMesh(surface.navMeshData);
            do
            {
                yield return null;
            } 
            while (!operation.isDone);
        }
        


        // IEnumerator Bake()
        // {
        //     startedBaking = true;
        //     while (true)
        //     {
        //         yield return new WaitForSeconds(5f);
        //         surface.BuildNavMesh();
        //     }
        // }
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