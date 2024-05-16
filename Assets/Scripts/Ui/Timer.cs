using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private float _maxValue;
    private float _currentValue;

    private void Awake()
    {
        _currentValue = _maxValue;
        EventHandler.StartGameEvent.AddListener(StartTimer);
        EventHandler.RestartLevelEvent.AddListener(RestartTimer);
        EventHandler.ReturnMainMenuEvent.AddListener(() => 
        { 
            CancelInvoke();
            _currentValue = _maxValue;
        });
    }

    private void StartTimer()
    {
        InvokeRepeating("TimerInvoke", 0f, 1f);
    }

    private void RestartTimer()
    {
        _currentValue = _maxValue;
        CancelInvoke();
        StartTimer();
    }

    private void TimerInvoke()
    {
        if (_currentValue >= 0)
        {
            _timerText.text = _currentValue.ToString();
            --_currentValue;
        }
        else
        {
            CancelInvoke();
            EventHandler.TimerIsEndEvent.Invoke();
        }
    }

    public void AddSeconds(int seconds)
    {
        _currentValue += seconds;
    }
}
