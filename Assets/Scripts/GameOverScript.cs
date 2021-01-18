using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    private Player_controller PlayerController;

    void Start()
    {
        GameObject PlayerControllerObject = GameObject.FindWithTag("Player");
        if (PlayerControllerObject != null)
        {
            PlayerController = PlayerControllerObject.GetComponent<Player_controller>();
        }
    }
    public void Setup()
    {
        gameObject.SetActive(true);
        PlayerController.gameObject.SetActive(false);
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
