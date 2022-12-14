using TMPro;
using UnityEngine;

public class EndGamePanel : MonoBehaviour
{
    [SerializeField] private GameObject _endPanel;
    [SerializeField] private TMP_Text _totalScoreText;
    [SerializeField] private ScoreCounter _scoreCounter;

    private void Start()
    {
        EventHandler.TimerIsEndEvent.AddListener(TimerIsEndHandler);
        _endPanel.SetActive(false);
    }

    private void TimerIsEndHandler()
    {
        _endPanel.SetActive(true);
        _totalScoreText.text = _scoreCounter.CurrentScore.ToString();
    }
}
