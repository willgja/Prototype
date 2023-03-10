using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IslandRotate : MonoBehaviour
{
    public PlayerController _player;
    private Transform _transform;
    private bool isRotation = false;
    public float rotateSpeed = 1f;
    public GameObject playerParent;

    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        if (_player.transform.position.x <= -13f && !isRotation)
        {
            playerParent.transform.SetParent(transform);
           // playerParent.transform.localRotation = Quaternion.identity;

            _transform.DORotate(_transform.localRotation.eulerAngles + new Vector3(0, 0, -90), 1f);
            if (transform.rotation.z <= -90)
            {
                _player._transform.DORotate(_player._transform.localRotation.eulerAngles + new Vector3(0, 0, 90), rotateSpeed);
            }

            isRotation = true;
           
        }
        else if (_player.transform.position.x >= 13f && isRotation)
        {
            _transform.DORotate(_transform.localRotation.eulerAngles + new Vector3(0, 0, 90), 1f);
           // transform.DORotate(_player._transform.localRotation.eulerAngles + new Vector3(0, 0, 90), rotateSpeed);
            isRotation = false;
        }
    }
}
