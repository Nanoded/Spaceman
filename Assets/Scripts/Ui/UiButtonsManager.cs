using UnityEngine;

public class UiButtonsManager : MonoBehaviour
{
    public void StartGameButton() => EventHandler.StartGameEvent.Invoke();
    public void ReturnMainMenuButton() 
    {
        EventHandler.ReturnMainMenuEvent.Invoke();
        EventHandler.ChangeScoreEvent.Invoke(0);
    }
    public void RestartLevel() 
    { 
        EventHandler.RestartLevelEvent.Invoke();
        EventHandler.ChangeScoreEvent.Invoke(0);
    }
    public void ExitGame() => Application.Quit();
}
