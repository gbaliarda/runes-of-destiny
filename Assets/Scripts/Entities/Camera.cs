using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour, ICamera
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _smoothSpeed = 5f;
    [SerializeField] private Vector3 _offset;

    #region ZOOM

    private int _zoomStep = 0;

    private Vector3[] _rotationSteps = {
        new Vector3(60f, 0f, 0f),
        new Vector3(50f, 0f, 0f),
        new Vector3(40f, 0f, 0f),
        new Vector3(30f, 0f, 0f),
        new Vector3(25f, 0f, 0f),
        new Vector3(20f, 0f, 0f)
    };

    private Vector3[] _offsetSteps = {
        new Vector3(0f, 20f, -12f),
        new Vector3(0f, 15f, -10f),
        new Vector3(0f, 10f, -8f),
        new Vector3(0f, 7f, -8f),
        new Vector3(0f, 6f, -7f),
        new Vector3(0f, 5f, -7f)
    };

    #endregion

    private Vector3 desiredPosition;
    private Quaternion desiredRotation;

    void Start()
    {
        UpdateCameraPositionAndRotation();
    }

    void Update()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput > 0f) ZoomIn();
        else if (scrollInput < 0f) ZoomOut();

        if (_target != null) MoveToTarget();
    }

    public void MoveToTarget()
    {
        desiredPosition = new Vector3(_target.position.x + _offset.x, _target.position.y + _offset.y, _target.position.z + _offset.z);

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        Quaternion smoothedRotation = Quaternion.Slerp(transform.rotation, desiredRotation, _smoothSpeed * Time.deltaTime);
        transform.rotation = smoothedRotation;
    }

    public void ZoomIn()
    {
        if (_zoomStep < _rotationSteps.Length - 1)
        {
            _zoomStep++;
            UpdateCameraPositionAndRotation();
        }
    }

    public void ZoomOut()
    {
        if (_zoomStep > 0)
        {
            _zoomStep--;
            UpdateCameraPositionAndRotation();
        }
    }

    private void UpdateCameraPositionAndRotation()
    {
        _offset = _offsetSteps[_zoomStep];
        desiredRotation = Quaternion.Euler(_rotationSteps[_zoomStep]);
    }
}
