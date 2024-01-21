using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelector : MonoBehaviour
{    
    [SerializeField] RuntimeAnimatorController[] _controllers;
    [SerializeField] Animator _animator;    

    private int _selectedIndex = 0;

    private void Start()
    {
        GameManager.onChangeAnimator -= ChangeRmote;
        GameManager.onChangeAnimator += ChangeRmote;
    }

    public void OnChangeCharacter(int direc)
    {
        int index = _selectedIndex + direc;

        if(index >= 0 && index < _controllers.Length)
        {
            _selectedIndex = index;
            GameManager.Instance.SetAnimator(index);            
        }            
    }    

    public void ChangeRmote(int index)
    {
        _selectedIndex = index;
        _animator.runtimeAnimatorController = _controllers[index];
    }
}
