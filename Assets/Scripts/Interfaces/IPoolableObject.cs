using UnityEngine;

public interface IPoolableObject
{
    ObjectPool Pool { get; set; }
    
    void InitPool(ObjectPool pool)
    {
        Pool = pool;
    }

    void GetObject(Transform userTransform);

    void StateReset();

    void UseObject();
}
