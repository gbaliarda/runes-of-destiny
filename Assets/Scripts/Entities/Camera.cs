using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.OnScreen;

public class Camera : MonoBehaviour, ICamera
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _smoothSpeed;
    [SerializeField] private Vector3 _offset;

    [SerializeField] private KeyCode _zoomIn;
    [SerializeField] private KeyCode _zoomOut;

    void Update()
    {
        Debug.Log($"axis: {Input.GetAxis("Mouse ScrollWheel")}");

        if (_target != null) MoveToTarget();
    }

    public void MoveToTarget()
    {
        Vector3 desiredPosition = new Vector3(_target.position.x + _offset.x, _target.position.y + _offset.y, _target.position.z + _offset.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }

    public void ZoomIn()
    {
        
    }

    public void ZoomOut()
    {

    }
}
