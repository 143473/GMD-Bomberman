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
        {
            var container = Instantiate(powerUp);
            container.transform.position = transform.position;
        }
        else
        {
            var container = Instantiate(curse);
            container.transform.position = transform.position;
        }
    }
}