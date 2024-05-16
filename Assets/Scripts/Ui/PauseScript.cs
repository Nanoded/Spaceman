using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    private void OnApplicationFocus(bool focus)
    {
        if(focus)
        {
            Time.timeScale = 1;
            AudioListener.pause = false;
        }
        else
        {
            Time.timeScale = 0;
            AudioListener.pause = true;
        }
    }
}
