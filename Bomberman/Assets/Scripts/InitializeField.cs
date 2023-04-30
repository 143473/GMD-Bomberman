using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class InitializeField : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject wall;
    [SerializeField]
    private  GameObject bombermanBlue;
    [SerializeField]
    private  GameObject bombermanRed;
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
        PlaceWalls();
    }
    
    void PlaceWalls()
    {
        for (int x = -10; x <= 10; x++)
        {
            for (int z = -5; z <= 5; z++)
            {
                if (Random.Range(0, 100) < 50)
                {
                    continue;
                }
                
                //no walls in stone walls
                if ((x + 10) % 2 == 1 && (z + 5) % 2 == 1)
                {
                    continue;
                }
                
                //no walls at corners
                if (x is <= -9 or >= 9 && z is <= -4 or >= 4)
                {
                    continue;
                }
                
                var vect = new Vector3(x, 0, z);
                Instantiate(wall, vect, transform.rotation, destroyableWalls.transform);
            }
        }
    }
    
    void PlaceBomberman()
    {
        var p1 = PlayerInput.Instantiate(bombermanRed,
            controlScheme: "Keyboard.Arrows", pairWithDevice: Keyboard.current);

        p1.transform.position = new Vector3(-10,0,-5);
        p1.transform.rotation = Quaternion.LookRotation(Vector3.back);
        p1.name = "Player 1";
        
        var p2 = PlayerInput.Instantiate(bombermanBlue,
            controlScheme: "Keyboard.WASD", pairWithDevice: Keyboard.current);

        p2.transform.position = new Vector3(10,0,5);
        p2.name = "Player 2";
    }
}
