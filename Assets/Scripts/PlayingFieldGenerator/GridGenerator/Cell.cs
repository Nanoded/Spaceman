using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private Transform _itemPosition;
    private bool _isEmpty = true;

    public bool IsEmpty => _isEmpty;

    public void PutItem(Transform item)
    {
        if(item == null)
        {
            return;
        }
        item.position = _itemPosition.position;
        ChangeState(false);
    }

    public void ChangeState(bool isEmpty)
    {
        _isEmpty = isEmpty;
    }
}
