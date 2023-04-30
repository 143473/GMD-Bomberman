using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;


public class InitializeField : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject wall;
    [SerializeField]
    private  GameObject bomberman;
    private GameObject destroyableWalls;

    private void Awake()
    {
        // For grouping the gameobjects created at runtime - prettier in editor 
        destroyableWalls = new GameObject
        {
            name = "Destroyable Walls"
        };
        
    }
    
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
        return new Vector3(GetRandomPos(1f,15f), 0f, GetRandomPos(-7f, 7f));
    }
    
    float GetRandomPos(float from, float to)
    {
        return Mathf.Round(Random.Range(from, to));
    }
    void PlaceWall()
    {
        var vect = GetRandomVect();
        
        if (Physics.OverlapSphere(vect, 0.4f).Length == 0)
        {
            Instantiate(wall, vect, transform.rotation, destroyableWalls.transform);
        }
    }
    void PlaceBomberman()
    {
        Vector3 vect;
        do
        {
            vect = GetRandomVect();
        } while (Physics.OverlapSphere(vect, 0.4f).Length != 0);

        var p1 = PlayerInput.Instantiate(bomberman,
            controlScheme: "Keyboard.Arrows", pairWithDevice: Keyboard.current);

        p1.transform.position = vect;
        p1.name = "Player 1";
        
        Vector3 vect2;
        do
        {
            vect2 = GetRandomVect();
        } while (Physics.OverlapSphere(vect2, 0.4f).Length != 0);

        var p2 = PlayerInput.Instantiate(bomberman,
            controlScheme: "Keyboard.WASD", pairWithDevice: Keyboard.current);

        p2.transform.position = vect2;
        p2.name = "Player 2";
    }
}
