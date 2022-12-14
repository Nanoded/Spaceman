using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private IPoolableObject _objectPrefab;
    private List<IPoolableObject> _poolObjects = new List<IPoolableObject>();

    public void InitPool(IPoolableObject objectPrefab)
    {
        gameObject.transform.position = new Vector3(1000, 1000, 1000);
        _objectPrefab = objectPrefab;
    }

    public IPoolableObject GetOrCreateObject(Transform userTransform)
    {
        IPoolableObject objectToReturn;
        if(_poolObjects.Count > 0)
        {
            objectToReturn = _poolObjects[0];
            _poolObjects.Remove(objectToReturn);
            objectToReturn.GetObject(userTransform);
            return objectToReturn;
        }
        objectToReturn = Instantiate((Object)_objectPrefab, userTransform.position, userTransform.rotation) as IPoolableObject;
        objectToReturn.InitPool(this);
        return objectToReturn;
    }

    public void ReturnObject(IPoolableObject poolObject)
    {
        poolObject.StateReset();
        _poolObjects.Add(poolObject);
    }
}
