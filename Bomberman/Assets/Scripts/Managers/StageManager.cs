using Helpers;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


public class StageManager : MonoBehaviour
{
    [SerializeField] private GameObject field;
    [SerializeField] private int wallChanceToSpawn;
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject nonDestructibleWall;
    [SerializeField] private GameObject frame;
    [SerializeField] private int stageLength = 15;
    [SerializeField] private int stageWidth = 15;
    private GameObject instatiatedWall;
    private GameObject instatiatedStone;
    private PathfindingAStar x;

    private int[,] stageLayout;
    public delegate void OnStageCreation(int stageLength, int stageWidth);
    public static OnStageCreation onStageCreation;
    
    public delegate void OnGridSet(Gridx grid);
    public static OnGridSet onGridSet;

    private GameObject stage;
    private Gridx gridx;

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

    private void Update()
    {
        // if (Input.GetKeyDown("space"))
        // {
        //     var vectWorld = GameObject.FindGameObjectsWithTag("Player").First(a =>a.gameObject.name == "Player 1").transform.position;
        //     var vect2World = GameObject.FindGameObjectsWithTag("Player").First(a =>a.gameObject.name == "Player 2").transform.position;
        //
        //     gridx.GetXY(vectWorld, out int vectx, out int vecty);
        //     gridx.GetXY(vect2World, out int vect2x, out int vect2y);
        //
        //     var y = x.GetPath((vectx, vecty), (vect2x, vect2y));
        //     foreach (var tile in y)
        //     {
        //         Debug.Log($"PATH ----- {tile.X} : {tile.Y}");
        //     }
        // }
    }

    void PlaceField()
    {
        // Scaling the field renders it black ....
        field.transform.localScale = new Vector3(stageLength / 10f, 5, stageWidth / 10f);
        field.transform.position = new Vector3((stageLength-1) / 2f, -0.5f, (stageWidth-1) / 2f);
        Instantiate(frame, new Vector3(10,-1,5), Quaternion.identity);
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
                    instatiatedStone = Instantiate(nonDestructibleWall, vect, transform.rotation, stage.transform);
                    var render = instatiatedStone.GetComponentInChildren<Renderer>(true);
                    render.material.color = StageHelper.StoneGradient();
                    instatiatedStone.transform.localScale= new Vector3(1,StageHelper.StoneWallHeight(), 1);
                }

            for (int j = 0; j < stageWidth; j++)
            {
                if (j == 0 || j == stageWidth-1)
                {
                    Vector3 vect = new Vector3(i, 0f, j);
                    stageLayout[i,j] = 5; 
                    instatiatedStone = Instantiate(nonDestructibleWall, vect, transform.rotation, stage.transform);
                    var render = instatiatedStone.GetComponentInChildren<Renderer>(true);
                    render.material.color = StageHelper.StoneGradient();
                    instatiatedStone.transform.localScale= new Vector3(1,StageHelper.StoneWallHeight(), 1);
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
                       instatiatedStone = Instantiate(nonDestructibleWall, vect, transform.rotation, stage.transform);
                       var render = instatiatedStone.GetComponentInChildren<Renderer>(true);
                       render.material.color = StageHelper.StoneGradient();
                       instatiatedStone.transform.localScale= new Vector3(1,StageHelper.StoneWallHeight(), 1);
                    }
                }
        }
        
        // Hardcoded Players spawn locations
        stageLayout[1, 1] = 1;
        stageLayout[1, 2] = 1;
        stageLayout[2, 1] = 1;
        
        stageLayout[stageLength-2, stageWidth-2] = 1;
        stageLayout[stageLength-3 , stageWidth-2] = 1;
        stageLayout[stageLength-2, stageWidth-3] = 1;
        
        stageLayout[1, stageWidth-2] = 1;
        stageLayout[2, stageWidth-2] = 1;
        stageLayout[1, stageWidth-3] = 1;
        
        stageLayout[stageLength-2, 1] = 1;
        stageLayout[stageLength-2, 2] = 1;
        stageLayout[stageLength-3, 1] = 1;
        
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
                        stageLayout[i,j] = 2;
                        instatiatedWall = Instantiate(wall, vect, transform.rotation, stage.transform);
                        var render = instatiatedWall.GetComponentInChildren<Renderer>(true);
                        render.material.color = StageHelper.BrickGradient();
                        instatiatedWall.transform.localScale= new Vector3(1,StageHelper.BrickWallHeight(), 1);
                    }
                }
            }
        }
        gridx = new Gridx(stageLayout);
        x = new PathfindingAStar(gridx);
        
        onStageCreation?.Invoke(stageLength, stageWidth);
        onGridSet?.Invoke(gridx);
    }
}