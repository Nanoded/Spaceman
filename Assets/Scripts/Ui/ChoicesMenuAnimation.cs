using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public enum SequenceType
{
    OpenSequence,
    CloseSequence
}

public class ChoicesMenuAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform[] _menuItems;
    [SerializeField] private Button _menuCallButton;
    [SerializeField] private float _openMenuSpeed;
    private Sequence _openMenuTween;
    private Sequence _closeMenuTween;
    private SequenceType _currentSequenceType;

    private void Start()
    {
        CreateAnimationSequences();
        _currentSequenceType = SequenceType.OpenSequence;
        _menuCallButton.onClick.AddListener(OpenCloseMenu);
        EventHandler.RestartLevelEvent.AddListener(ReloadMenu);
        EventHandler.ReturnMainMenuEvent.AddListener(ReloadMenu);
    }

    private void CreateAnimationSequences()
    {
        _openMenuTween = DOTween.Sequence();
        _closeMenuTween = DOTween.Sequence();
        _openMenuTween.Pause().SetAutoKill(false)
            .OnPlay(() => _menuCallButton.interactable = false)
            .OnStepComplete(() => _menuCallButton.interactable = true);
        _closeMenuTween.Pause().SetAutoKill(false)
            .OnPlay(() => _menuCallButton.interactable = false)
            .OnStepComplete(() => _menuCallButton.interactable = true); 
        FillInSequences();
    }

    private void FillInSequences()
    {
        Vector3 closePosition = _menuCallButton.GetComponent<RectTransform>().localPosition;
        foreach (var i in _menuItems)
        {
            Vector3 openPosition = i.localPosition;
            i.localPosition = closePosition;
            _openMenuTween.onPlay += () => i.gameObject.SetActive(true);
            _closeMenuTween.onStepComplete += () => i.gameObject.SetActive(false);
            _openMenuTween.Insert(0, i.DOLocalMove(openPosition, _openMenuSpeed));
            _closeMenuTween.Insert(0, i.DOLocalMove(closePosition, _openMenuSpeed));
            i.gameObject.SetActive(false);
        }
    }

    private void OpenCloseMenu()
    {
        Sequence currentSequence = _currentSequenceType == SequenceType.OpenSequence ? _openMenuTween : _closeMenuTween;
        _currentSequenceType = currentSequence == _openMenuTween ? SequenceType.CloseSequence : SequenceType.OpenSequence;
        currentSequence.Restart();
    }

    private void ReloadMenu()
    {
        _closeMenuTween.Restart();
        _currentSequenceType = SequenceType.OpenSequence;
    }
}
