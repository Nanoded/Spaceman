using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RestartPanelAnimation : MonoBehaviour
{
    [SerializeField] private Image _restartPanelImage;
    [SerializeField] private float _animationDuration;
    private Tween _restartAniomation;

    void Start()
    {
        ChangePanelVisibility(false);
        EventHandler.RestartLevelEvent.AddListener(RestartLevel);
        _restartAniomation = _restartPanelImage.DOFade(0, _animationDuration).Pause();
        _restartAniomation.OnStepComplete(() => ChangePanelVisibility(false));
        _restartAniomation.SetAutoKill(false);
    }

    private void RestartLevel()
    {
        _restartPanelImage.color = Color.white;
        ChangePanelVisibility(true);
        _restartAniomation.Restart();
    }

    private void ChangePanelVisibility(bool isVisible)
    {
        _restartPanelImage.gameObject.SetActive(isVisible);
    }
}
