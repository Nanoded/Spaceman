using DG.Tweening;
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
            EventHandler.ReturnMainMenuEvent.AddListener(() => 
            { 
                StopAllCoroutines();
                ShowPanel();
            });
            EventHandler.StartGameEvent.AddListener(() => HidePanel());
            ShowPanel();
    	}

        private void ShowPanel()
        {
            _bonusPanel.DOAnchorPos(Vector2.zero, 1).SetEase(Ease.Linear);
        }

        public void HidePanel()
        {
            _bonusPanel.DOAnchorPos(_hidePosition, 1).SetEase(Ease.Linear);
        }
    }
}
