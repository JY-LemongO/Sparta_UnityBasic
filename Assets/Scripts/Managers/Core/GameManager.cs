using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager s_instance;
    private static bool s_init = false;

    private Social _social = new Social();
    public static Social Social => Instance?._social;

    private static Player _player;
    public static Player Player
    {
        get
        {
            if (_player == null)
            {
                Player player = FindObjectOfType<Player>();
                if (player != null)
                    player.Setup(PlayerName, PlayerAnimator);

                _player = player;
            }
            return _player;
        }
    }

    public delegate void OnChangeAnimator(int index);
    public static event OnChangeAnimator onChangeAnimator;    

    private static int plyaerAnimatorIndex;

    public static string    PlayerName      { get; private set; }
    public static int       PlayerAnimator
    {
        get => plyaerAnimatorIndex;
        private set
        {
            plyaerAnimatorIndex = value;            
            onChangeAnimator?.Invoke(value);
        }
    }

    public void GoMainScene(string name)
    {
        onChangeAnimator = null;
        SetName(name);
        SceneManager.LoadScene("MainScene");
    }

    public void SetName(string name) => PlayerName = name;
    public void SetAnimator(int index) => PlayerAnimator = index;


    public static GameManager Instance
    {
        get
        {
            if (s_init == false)
            {
                s_init = true;
                GameObject go = GameObject.Find("@GameManager");
                if (go == null)
                {
                    go = new GameObject() { name = "@GameManager" };
                    go.AddComponent<GameManager>();
                }

                DontDestroyOnLoad(go);
                s_instance = go.GetComponent<GameManager>();
            }

            return s_instance;
        }
    }
}
