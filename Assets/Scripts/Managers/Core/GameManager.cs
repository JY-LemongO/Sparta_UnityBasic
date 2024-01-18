using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager s_instance;
    public static GameManager Instance { get { Init(); return s_instance; } }

    private Player _player;

    public Player Player
    {
        get
        {
            if(_player == null)
            {
                _player = FindObjectOfType<Player>();
                _player.Setup(PlayerName);
            }
                return _player;
        }
    }
    public static string PlayerName { get; private set; }

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
        SetName(name);
        SceneManager.LoadScene("MainScene");
    }

    public void SetName(string name) => PlayerName = name;
}
