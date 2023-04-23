using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.InputSystem;


public class InitializeField : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject wall;
    public GameObject bomberman;

    void Start()
    {
        PlaceBomberman();
        
        for (int i = 0; i < 60; i++)
        {
            PlaceWall();
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
    void PlaceWall()
    {
        var vect = GetRandomVect();
        //checking for colliding walls
        Collider[] colliders = Physics.OverlapSphere(vect, 0.4f);
        if (colliders.Length == 0)
        {
            //check if it collides with bomberman
            colliders = Physics.OverlapSphere(vect, 2f);
            //if (!colliders.Any(c => c.GetComponent<BombermanCharacterController>() != null)) {
            if (!colliders.Any(c => c.tag.Equals("Player"))) {
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

        bomberman.name = "Player 1";
        var p1 = PlayerInput.Instantiate(bomberman,
            controlScheme: "Keyboard.Arrows", pairWithDevice: Keyboard.current);

        p1.transform.position = vect;
        
        Vector3 vect2;
        do
        {
            vect2 = GetRandomVect();
        } while (Physics.OverlapSphere(vect2, 0.4f).Length != 0);
        
        bomberman.name = "Player 2";
        var p2 = PlayerInput.Instantiate(bomberman,
            controlScheme: "Keyboard.WASD", pairWithDevice: Keyboard.current);

        p2.transform.position = vect2;

        //Instantiate(bomberman, vect, transform.rotation);
    }
}
