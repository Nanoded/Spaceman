using UnityEngine;
using DG.Tweening;

public class ShipController : MonoBehaviour
{
    [SerializeField] private AttackBehaviour _attackBehaviour;
    [SerializeField] private float _distanceToGrid;
    [SerializeField] private float _movementTime;
    [SerializeField] private Vector3 _rotationEndValue;
    [SerializeField] private GridGenerator _gridGenerator;
    private ShipAnimation _shipAnimation;
    private Sequence _idleAnimation;
    private Sequence _moveToSequence;
    private Tween _preattackRotationTween;

    public Vector3 ShipPosition => transform.localPosition;

    private void Awake()
    {
        _shipAnimation = new ShipAnimation(transform, _movementTime, _rotationEndValue);
        _idleAnimation = _shipAnimation.CreateIdleAnimation().SetAutoKill(false);
    }

    private void Start()
    {
        Vector3 startPosition = transform.position;
        Vector3 startRotation = transform.rotation.eulerAngles;
        EventHandler.StartGameEvent.AddListener(() =>
        {
            _idleAnimation.Pause();
            StartMoveToShootingPosition();
        });
        EventHandler.ReturnMainMenuEvent.AddListener(() =>
        {
            _moveToSequence.Kill();
            _preattackRotationTween.Kill();
            _moveToSequence = _shipAnimation.CreateMoveToAnimation(startPosition);
            _moveToSequence.Append(transform.DORotate(startRotation, 1).SetEase(Ease.Linear));
            _moveToSequence.OnComplete(() => _idleAnimation.Play());
            _moveToSequence.Play();
        });
        EventHandler.RestartLevelEvent.AddListener(PreattackPreparation);
    }

    private void StartMoveToShootingPosition()
    {
        _moveToSequence.Kill();
        Vector3 movePosition =  new Vector3(0, .5f, -(_gridGenerator.GridRadius + _distanceToGrid));
        _moveToSequence = _shipAnimation.CreateMoveToAnimation(movePosition);
        _moveToSequence.OnComplete(() =>
        {
            PreattackPreparation();
        });
        _moveToSequence.Play();
    }

    private void PreattackPreparation()
    {
        _preattackRotationTween = transform.DORotate(_rotationEndValue, 1).OnComplete(() =>
        {
            _attackBehaviour.EnableAttackBehaviour(_shipAnimation.CreateRotationAnimation());
        });
    }
}
