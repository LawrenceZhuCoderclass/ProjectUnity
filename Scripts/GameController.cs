using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    public TilemapRenderer Spikes;
    public PlayerState playerState;
    public GameOverScript GameOverScript;
    public PauseScript PauseScript;
    private Player_controller PlayerController;
    public Background GameOverBackGround;

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
        GameObject PlayerControllerObject = GameObject.FindWithTag("Player");
        if (PlayerControllerObject != null)
        {
            PlayerController = PlayerControllerObject.GetComponent<Player_controller>();
        }
    }
    void Update()
    {
        switch (playerState)
        {
            case PlayerState.Alive:
                Alive();
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    playerState = PlayerState.Paused;
                }
                break;
            case PlayerState.Dead:
                GameOver();
                break;
            case PlayerState.Paused:
                Pause();                
                break;

        }
    }
    public void Alive()
    {
        Time.timeScale = 1f;
        Debug.Log("Alive");
    }
    public void GameOver()
    {
        GameOverScript.Setup();
        PlayerController.gameObject.SetActive(false);
        Debug.Log("GameOver");
    }
    public void Pause()
    {
        PauseScript.PopUp();
        Time.timeScale = 0f;
        Debug.Log("Paused");
    }
    public void Resume()
    {
        playerState = PlayerState.Alive;
        PauseScript.PopOut();
        Debug.Log("resume");

    }
    public void RestartButton()
    {
        SceneManager.LoadScene("Level_1");
        Debug.Log("Level1");
    }
    public void ExitButton()
    {
        SceneManager.LoadScene("Menu");
        Debug.Log("Menu");
    }

}
