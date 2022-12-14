using DG.Tweening;
using UnityEngine;

public enum ItemType
{
    GoodItem,
    BadItem
}

[RequireComponent(typeof(AudioSource))]
public class GridItem : MonoBehaviour
{
    [SerializeField] private ItemType _itemType;
    [SerializeField] private int _scoreCount;
    [SerializeField] private float _animationDuration;
    [SerializeField] private Transform _visualItem;
    [SerializeField] private ParticleSystem _destroyEffect;
    private Cell _currentCell;
    private Sequence _idleAnimation;
    private AudioSource _hitSound;

    public ItemType CurrentType => _itemType;

    private void Start()
    {
        _hitSound = GetComponent<AudioSource>();
        _idleAnimation = InitIdleAnimation();
        _idleAnimation.SetLoops(-1, LoopType.Restart).Play();
    }

    private Sequence InitIdleAnimation()
    {
        Sequence seq = DOTween.Sequence();
        seq.Pause();
        seq.Append(_visualItem.DORotate(_visualItem.rotation.eulerAngles + Vector3.up * 180, _animationDuration).SetEase(Ease.Linear));
        seq.Append(_visualItem.DORotate(_visualItem.rotation.eulerAngles + Vector3.up * 360, _animationDuration).SetEase(Ease.Linear));
        return seq;
    }

    public void PutInCell(Cell cell)
    {
        _currentCell = cell;
    }

    public  void CollectItem()
    {
        _hitSound.Play();
        _idleAnimation.Pause();
        _visualItem.gameObject.SetActive(false);
        _destroyEffect.Play();
        EventHandler.ChangeScoreEvent.Invoke(_scoreCount);
        DestroyObject(_destroyEffect.main.startLifetime.constant);
    }

    public void DestroyObject(float timeToDestroy = 0)
    {
        _currentCell.ChangeState(true);
        _idleAnimation.Kill();
        Destroy(gameObject, timeToDestroy);
    }
}
