using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _nowTimeText;
    [SerializeField] TextMeshProUGUI _participantsCountText;
    [SerializeField] TextMeshProUGUI _participantsNameText;

    [SerializeField] GameObject _participantListPanel;
    [SerializeField] GameObject _mainMenuPanel;

    [SerializeField] GameObject _manualChanger;
    [SerializeField] GameObject _nameChanger;    
    
    private bool isPanelOpen = false;

    public static bool IsChangerOpen = false;

    private void Awake()
    {
        GameManager.Social.OnCountPeople += UpdateParticipants;        
    }

    private void Update()
    {
        if (_nowTimeText != null)
            _nowTimeText.text = DateTime.Now.ToString("HH:mm");
    }

    private void UpdateParticipants(int value, List<PersonCounter> people)
    {
        _participantsCountText.text = $"현재 참가자 : {value}";

        string names = "";
        foreach (PersonCounter person in people)
            names += $"{person.Name}\n";

        _participantsNameText.text = names;
    }

    public void OnOffCharacterChanger()
    {
        if (_manualChanger.activeSelf)
            UIOnOff(_manualChanger, false);
        else
            UIOnOff(_manualChanger, true); ;
    }

    public void OnOffNameChanger()
    {
        if (_nameChanger.activeSelf)
            UIOnOff(_nameChanger, false);
        else
            UIOnOff(_nameChanger, true);
    }

    private void UIOnOff(GameObject obj, bool isOn = false)
    {
        if (IsChangerOpen && isOn == true)
            return;

        obj.SetActive(isOn);
        IsChangerOpen = isOn;

        if (!isOn)
            GameManager.Player.ChangeState(PlayerState.Idle);
        else
            GameManager.Player.ChangeState(PlayerState.UI);
    }

    public void OnOffList()
    {
        if (isPanelOpen)
            StartCoroutine(PanelSlideRoutine(0, 300));
        else
            StartCoroutine(PanelSlideRoutine(300, 0));
    }

    private IEnumerator PanelSlideRoutine(float start, float end)
    {
        float current = 0;
        float percent = 0;
        float time = 0.2f;

        RectTransform rect = _participantListPanel.GetComponent<RectTransform>();

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / time;

            rect.anchoredPosition = Vector2.Lerp(new Vector2(start, rect.anchoredPosition.y), new Vector2(end, rect.anchoredPosition.y), percent);
            yield return null;
        }

        isPanelOpen = !isPanelOpen;
    }
}
