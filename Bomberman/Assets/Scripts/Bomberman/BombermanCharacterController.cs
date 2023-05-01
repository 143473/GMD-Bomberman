using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class BombermanCharacterController : MonoBehaviour
{
    private CharacterController controller;
    private float turnSmoothVelocity;
    public UnityEvent onPlaceBomb;
    public delegate void OnManuallyExplodeBomb(string name);
    public static OnManuallyExplodeBomb onManuallyExplodeBomb;
    private BombermanStats bombermanStats;
    private Vector2 movementInput = Vector2.zero;
    public float turnSmoothTime = 0.1f;
    public bool isWalking { get; set; }

    void Start()
    {
        bombermanStats = gameObject.GetComponent<BombermanStats>();
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
        if (bombermanStats.RemoteExplosion)
        {
            onManuallyExplodeBomb?.Invoke(name);
        }
    }
    
    void Update()
    {
        Vector3 direction = new Vector3(movementInput.x, -1f, movementInput.y).normalized;
        
        if (movementInput.x != 0.0f || movementInput.y != 0.0)
		{
            float targetAngle = Mathf.Atan2(-direction.x, -direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
        
        controller.Move(direction * (bombermanStats.Speed * Time.deltaTime));
    }
}
