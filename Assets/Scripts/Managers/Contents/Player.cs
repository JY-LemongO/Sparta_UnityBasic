using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState { Idle, Move, Interact }

public class Player : MonoBehaviour
{
    private SpriteRenderer _rend;
    private Rigidbody2D _rigid;
    private InputManager _inputManager;

    [SerializeField] Vector2 _moveVect;
    [SerializeField] float _moveSpeed;

    private void Awake()
    {
        GameManager.Instance.PlayerSetup(this);
    }

    private void Update()
    {
        if(Camera.main.ScreenToViewportPoint(Input.mousePosition).x < 0.5f)
            _rend.flipX = true;
        else
            _rend.flipX = false;        
    }

    public void Setup()
    {
        _rend = GetComponent<SpriteRenderer>();
        _rigid = GetComponent<Rigidbody2D>();
        _inputManager = GetComponent<InputManager>();

        _inputManager.onMovePlayer -= PlayerMove;
        _inputManager.onMovePlayer += PlayerMove;
    }

    private void PlayerMove()
    {
        _moveVect.x = Input.GetAxisRaw("Horizontal");
        _moveVect.y = Input.GetAxisRaw("Vertical");

        Vector2 currentPos = transform.position;
        Vector2 nextPos = (currentPos + _moveVect) * _moveSpeed * Time.fixedDeltaTime;

        _rigid.MovePosition(nextPos);
    }
}
