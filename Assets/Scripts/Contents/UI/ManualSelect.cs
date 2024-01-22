using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManualSelect : MonoBehaviour
{
    public void OnManualChangeCharacter(int index)
    {
        if (SceneManager.GetActiveScene().name == "StartScene")
            GameManager.Instance.SetAnimator(index);
        else
        {            
            UIManager.IsChangerOpen = false;
            GameManager.Player.ChangeState(PlayerState.Idle);
            GameManager.Player.ChangeAnimator(index);
        }            
        
        gameObject.SetActive(false);
    }    
}
