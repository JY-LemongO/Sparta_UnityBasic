using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCTalk : MonoBehaviour
{
    private Player _player;

    [Header("# Other")]
    [SerializeField] GameObject _blinder;

    [Header("# Images")]
    [SerializeField] Image _npcImage;
    [SerializeField] Image _playerImage;

    [Header("# Dialogue")]
    [SerializeField] GameObject         _dialoguePanel;
    [SerializeField] Button             _talkStartBtn;
    [SerializeField] TextMeshProUGUI    _talkText;
    [SerializeField] Dialogue           _dialogue;

    private int _dialogueIndex = 0;

    private void Start()
    {
        _player = GameManager.Player;
        _talkStartBtn.onClick.AddListener(Talk);
    }

    private void Update()
    {
        NextScript();
    }

    public void AppearTalk(bool isOn) => gameObject.SetActive(isOn);

    public void Talk()
    {
        _blinder.SetActive(true);
        _talkStartBtn.gameObject.SetActive(false);

        _player.ChangeState(PlayerState.Talk);
        _playerImage.sprite = _player.P_Sprite;
        _npcImage.sprite = _player.Npc.GetNpcSprite();

        _dialogueIndex = 0;
        _talkText.text = _dialogue.scripts[_dialogueIndex];
        _dialoguePanel.SetActive(true);
    }

    public void NextScript()
    {
        if(_player.State == PlayerState.Talk && Input.GetKeyDown(KeyCode.Space))
        {
            _dialogueIndex++;

            if (_dialogueIndex == _dialogue.scripts.Length)
            {
                GameManager.Player.ChangeState(PlayerState.Idle);

                _talkStartBtn.gameObject.SetActive(true);
                _dialoguePanel.SetActive(false);
                _blinder.SetActive(false);                
            }
            else
                _talkText.text = _dialogue.scripts[_dialogueIndex];
        }
    }
}
