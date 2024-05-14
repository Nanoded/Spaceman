using DG.Tweening;
using UnityEngine;

namespace NewNamespace
{
    public class MainMenuAnimation : MonoBehaviour
    {
        [SerializeField] private Ease _ease;
        [SerializeField] private float _speed;
        [SerializeField] private RectTransform[] _elementsTransforms;
        private Vector2[] _elementsStartPositions;

    	private void Start()
    	{
            EventHandler.StartGameEvent.AddListener(DisableMenu);
            EventHandler.ReturnMainMenuEvent.AddListener(EnableMenu);
            SaveStartPositions();
            EnableMenu();
    	}

        private void SaveStartPositions()
        {
            _elementsStartPositions = new Vector2[_elementsTransforms.Length];
            for (int i = 0; i < _elementsTransforms.Length; i++)
            {
                _elementsStartPositions[i] = _elementsTransforms[i].anchoredPosition;
            }
        }

        private void EnableMenu()
        {
            foreach (var button in _elementsTransforms)
            {
                button.DOAnchorPos(Vector2.zero, _speed).SetEase(_ease);
            }
        }

        private void DisableMenu()
        {
            for(int i = 0; i < _elementsTransforms.Length; i++)
            {
                _elementsTransforms[i].DOAnchorPos(_elementsStartPositions[i], _speed).SetEase(_ease);
            }
        }
    }
}
