using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    void Start()
    {
        
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }



}
