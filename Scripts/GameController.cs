using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    
    public PlayerState playerState;
    public  GameObject PauseScript;
    private Player_controller PlayerController;
    public GameObject GameOverBackGround;
    

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
        
        GameObject PauseScript = GameObject.FindWithTag("Pause");
        GameObject GameOverBackGround = GameObject.FindWithTag("GameOverBackGround");
        GameObject PlayerControllerObject = GameObject.FindWithTag("Player");
        PlayerController = PlayerControllerObject.GetComponent<Player_controller>();

        
    }
    void Update()
    {
        switch (playerState)
        {
            case PlayerState.Alive:
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

    public void GameOver()
    {
        GameOverBackGround.gameObject.SetActive(true);
        PlayerController.gameObject.SetActive(false);
    }
    public void Pause()
    {
        PauseScript.gameObject.SetActive(true);
        PlayerController.enabled = false;
    }
    public void Resume()
    {
        playerState = PlayerState.Alive;
        PauseScript.gameObject.SetActive(false);
        PlayerController.enabled = true;
    }
    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Level1");
    }
    public void ExitButton()
    {
        SceneManager.LoadScene("Menu");
    }

}
