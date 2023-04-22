using System;
using PickUps.Curses;
using UnityEngine;
using UnityEngine.UIElements;

namespace Bomberman
{
    public class CurseInteraction : MonoBehaviour
    {
        protected GameObject bomberman;
        private static readonly int Red = Shader.PropertyToID("Red");
        private static readonly int White = Shader.PropertyToID("White");

        private void Awake()
        {
            bomberman = gameObject;
            BombermanStats.onCursedBomberman += SetVisualToCursedMode;
        }

        private void Update()
        {
            if (!bomberman.GetComponent<BombermanStats>().Cursed)
            {
                SetVisualToNormalMode();
            }
        }

        protected void SetVisualToCursedMode()
        {
            var bombermanRenderer =  bomberman.GetComponent<Renderer>();
            bombermanRenderer.material.SetColor(Red, Color.red);
        }

        protected void SetVisualToNormalMode()
        {
            var bombermanRenderer =  bomberman.GetComponent<Renderer>();
            bombermanRenderer.material.SetColor(White, Color.white);
        }
        
        private void OnTriggerEnter(Collider bomberman)
        {
            if (gameObject.GetComponent<BombermanStats>().Cursed)
            {
                var curse = gameObject.GetComponent<Curse>();
                if (bomberman.gameObject.tag.Equals("Player") && !bomberman.gameObject.GetComponent<BombermanStats>().Cursed)
                {
                    bomberman.gameObject.AddComponent(curse.GetType());
                    bomberman.GetComponent<BombermanStats>().Cursed = true;
                }
            }
        }
    }
}