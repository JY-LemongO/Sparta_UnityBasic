using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _nowTimeText;
    [SerializeField] GameObject _manualChanger;
    [SerializeField] GameObject _nameChanger;        

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
}
