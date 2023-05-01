using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TRYINGSTUFFOUT.CursesV2.ScriptableObjects;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Utils;
using Random = UnityEngine.Random;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private BombermanStatsV3SO bombermanStatsV3So;
        [SerializeField] private GameObject bomberman;
        [SerializeField] private GameSettings gameSettings;

        private IDictionary<int, Vector3> playerSpawnLocations;

        private void Awake()
        {
            playerSpawnLocations = new Dictionary<int, Vector3>();
            bombermanStatsV3So.lives = gameSettings.playerLivesToStartWith;
            StageManager.onStageCreation += SetBombermanSpawnLocations;
        }

        private void Start()
        {
            OnBombermanDestroy.onBombermanDeath += BombermanRespawn;
        }

        void SetBombermanSpawnLocations(int stageLength, int stageWidth)
        {
            playerSpawnLocations.Add(1, new Vector3(1, 0.6f, 1));
            playerSpawnLocations.Add(2, new Vector3(stageLength-2, 0.6f, stageWidth-2));
            playerSpawnLocations.Add(3, new Vector3(1, 0.6f, stageWidth-2));
            playerSpawnLocations.Add(4, new Vector3(stageLength-2, 0.6f, 1));

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
        }

        void BombermanRespawn(float lives, GameObject bomberman)
        {
            if(lives <= 0)
                return;
            
            Vector3 vect;
            do
            {
                vect = GetRandomVect();
            } while (Physics.OverlapSphere(vect, 0.4f).Length != 0);
            
            StartCoroutine(RespawnDelay(bomberman, vect));
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