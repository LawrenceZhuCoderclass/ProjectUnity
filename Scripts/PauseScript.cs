using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    public void PopUp()
    {
        gameObject.SetActive(true);
        
    }
    public void PopOut()
    {
        gameObject.SetActive(false);
    }
}
