using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public TilemapRenderer Spikes;
    public PlayerState playerState;
    public GameOverScript GameOverScript;

    public enum PlayerState
    {
        Alive,
        Dead,
        Paused
    }

    void Awake()
    {
        playerState = PlayerState.Alive;
    }
    void Start()
    {

    }

    public void GameOver()
    {
        GameOverScript.Setup();
    }
    void Update()
    {
        switch (playerState)
        {
            case PlayerState.Alive:
                break;
            case PlayerState.Dead:
                Debug.Log("DEAD");
                GameOver();
                break;

        }
    }

}
