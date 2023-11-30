using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FollowCanvas : MonoBehaviour
{
    [SerializeField] private GameObject _lookAt;
    [SerializeField] private Vector3 _offset;

    [SerializeField] private Camera _camera;


    void Start()
    {
        _camera = Camera.main;
    }


    void Update()
    {
        Vector3 position = _camera.WorldToScreenPoint(_lookAt.transform.position + _offset);

        if (transform.position != position) transform.position = position;
    }

    public void SetLookAt(GameObject lookAt)
    {
        _lookAt = lookAt;
    }

    public void DestroyFollow()
    {
        Destroy(_lookAt);
        Destroy(this);
    }
}
