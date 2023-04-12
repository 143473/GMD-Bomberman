using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{

  //  public GameObject destroyableWall;
    // Start is called before the first frame update
    void Start()
    {
    //    Instantiate(destroyableWall, transform.position, transform.rotation);
        
    }

    public void DestroyWall()
    {
        Destroy(gameObject);
    }

}
