using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class BombermanCharacterController : MonoBehaviour
{
    private CharacterController controller;
    //public float speed = 6f;
    private float turnSmoothVelocity;

    public UnityEvent onPlaceBomb;
    public delegate void OnManuallyExplodeBomb(string name);
    public static OnManuallyExplodeBomb onManuallyExplodeBomb;
    private BombermanStats bombermanStats;
    private Vector2 movementInput = Vector2.zero;

    public float turnSmoothTime = 0.1f;

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
        // float horizontal = Input.GetAxis("Horizontal");
        // float vertical = Input.GetAxis("Vertical");
        // Vector3 direction = new Vector3(horizontal, -1f, vertical).normalized;
        Vector3 direction = new Vector3(movementInput.x, -1f, movementInput.y).normalized;

       
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        
		//if (horizontal != 0.0f || vertical != 0.0)
        if (movementInput.x != 0.0f || movementInput.y != 0.0)
		{
			transform.rotation = Quaternion.Euler(0f, angle, 0f);
		}
        controller.Move(direction * gameObject.GetComponent<BombermanStats>().Speed * Time.deltaTime);
    }
}
