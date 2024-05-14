using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace UI
{
    public class BonusSeconds : MonoBehaviour
    {
        [SerializeField] private RectTransform _bonusPanel;
        [SerializeField] private RectTransform _buttonTransform;
        [SerializeField] private int _secondsForShow;
        private Vector2 _hidePosition;

    	private void Start()
    	{
            _buttonTransform.DORotate(new Vector3(0, 0, 90), 1).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
            _hidePosition = _bonusPanel.anchoredPosition;
            EventHandler.StartGameEvent.AddListener(() =>StartCoroutine(WaitSecondsForShow()));
            EventHandler.RestartLevelEvent.AddListener(() =>
            {
                StopAllCoroutines();
                HidePanel();
                StartCoroutine(WaitSecondsForShow());
            });
            EventHandler.ReturnMainMenuEvent.AddListener(() => 
            { 
                StopAllCoroutines();
                HidePanel();
            });
            EventHandler.TimerIsEndEvent.AddListener(() => HidePanel());
    	}

        private IEnumerator WaitSecondsForShow()
        {
            yield return new WaitForSeconds(_secondsForShow);
            _bonusPanel.DOAnchorPos(Vector2.zero, 1).SetEase(Ease.Linear);
        }

        private void HidePanel()
        {
            _bonusPanel.DOAnchorPos(_hidePosition, 1).SetEase(Ease.Linear);
        }
    }
}
