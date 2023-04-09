using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


public class CameraShake : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private Vector3 _positionStrength;
    [SerializeField] private Vector3 _rotationStrength;

    private static event Action Shake;

    public static void Invoke()
    {
        Shake?.Invoke();
    }

    private void OnEnable() => Shake += CameraShaker;
    private void OnDestroy() => Shake += CameraShaker;
  

    private void CameraShaker()
    {
        _camera.DOComplete();
        _camera.DOShakePosition(0.5f, _positionStrength);
        _camera.DOShakeRotation(0.5f, _rotationStrength);
    }
}
