using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Utils;

public class BombermanCharacterController : MonoBehaviour
{
    private CharacterController controller;
    private float turnSmoothVelocity;
    public UnityEvent onPlaceBomb;
    public delegate void OnManuallyExplodeBomb(string name);
    public static OnManuallyExplodeBomb onManuallyExplodeBomb;

    public delegate void OnWalk(bool isWalking);
    public static OnWalk onWalk;
    
    //private BombermanStats bombermanStats;
    private FinalBombermanStatsV2 bombermanStats;
    private Vector2 movementInput = Vector2.zero;
    private Vector3 direction = Vector3.zero;

    public float turnSmoothTime = 0.1f;
    
    void Start()
    {
        //bombermanStats = gameObject.GetComponent<BombermanStats>();
        bombermanStats = gameObject.GetComponent<FinalBombermanStatsV2>();
        controller = gameObject.GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnMovement(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }

    public void OnPlaceBomb(InputValue value)
    {
        onPlaceBomb.Invoke();
    }

    public void OnExplodeBomb(InputValue value)
    {
        if (bombermanStats.GetBooleanStat(Stats.RemoteExplosion))
        {
            onManuallyExplodeBomb?.Invoke(name);
        }
    }
    
    void Update()
    {
        if(!bombermanStats.GetBooleanStat(Stats.InverseControls)) 
            direction = new Vector3(movementInput.x, -1f, movementInput.y).normalized;
        else
            direction = new Vector3(-movementInput.x, -1f, -movementInput.y).normalized;
        
        float targetAngle = Mathf.Atan2(-direction.x, -direction.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        
        if (movementInput.x != 0.0f || movementInput.y != 0.0)
		{
			transform.rotation = Quaternion.Euler(0f, angle, 0f);
            onWalk?.Invoke(true);
		}
        else
        {
            onWalk?.Invoke(false);
        }
        controller.Move(direction * (bombermanStats.GetNumericStat(Stats.Speed) * Time.deltaTime));
    }
}
