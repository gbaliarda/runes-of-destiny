using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CinemachineCamera : MonoBehaviour, ICamera
{
    private CinemachineFramingTransposer _virtualCamera;
    private int _currentStep = 0;

    private int[] _rotationSteps = { 60, 50, 40, 30, 25, 20};
    private int[] _cameraDistances = { 20, 17, 13, 10, 8, 6};
    void Start()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>()?.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    void Update()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput > 0f) ZoomIn();
        else if (scrollInput < 0f) ZoomOut();

        UpdateZoomAndDistance();

    }

    private void UpdateZoomAndDistance()
    {
        Quaternion desiredRotation = Quaternion.Euler(new Vector3(_rotationSteps[_currentStep], 0, 0));

        Quaternion smoothedRotation = Quaternion.Lerp(transform.rotation, desiredRotation, 5f * Time.deltaTime);
        transform.rotation = smoothedRotation;

        int desiredCameraDistance = _cameraDistances[_currentStep];
        float smoothedCameraDistance = Mathf.Lerp(_virtualCamera.m_CameraDistance, desiredCameraDistance, 5f * Time.deltaTime);
        _virtualCamera.m_CameraDistance = smoothedCameraDistance;
    }

    public void MoveToTarget()
    {
        throw new System.NotImplementedException();
    }

    public void ZoomIn()
    {
        if (_currentStep < _rotationSteps.Length - 1) _currentStep++;
    }

    public void ZoomOut()
    {
        if (_currentStep > 0) _currentStep--;
    }

}
