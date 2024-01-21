using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NameUI : MonoBehaviour
{
    [SerializeField] TMP_InputField _nameInput;
    [SerializeField] Button _applyButton;
    [SerializeField] TextMeshProUGUI _noticeText;
    
    private bool _isAlerting = false;

    private void Start()
    {
        _nameInput.onEndEdit.AddListener(OnInputField);
        _applyButton.onClick.AddListener(OnApplyBtn);
    }

    // 하이라이트 false 돼면 호출 X 버튼은 O

    private void OnApplyBtn()
    {
        string name = _nameInput.text;
        EnterName(name, true);
    }

    private void OnInputField(string value)
    {
        EnterName(value);
    }

    private void EnterName(string value, bool isBtn = false)
    {        
        if (!Input.GetKeyDown(KeyCode.Return) && !isBtn)
            return;

        if (value.Length >= 2 && value.Length <= 8)
        {            
            if (SceneManager.GetActiveScene().name == "StartScene")
                GameManager.Instance.GoMainScene(value);
            else
            {
                GameManager.Player.ChangeName(value);
                _nameInput.text = "";
                gameObject.SetActive(false);
            }                
        }            
        else
            StartCoroutine(NoticeTextRoutine());
    }

    private IEnumerator NoticeTextRoutine()
    {
        if (_isAlerting)
            yield break;

        _isAlerting = true;
        _noticeText.gameObject.SetActive(true);
        _nameInput.text = "";

        yield return new WaitForSeconds(1);

        _noticeText.gameObject.SetActive(false);
        _isAlerting = false;
    }
}
