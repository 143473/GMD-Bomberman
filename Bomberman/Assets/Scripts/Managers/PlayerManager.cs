using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TRYINGSTUFFOUT.CursesV2.ScriptableObjects;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Utils;
using Random = UnityEngine.Random;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        [FormerlySerializedAs("bombermanStatsV3So")] [SerializeField] private BombermanStatsSO bombermanStatsSo;
        [SerializeField] private GameObject bomberman;
        [SerializeField] private GameSettings gameSettings;
        [SerializeField] private Canvas canvasPrefab;


        private IDictionary<int, Vector3> playerSpawnLocations;

        private void Awake()
        {
            playerSpawnLocations = new Dictionary<int, Vector3>();
            bombermanStatsSo.lives = gameSettings.playerLivesToStartWith;
            StageManager.onStageCreation += SetBombermanSpawnLocations;
        }

        private void Start()
        {
            OnBombermanDestroy.onBombermanDeath += BombermanRespawn;
        }

        void SetBombermanSpawnLocations(int stageLength, int stageWidth)
        {
            playerSpawnLocations.Add(1, new Vector3(1, 0.5f, 1));
            playerSpawnLocations.Add(2, new Vector3(stageLength-2, 0.5f, stageWidth-2));
            playerSpawnLocations.Add(3, new Vector3(1, 0.5f, stageWidth-2));
            playerSpawnLocations.Add(4, new Vector3(stageLength-2, 0.5f, 1));

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
           CreateCanvas(canvasPrefab, 2f,11f, player1.name);

            if (gameSettings.numberOfHumanPlayers == 2)
            {
                var player2 = PlayerInput.Instantiate(bomberman,
                    controlScheme: player2KeyboardScheme, pairWithDevice: Keyboard.current);
                player2.name = "Player 2";
                player2.transform.position = playerSpawnLocations[2]; 
               CreateCanvas(canvasPrefab,18f,11f, player2.name);

            }
        }

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
        
        private void CreateCanvas(Canvas canvasP, float x, float z, string playerName) {
            var canvas = Instantiate(canvasP);
            canvas.transform.position = new Vector3(x, 1, z);
            canvas.name = playerName + " Canvas";
        }
    }
}