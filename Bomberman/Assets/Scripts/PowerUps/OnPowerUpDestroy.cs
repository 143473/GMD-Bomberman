using Interfaces;
using UnityEngine;

public class OnPowerUpDestroy : MonoBehaviour, IDamage
{
    void Start()
    {
        
    }
    public void OnDamage()
    {
        Destroy(gameObject);
    }
}