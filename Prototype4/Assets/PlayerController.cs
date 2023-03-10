using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 6.0f;
    public float maxSpeed = 10.0f;
    public float rotateSpeed = 90.0f;

    private CharacterController controller;
    private Vector3 movement;

    public Transform _transform;
    public bool isRotating = false;
    private bool isFront = false;

   

    void Start()
    {
        controller = GetComponent<CharacterController>();
        _transform = GetComponent<Transform>();

        
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontal, 0.0f, vertical).normalized;

        if (moveDirection.magnitude > 0.0f)
        {
            movement = moveDirection * moveSpeed;
            movement = Vector3.ClampMagnitude(movement, maxSpeed);
        }
        else
        {
            movement = Vector3.zero;
        }

        movement.y = 0.0f;

        

        if (transform.position.z <= -12f && !isFront)
        {
            transform.DORotate(_transform.localRotation.eulerAngles + new Vector3(90, 0, 0), rotateSpeed);
            isFront = true;
        }

        controller.Move(movement * Time.deltaTime);
    }
}
