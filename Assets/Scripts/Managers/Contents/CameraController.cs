using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform _playerTransform;
    [SerializeField] Vector2 _borderLeftDown;
    [SerializeField] Vector2 _borderRightUp;
    [SerializeField] float _cameraSpeed;
    private void Start()
    {
        _playerTransform = GameManager.Player.transform;
    }

    private void FixedUpdate()
    {        
        transform.position = Vector3.Lerp(transform.position, _playerTransform.position + Vector3.forward * -10, Time.deltaTime * _cameraSpeed);

        float limitX = Mathf.Clamp(transform.position.x, _borderLeftDown.x, _borderRightUp.x);
        float limitY = Mathf.Clamp(transform.position.y, _borderLeftDown.y, _borderRightUp.y);

        transform.position = new Vector3(limitX, limitY, -10);
    }
}
