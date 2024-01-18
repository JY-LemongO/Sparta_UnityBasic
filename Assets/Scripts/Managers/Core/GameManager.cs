using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager s_instance;
    public static GameManager Instance { get { Init(); return s_instance; } }

    private static Player _player;

    public static Player Player
    {
        get
        {
            if(_player == null)
            {
                _player = FindObjectOfType<Player>();
                _player.Setup(PlayerName, PlayerAnimator);
            }
                return _player;
        }
    }

    public delegate void OnChangeAnimator(int index);
    public static event OnChangeAnimator _onChangeAnimator;

    private static int _playerAnimator;

    public static string    PlayerName      { get; private set; }
    public static int       PlayerAnimator
    {
        get { return _playerAnimator; }
        private set
        {
            _playerAnimator = value;            
            _onChangeAnimator?.Invoke(value);
        }
    }

    private void Awake()
    {
        Init();

        if(s_instance != gameObject)
            Destroy(gameObject);        
    }

    private static void Init()
    {
        if(s_instance == null)
        {
            GameObject go = GameObject.Find("@GameManager");

            if(go == null)
            {
                go = new GameObject("@GameManager");
                go.AddComponent<GameManager>();
            }

            s_instance = go.GetComponent<GameManager>();
            DontDestroyOnLoad(go);
        }        
    }

    public void GoMainScene(string name)
    {
        _onChangeAnimator = null;
        SetName(name);
        SceneManager.LoadScene("MainScene");
    }

    public void SetName(string name) => PlayerName = name;
    public void SetAnimator(int index) => PlayerAnimator = index;
}
