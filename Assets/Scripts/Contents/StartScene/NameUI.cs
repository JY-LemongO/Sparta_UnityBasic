using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NameUI : MonoBehaviour
{
    [SerializeField] TMP_InputField _nameInput;
    [SerializeField] Button _applyButton;
    [SerializeField] TextMeshProUGUI _noticeText;

    private bool _isAlerting = false;

    private void Start()
    {
        _nameInput.onEndEdit.AddListener(EnterName);
        _applyButton.onClick.AddListener(OnApplyBtn);
    }

    private void OnApplyBtn()
    {
        string inputValue = _nameInput.text;        
        EnterName(inputValue);
    }

    private void EnterName(string value)
    {
        if (value.Length >= 2 && value.Length <= 8)
            GameManager.Instance.GoMainScene(value);
        else
            StartCoroutine(NoticeTextRoutine());
    }

    private IEnumerator NoticeTextRoutine()
    {
        if (_isAlerting)
            yield break;

        _isAlerting = true;
        _noticeText.gameObject.SetActive(true);

        yield return new WaitForSeconds(1);

        _noticeText.gameObject.SetActive(false);
        _isAlerting = false;
    }
}
