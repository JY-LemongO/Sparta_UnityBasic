using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum PlayerState { Idle, Move, Interact }

public class Player : MonoBehaviour
{
    private SpriteRenderer   _rend;
    private Rigidbody2D      _rigid;
    private Animator         _animator;
    private InputManager     _inputManager;
    private TextMeshProUGUI  _nameText;

    public PlayerState State { get; private set; }

    [SerializeField] Vector3 _moveVect;
    [SerializeField] float   _moveSpeed;
    [SerializeField] string  _name;    

    private void Update()
    {
        if(Camera.main.ScreenToViewportPoint(Input.mousePosition).x < 0.5f)
            _rend.flipX = true;
        else
            _rend.flipX = false;        
    }

    public void Setup(string name)
    {
        _name           = name;
        _rend           = GetComponent<SpriteRenderer>();
        _rigid          = GetComponent<Rigidbody2D>();
        _animator       = GetComponent<Animator>();
        _inputManager   = GetComponent<InputManager>();
        _nameText       = GetComponentInChildren<TextMeshProUGUI>();
        _nameText.text  = _name;

        _inputManager._onMovePlayer -= PlayerMove;
        _inputManager._onMovePlayer += PlayerMove;
    }

    private void PlayerMove()
    {
        if (State != PlayerState.Move)
            State = PlayerState.Move;

        _moveVect.x = Input.GetAxisRaw("Horizontal");
        _moveVect.y = Input.GetAxisRaw("Vertical");

        float move = Mathf.Abs(_moveVect.x) + Mathf.Abs(_moveVect.y);
        _animator.SetFloat("Speed", move);

        Vector2 currentPos = transform.position;
        Vector2 nextPos = _moveVect * _moveSpeed * Time.fixedDeltaTime;
        _rigid.MovePosition(currentPos + nextPos);
    }
}
