using DG.Tweening;
using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreCounterText;
    [SerializeField] private float _animationDuration;
    private int _currentScore = 0;
    public int CurrentScore => _currentScore;

    private void Start()
    {
        EventHandler.ChangeScoreEvent.AddListener(ChangeScore);
        ApplyScores();
    }

    public void ChangeScore(int score)
    {
        _scoreCounterText.transform.DOShakeScale(_animationDuration);
        _currentScore += score;
        _currentScore = _currentScore < 0 ? 0 : _currentScore;
        ApplyScores();
    }

    private void ApplyScores()
    {
        _scoreCounterText.text = _currentScore.ToString();
    }
}
