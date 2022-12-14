using DG.Tweening;
using UnityEngine;

public class ShipAnimation
{
    private Transform _transform;
    private float _speed;
    private Vector3 _rotationEndValue;

    public ShipAnimation(Transform transform, float speed, Vector3 rotationEndValue)
    {
        _transform = transform;
        _speed = speed;
        _rotationEndValue = rotationEndValue;
    }

    public Sequence CreateMoveToAnimation(Vector3 movePosition)
    {
        Sequence moveSequence = DOTween.Sequence();
        moveSequence.Pause();
        moveSequence.Append(_transform.DOMove(movePosition, _speed));
        moveSequence.Insert(0, _transform.DOLookAt(movePosition, _speed));
        return moveSequence;
    }

    public Sequence CreateRotationAnimation()
    {
        Vector3 reverseRotationEndValueX = new Vector3(-_rotationEndValue.x, -_rotationEndValue.y, -_rotationEndValue.z);
        Sequence rotationSequence = DOTween.Sequence();
        rotationSequence.Pause();
        rotationSequence.Append(_transform.DORotate(reverseRotationEndValueX, _speed));
        rotationSequence.Append(_transform.DORotate(_rotationEndValue, _speed));
        rotationSequence.SetLoops(-1);
        return rotationSequence;
    }

    public Sequence CreateIdleAnimation()
    {
        Sequence idleAnimation = DOTween.Sequence();
        idleAnimation.Append(_transform.DOMoveY(_transform.position.y + .1f, _speed).SetEase(Ease.Linear));
        idleAnimation.Append(_transform.DOMoveY(_transform.position.y - .1f, _speed).SetEase(Ease.Linear));
        idleAnimation.SetLoops(-1, LoopType.Yoyo);
        return idleAnimation;
    }
}
