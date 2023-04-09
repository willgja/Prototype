using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    private Transform aviaoTransform;
    public float speed = 10.0f;

    Gun[] guns;
    bool shoot;

    private void Start()
    {
        aviaoTransform = GetComponent<Transform>();

        guns = transform.GetComponentsInChildren<Gun>();
    }

    public void Move(Vector3 direction, float speed)
    {
        aviaoTransform.Translate(direction * speed * Time.deltaTime);
    }

    public void StopMovement()
    {
        aviaoTransform.Translate(Vector3.zero);
    }

    private void Update()
    {
        ShootGuns();

        MovementPlayer();
        
    }

    void ShootGuns()
    {
        shoot = Input.GetKeyDown(KeyCode.LeftControl);
        if (shoot)
        {
            foreach(Gun gun in guns)
            {
                gun.Shoot();
            }
        }

    }

    void MovementPlayer()
    {
        if (Input.GetKey(KeyCode.UpArrow) && transform.position.z <= 12f)
        {
            Move(Vector3.forward, speed);
        }
        else if (Input.GetKey(KeyCode.DownArrow) && transform.position.z >= -12f)
        {
            Move(Vector3.back, speed);
        }

        if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x >= -13f)
        {
            Move(Vector3.left, speed);
        }
        else if (Input.GetKey(KeyCode.RightArrow) && transform.position.x <= 13f)
        {
            Move(Vector3.right, speed);
        }

        if (!Input.anyKey)
        {
            StopMovement();
        }
    }
}




