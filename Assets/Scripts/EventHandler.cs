using UnityEngine.Events;

public class EventHandler
{
    public static UnityEvent StartGameEvent = new();
    public static UnityEvent ReturnMainMenuEvent = new();
    public static UnityEvent RestartLevelEvent = new();
    public static UnityEvent TimerIsEndEvent = new();
    public static UnityEvent<int> ChangeScoreEvent = new();
}
