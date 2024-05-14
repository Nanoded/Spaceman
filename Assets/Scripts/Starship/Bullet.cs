using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour, IPoolableObject
{
    [SerializeField] private float _speed;
    [SerializeField] private int _lifetime;
    [SerializeField] private int _missedScore;
    private Rigidbody _rigidbody;
    private ObjectPool _bulletPool;

    public ObjectPool Pool 
    { 
        get => _bulletPool; 
        
        set 
        {
            _bulletPool = _bulletPool != null ? _bulletPool : value;
        } 
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out GridItem gridItem))
        {
            gridItem.CollectItem();
            Pool.ReturnObject(this);
        }
    }

    public void StateReset()
    {
        _rigidbody.velocity = Vector3.zero;
        transform.position = Pool.transform.position;
    }

    public void UseObject()
    {
        StartCoroutine(WaitBulletLifetime());
    }

    private IEnumerator WaitBulletLifetime()
    {
        _rigidbody.AddForce(transform.forward, ForceMode.Force);
        yield return new WaitForSeconds(_lifetime);
        if(_rigidbody.velocity.magnitude > 0)
        {
            EventHandler.ChangeScoreEvent.Invoke(_missedScore);
        }
        Pool.ReturnObject(this);
    }

    public void GetObject(Transform userTransform)
    {
        transform.position = userTransform.position;
        transform.rotation = userTransform.rotation;
    }
}
