using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Player _playerController;

    private void Start()
    {        
        _playerController = GameManager.Player;
    }

    private void LateUpdate()
    {
        transform.position = _playerController.transform.position - Vector3.forward * 10;
    }
}
