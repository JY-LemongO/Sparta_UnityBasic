using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public delegate void OnMovePlayer();

    public event OnMovePlayer _onMovePlayer;

    private void FixedUpdate()
    {        
        _onMovePlayer?.Invoke();
    }
}
