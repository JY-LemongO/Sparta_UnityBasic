using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum PlayerState { Idle, Move, Talk, UI }

public class Player : MonoBehaviour
{
    [SerializeField] RuntimeAnimatorController[] _controllers;
    [SerializeField] Sprite[] _sprites;

    private SpriteRenderer   _rend;
    private Rigidbody2D      _rigid;
    private Animator         _animator;
    private InputManager     _inputManager;
    private TextMeshProUGUI  _nameText;
    private PersonCounter    _personCounter;

    public Sprite P_Sprite { get; private set; }
    public NPC Npc { get; private set; }

    public PlayerState State { get; private set; }

    [SerializeField] Vector3 _moveVect;
    [SerializeField] float   _moveSpeed;    

    private void Update()
    {
        if(Camera.main.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x)
            _rend.flipX = true;
        else
            _rend.flipX = false;        
    }

    public void SetCurrentNpc(NPC npc)
    {
        Npc = npc;
    }

    public void Setup(string name, int index)
    {        
        _rend           = GetComponent<SpriteRenderer>();
        _rigid          = GetComponent<Rigidbody2D>();
        _animator       = GetComponent<Animator>();
        _inputManager   = GetComponent<InputManager>();
        _personCounter  = GetComponent<PersonCounter>();
        _nameText       = GetComponentInChildren<TextMeshProUGUI>();        

        ChangeName(name);
        ChangeAnimator(index);        
        
        _inputManager._onMovePlayer += PlayerMove;        
    }

    private void PlayerMove()
    {
        if (State == PlayerState.Talk || State == PlayerState.UI)
            return;

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
        _personCounter.Name = name;
        _nameText.text = name;        
    }

    public void ChangeAnimator(int index)
    {
        _animator.runtimeAnimatorController = _controllers[index];
        P_Sprite = _sprites[index];
    }

    public void ChangeState(PlayerState state) => State = state;
}
