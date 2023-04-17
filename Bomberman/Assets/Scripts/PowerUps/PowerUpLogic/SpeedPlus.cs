using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using PowerUps.Interfaces;
using UnityEngine;

public class SpeedPlus : MonoBehaviour,IPowerUp
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyEffect(BombermanStats bombermanStats)
    {
        bombermanStats.Speed++;
    }
}
