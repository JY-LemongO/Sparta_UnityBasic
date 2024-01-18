using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager s_instance;
    public static GameManager Instance { get { Init(); return s_instance; } }

    public Player Player { get; private set; }

    private void Awake()
    {
        Init();
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

    public void PlayerSetup(Player player)
    {
        Player = player;
        Player.Setup();
    }
}
