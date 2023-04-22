using Interfaces;
using UnityEngine;

public class OnBombermanDestroy : MonoBehaviour, IDamage
{
    void Start()
    {
    }

    public void OnDamage()
    {
        gameObject.SetActive(false);
        gameObject.GetComponent<BombermanStats>().Lives--;
    }
}