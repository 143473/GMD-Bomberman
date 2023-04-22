using Interfaces;
using UnityEngine;

public class OnBombDestroy : MonoBehaviour, IDamage
{
    void Start()
    {
        
    }
    public void OnDamage()
    {
        gameObject.GetComponent<BombScript>().Explode();
    }
}