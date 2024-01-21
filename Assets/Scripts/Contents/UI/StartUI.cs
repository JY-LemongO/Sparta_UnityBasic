using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUI : MonoBehaviour
{
    [SerializeField] GameObject _manualChanger;

    public void OnOffCharacterChanger()
    {
        if (_manualChanger.activeSelf)
            _manualChanger.SetActive(false);
        else
            _manualChanger.SetActive(true);
    }
}
