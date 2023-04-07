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
            _transform.DORotate(_transform.localRotation.eulerAngles + new Vector3(0, 0, -90), 1f);

            isRotation = true;
           
        }
        else if (_player.transform.position.x >= 13f && isRotation)
        {
            _transform.DORotate(_transform.localRotation.eulerAngles + new Vector3(0, 0, 90), 1f);
           
            isRotation = false;
        }
    }
}
