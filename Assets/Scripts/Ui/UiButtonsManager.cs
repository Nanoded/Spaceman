using UnityEngine;

public class UiButtonsManager : MonoBehaviour
{
    public void StartGameButton() => EventHandler.StartGameEvent.Invoke();
    public void ReturnMainMenuButton() => EventHandler.ReturnMainMenuEvent.Invoke();
    public void RestartLevel() => EventHandler.RestartLevelEvent.Invoke();
    public void ExitGame() => Application.Quit();
}
