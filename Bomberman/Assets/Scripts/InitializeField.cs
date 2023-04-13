using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class InitializeField : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject wall;
    public GameObject bomberman;

    void Start()
    {
        PlaceBomberman();
        
        for (int i = 0; i < 70; i++)
        {
            PlaceWall();
        }
    }

    Vector3 GetRandomVect()
    {
        return new Vector3(GetRandomPos(), 0.5f, GetRandomPos());
    }
    
    float GetRandomPos()
    {
        return ToGrid(Random.Range(-8.75f, 8.75f));
    }

    float ToGrid(float pos)
    {
        return Mathf.Round(pos / 1.25f) * 1.25f;
    }

    void PlaceWall()
    {
        var vect = GetRandomVect();
        //checking for colliding walls
        Collider[] colliders = Physics.OverlapSphere(vect, 0.4f);
        if (colliders.Length == 0)
        {
            //check if it collides with bomberman
            colliders = Physics.OverlapSphere(vect, 2f);
            if (!colliders.Any(c => c.GetComponent<BombermanCharacterController>() != null)) {
                Instantiate(wall, vect, transform.rotation);
            }
        }
    }

    void PlaceBomberman()
    {
        Vector3 vect;
        do
        {
            vect = GetRandomVect();
        } while (Physics.OverlapSphere(vect, 0.4f).Length != 0);
        
        Instantiate(bomberman, vect, transform.rotation);
    }
}
