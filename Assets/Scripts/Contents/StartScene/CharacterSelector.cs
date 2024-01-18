using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelector : MonoBehaviour
{    
    [SerializeField] RuntimeAnimatorController[] _controllers;
    [SerializeField] Animator _animator;
    [SerializeField] GameObject _manualSelector;

    private int _selectedIndex = 0;

    private void Start()
    {
        _animator.runtimeAnimatorController = _controllers[_selectedIndex];
    }

    public void OnChangeCharacter(int direc)
    {
        int index = _selectedIndex + direc;

        if(index >= 0 && index < _controllers.Length)
        {
            _selectedIndex = index;
            _animator.runtimeAnimatorController = _controllers[_selectedIndex];
            GameManager.Instance.SetAnimator(_selectedIndex);
        }
    }

    public void OnOffSelector()
    {
        if (_manualSelector.activeSelf)
            _manualSelector.SetActive(false);
        else
            _manualSelector.SetActive(true);
    }

    public void OnManualChangeCharacter(int index)
    {
        _selectedIndex = index;
        _animator.runtimeAnimatorController = _controllers[_selectedIndex];
        GameManager.Instance.SetAnimator(_selectedIndex);
        OnOffSelector();
    }
}
