using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _nowTimeText;

    [SerializeField] GameObject _participantListPanel;
    [SerializeField] GameObject _mainMenuPanel;

    [SerializeField] GameObject _manualChanger;
    [SerializeField] GameObject _nameChanger;

    private bool isPanelOpen = false;    

    private void Update()
    {
        if (_nowTimeText != null)
            _nowTimeText.text = DateTime.Now.ToString("HH:mm");
    }    

    public void OnOffCharacterChanger()
    {
        if (_manualChanger.activeSelf)
            _manualChanger.SetActive(false);
        else
            _manualChanger.SetActive(true);
    }

    public void OnOffNameChanger()
    {
        if (_nameChanger.activeSelf)
            _nameChanger.SetActive(false);
        else
            _nameChanger.SetActive(true);
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
