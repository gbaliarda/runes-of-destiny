using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour, ICamera
{
    [SerializeField] private Transform _player;
    [SerializeField] private UnityEngine.Camera _minimapCamera;

    private int _currentZoom = 2;
    private float[] _zoomValues = { 6, 10, 15, 20, 25 };
    
    void Start()
    {
        if (_minimapCamera == null) _minimapCamera = GameObject.FindGameObjectWithTag("MinimapCamera").GetComponent<UnityEngine.Camera>();
    }

    void LateUpdate()
    {
        MoveToTarget();
        UpdateCameraZoom();
    }
    public void MoveToTarget()
    {
        Vector3 newPosition = _player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;
    }

    public void ZoomIn()
    {
        if (_currentZoom > 0)
        {
            _currentZoom--;
            UpdateCameraZoom();
        }
    }

    public void ZoomOut()
    {
        if (_zoomValues.Length - 1 > _currentZoom)
        {
            _currentZoom++;
            UpdateCameraZoom();
        }
    }

    private void UpdateCameraZoom()
    {
        float desiredValue = _zoomValues[_currentZoom];

        float smoothedOrthographicSize = Mathf.Lerp(_minimapCamera.orthographicSize, desiredValue, 5f * Time.deltaTime);
        _minimapCamera.orthographicSize = smoothedOrthographicSize;
    }
}
