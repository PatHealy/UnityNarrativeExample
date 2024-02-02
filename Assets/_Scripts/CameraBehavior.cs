using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraBehavior : MonoBehaviour
{
    private CinemachineVirtualCamera _camera;
    public static CameraBehavior instance;

    private void Awake()
    {
        instance = this;
        _camera = GetComponent<CinemachineVirtualCamera>();
        RestoreFollowPlayer();
    }

    public void FocusCamera(Transform target, Vector3 position)
    {
        _camera.enabled = true;
        _camera.LookAt = target;
        transform.position = position;
    }

    public void RestoreFollowPlayer()
    {
        _camera.enabled = false;
    }
}
