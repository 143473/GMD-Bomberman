using System;
using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        private PlayerInputManager playerInputManager;

        private void Awake()
        {
            playerInputManager = PlayerInputManager.instance;
        }

        private void Start()
        {
            BombermanInstantiation();
            OnBombermanDestroy.onBombermanDeath += BombermanRespawn;
        }

        void BombermanInstantiation()
        {
            
        }

        void BombermanRespawn(float lives, GameObject bomberman)
        {
            Debug.Log("xxx");
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

        IEnumerator BombermanRespawnFlash(GameObject gameObject)
        {
            float timer = 2;
            while (timer > 0)
            {
                timer -= Time.deltaTime;
                // gameObject.GetComponents<MeshRenderer>()
                yield return new WaitForSeconds(0.4f);
            }
        }
        Vector3 GetRandomVect()
        {
            return new Vector3(GetRandomPos(1f,15f), 0.5f, GetRandomPos(-7f, 7f));
        }
    
        float GetRandomPos(float from, float to)
        {
            return Mathf.Round(Random.Range(from, to));
        }
    }
}