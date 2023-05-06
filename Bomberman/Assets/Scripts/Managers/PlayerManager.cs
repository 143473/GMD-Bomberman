using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TRYINGSTUFFOUT.CursesV2.ScriptableObjects;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using Utils;
using Random = UnityEngine.Random;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        [FormerlySerializedAs("bombermanStatsV3So")] [SerializeField] private BombermanStatsSO bombermanStatsSo;
        [SerializeField] private GameObject bomberman;
        [SerializeField] private GameObject bombermanAI;
        [SerializeField] private GameSettings gameSettings;

        // public delegate void OnNavAgentAttachment();
        // public static OnNavAgentAttachment onNavAgentAttachment;
        
        private List<GameObject> aiList;
        private IDictionary<int, Vector3> playerSpawnLocations;

        private void Awake()
        {
            aiList =  new List<GameObject> { };
            playerSpawnLocations = new Dictionary<int, Vector3>();
            bombermanStatsSo.lives = gameSettings.playerLivesToStartWith;
            StageManager.onStageCreation += SetBombermanSpawnLocations;
            OnBombermanDestroy.onBombermanDeath += BombermanRespawn;
        }

        private void Start()
        {

        }

        void SetBombermanSpawnLocations(int stageLength, int stageWidth)
        {
            playerSpawnLocations.Add(1, new Vector3(1, 0.5f, 1));
            playerSpawnLocations.Add(2, new Vector3(stageLength-2, 0.5f, stageWidth-2));
            playerSpawnLocations.Add(3, new Vector3(1, 1f, stageWidth-2));
            playerSpawnLocations.Add(4, new Vector3(stageLength-2, 1f, 1));

            BombermanInstantiation();
        }

        void BombermanInstantiation()
        {
            var player1KeyboardScheme = $"Keyboard." + gameSettings.player1Layout;
            var player2KeyboardScheme = $"Keyboard." + gameSettings.player2Layout;

            var player1 = PlayerInput.Instantiate(bomberman,
                controlScheme: player1KeyboardScheme, pairWithDevice: Keyboard.current);
            player1.name = "Player 1";
            player1.transform.position = playerSpawnLocations[1];

            if (gameSettings.numberOfHumanPlayers == 2)
            {
                var player2 = PlayerInput.Instantiate(bomberman,
                    controlScheme: player2KeyboardScheme, pairWithDevice: Keyboard.current);
                player2.name = "Player 2";
                player2.transform.position = playerSpawnLocations[2];
            }
        
            
            // AI instantiation
            if (gameSettings.numberOfAIPlayers != 0)
            {
                var humans = gameSettings.numberOfHumanPlayers;
                for (int i = 1; i <= gameSettings.numberOfAIPlayers; i++)
                {
                    var ai = Instantiate(bombermanAI);
                    ai.name = $"Player {humans + i}";
                    ai.transform.position = playerSpawnLocations[humans + i];
                    aiList.Add(ai);
                }
            }
            //StartCoroutine(AiBombermanAgentDelay());
        }

        // IEnumerator AiBombermanAgentDelay()
        // {
        //     yield return new WaitForSeconds(0.3f);
        //     foreach (var ai in aiList)
        //     {
        //         ai.AddComponent<NavMeshAgent>();
        //     }
        //     onNavAgentAttachment?.Invoke();
        // }

        void BombermanRespawn(float lives, GameObject bomberman)
        {
            if(lives <= 0)
                return;
            // Work in progress, vectors for respawn
            // Vector3 vect;
            // do
            // {
            //     vect = GetRandomVect();
            // } while (Physics.OverlapSphere(vect, 0.4f).Length != 0);

            var random = Random.Range(1, playerSpawnLocations.Count+1);
            StartCoroutine(RespawnDelay(bomberman, playerSpawnLocations[random]));
        }

        IEnumerator RespawnDelay(GameObject bomberman, Vector3 vect)
        {
            yield return new WaitForSeconds(2f);
            bomberman.transform.position = vect;
            bomberman.GetComponent<BombermanCharacterController>().enabled = true;
        }
        Vector3 GetRandomVect()
        {
            return new Vector3(GetRandomPos(1f,15f), 0.7f, GetRandomPos(-7f, 7f));
        }
    
        float GetRandomPos(float from, float to)
        {
            return Mathf.Round(Random.Range(from, to));
        }
    }
}