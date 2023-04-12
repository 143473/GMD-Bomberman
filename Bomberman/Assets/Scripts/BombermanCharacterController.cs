using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombermanCharacterController : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 6f;
    private float turnSmoothVelocity;

    public float turnSmoothTime = 0.1f;

    void Start()
    { 
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, -1f, vertical).normalized;

       
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        
		if (horizontal != 0.0f || vertical != 0.0)
		{
			transform.rotation = Quaternion.Euler(0f, angle, 0f);
		}
        controller.Move(direction * speed * Time.deltaTime);
    }
}
