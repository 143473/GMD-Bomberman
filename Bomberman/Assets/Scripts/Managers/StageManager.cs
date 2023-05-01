using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class StageManager : MonoBehaviour
{
    [SerializeField] private GameObject field;
    [SerializeField] private int wallChanceToSpawn;
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject nonDestructibleWall;
    [SerializeField] private int stageLength = 15;
    [SerializeField] private int stageWidth = 15;

    private int[,] stageLayout;
    public delegate void OnStageCreation(int stageLength, int stageWidth);

    public static OnStageCreation onStageCreation;

    private GameObject stage;

    private void Awake()
    {
        stage = new GameObject
        {
            name = "Stage"
        };
    }

    private void Start()
    {
        //Instantiate(field);
        PlaceField();
        GenerateRandomStage();
    }

    void PlaceField()
    {
        // Scaling the field renders it black ....
        field.transform.localScale = new Vector3(stageLength / 10f, 0, stageWidth / 10f);
        field.transform.position = new Vector3((stageLength-1) / 2f, -0.5f, (stageWidth-1) / 2f);
        Instantiate(field, stage.transform);
    }

    void GenerateRandomStage()
    {
        stageLayout = new int[stageLength, stageWidth];

        for (int i = 0; i < stageLength; i++)
        {
            for (int j = 0; j < stageWidth; j++)
            {
                stageLayout[i, j] = 0;
            }
        }
        // Spawn outer walls
        for (int i = 0; i < stageLength; i++)
        {
            if (i == 0 || i == stageLength-1)
                for (int j = 0; j < stageWidth; j++)
                {
                    Vector3 vect = new Vector3(i, 0f, j);
                    stageLayout[i,j] = 5; 
                    Instantiate(nonDestructibleWall, vect, transform.rotation, stage.transform);
                }

            for (int j = 0; j < stageWidth; j++)
            {
                if (j == 0 || j == stageWidth-1)
                {
                    Vector3 vect = new Vector3(i, 0f, j);
                    stageLayout[i,j] = 5; 
                    Instantiate(nonDestructibleWall, vect, transform.rotation, stage.transform);
                }
            }
        }

        // Spawn inner nondestructiblewalls
        for (int i = 1; i < stageLength; i++)
        {
            if (i % 2 == 0)
                for (int j = 1; j < stageWidth; j++)
                {
                    Vector3 vect = new Vector3(i, 0f, j);
                    if (j % 2 == 0)
                    {
                        stageLayout[i,j] = 5;
                        Instantiate(nonDestructibleWall, vect, transform.rotation, stage.transform);
                    }
                }
        }
        
        // Hardcoded Players spawn locations
        stageLayout[1, 1] = 1;
        stageLayout[1, 2] = 1;
        stageLayout[2, 1] = 1;
        
        stageLayout[stageLength-2, stageWidth-2] = 2;
        stageLayout[stageLength-3 , stageWidth-2] = 2;
        stageLayout[stageLength-2, stageWidth-3] = 2;
        
        stageLayout[1, stageWidth-2] = 3;
        stageLayout[2, stageWidth-2] = 3;
        stageLayout[1, stageWidth-3] = 3;
        
        stageLayout[stageLength-2, 1] = 4;
        stageLayout[stageLength-2, 2] = 4;
        stageLayout[stageLength-3, 1] = 4;
        
        //Spawn destructible walls
        for (int i = 1; i < stageLength; i++)
        {
            for (int j = 1; j < stageWidth; j++)
            {
                if (stageLayout[i,j] == 0)
                {
                    var random = Random.Range(0, 100);
                    Vector3 vect = new Vector3(i, 0f, j);
                    if (random < wallChanceToSpawn)
                    {
                        Instantiate(wall, vect, transform.rotation, stage.transform);
                    }
                }
            }
        }
        onStageCreation?.Invoke(stageLength, stageWidth);
    }
}