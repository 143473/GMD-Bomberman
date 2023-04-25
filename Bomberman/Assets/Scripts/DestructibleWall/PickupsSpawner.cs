using UnityEngine;
using Random = UnityEngine.Random;

public class PickupsSpawner : MonoBehaviour
{
    [SerializeField] protected float powerUpChanceToSpawn = 70;
    
    [SerializeField] protected GameObject curse;
    [SerializeField] protected GameObject powerUp;
    
    public void SpawnPickUp()
    {
        var randomNumber = Random.Range(0, 100);
        if (randomNumber < powerUpChanceToSpawn)
            Instantiate(powerUp, transform.position, Quaternion.identity);
        else
            Instantiate(curse, transform.position, Quaternion.identity);
    }
}