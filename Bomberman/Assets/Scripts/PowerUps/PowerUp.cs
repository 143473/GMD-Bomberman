using System.Linq;
using PowerUps.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

public class PowerUp : MonoBehaviour
{
    [SerializeField] 
    private float availabilityInSeconds = 10;
    private static GameObject[] _powerUps;
    // Start is called before the first frame update
    void Start()
    {
        var chanceToAddToList = Random.Range(0, 100);
        _powerUps = Resources.LoadAll<GameObject>("PowerUps");
        _powerUps = _powerUps.SkipWhile(a =>
        {
            var skipped = false;
            if (a.TryGetComponent(out IPowerUp powerUpScript))
                skipped = chanceToAddToList > powerUpScript.ChanceToSpawn();
            return skipped;
        }).ToArray();
        SpawnRandomPowerUp();
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, availabilityInSeconds);
    }

    void SpawnRandomPowerUp()
    {
        var powerUpNumber = Random.Range(0, _powerUps.Length);
        Instantiate(_powerUps[powerUpNumber], gameObject.transform);
    }
}
