using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish_Level : MonoBehaviour
{
    public GameObject YouWin;
    private Player_controller PlayerController;
    void Start()
    {
        GameObject YouWin = GameObject.FindWithTag("YouWin");
        GameObject PlayerControllerObject = GameObject.FindWithTag("Player");
        PlayerController = PlayerControllerObject.GetComponent<Player_controller>();
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (SceneManager.GetActiveScene().name == "Level_2" && col.transform.CompareTag("Player"))
        {
            YouWin.gameObject.SetActive(true);
            PlayerController.gameObject.SetActive(false);
        }
        if (col.transform.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }
 
        
    }
}
