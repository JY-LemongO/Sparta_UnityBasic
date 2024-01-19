using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum PlayerState { Idle, Move, Interact, UI }

public class Player : MonoBehaviour
{
    [SerializeField] RuntimeAnimatorController[] _controllers;

    private SpriteRenderer   _rend;
    private Rigidbody2D      _rigid;
    private Animator         _animator;
    private InputManager     _inputManager;
    private TextMeshProUGUI  _nameText;

    private NPC _npc = null;

    public PlayerState State { get; private set; }

    [SerializeField] Vector3 _moveVect;
    [SerializeField] float   _moveSpeed;
    [SerializeField] string  _name;    

    private void Update()
    {
        if(Camera.main.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x)
            _rend.flipX = true;
        else
            _rend.flipX = false;        
    }

    public void Setup(string name, int index)
    {        
        _rend           = GetComponent<SpriteRenderer>();
        _rigid          = GetComponent<Rigidbody2D>();
        _animator       = GetComponent<Animator>();
        _inputManager   = GetComponent<InputManager>();
        _nameText       = GetComponentInChildren<TextMeshProUGUI>();

        ChangeName(name);
        ChangeAnimator(index);

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
        if (_animator.parameterCount != 0)
            _animator.SetFloat("Speed", move);

        Vector2 currentPos = transform.position;
        Vector2 nextPos = _moveVect.normalized * _moveSpeed * Time.fixedDeltaTime;
        _rigid.MovePosition(currentPos + nextPos);
    }

    public void ChangeName(string name)
    {
        _name = name;
        _nameText.text = _name;
    }

    public void ChangeAnimator(int index)
    {
        _animator.runtimeAnimatorController = _controllers[index];        
    }

    public void ChangeState(PlayerState state) => State = state;
}
