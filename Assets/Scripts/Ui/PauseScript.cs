using UnityEngine;
using YG;

public class PauseScript : MonoBehaviour
{
    private bool _canWork;

    private void Awake()
    {
        _canWork = true;
        YandexGame.OpenFullAdEvent += () => _canWork = false;
        YandexGame.CloseFullAdEvent += () => _canWork = true;
    }

    private void OnApplicationFocus(bool focus)
    {
        if(_canWork)
            Enable(focus);
    }

    public void Enable(bool isActive)
    {
        if(isActive)
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
