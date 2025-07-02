using UnityEngine;
using UnityEngine.Pool;

public class GameObjectPooler {

    // Object to be instantiated
    private readonly GameObject _prefab;

    private readonly ObjectPool<GameObject> _objectPool;

    // Default size of the pool
    private int _defaultSize;

    // Maximum size of the pool
    private int _maxSize;

    public GameObjectPooler(GameObject prefab, int defaultSize, int maxSize) {
        _prefab = prefab;
        _defaultSize = defaultSize;
        _maxSize = maxSize;
        _objectPool = new ObjectPool<GameObject>(CreatePooledObject, OnGetFromPool, OnReturnToPool, OnDestroyPooledObject, true, defaultSize, maxSize);
    }
    
    // Wrapper function for pool.Get. Gets object and sets position.
    public GameObject GetObject(Vector3 position)
    {
        GameObject obj = _objectPool.Get();
        obj.transform.position = position;
        return obj;
    }

    // Wrapper function for pool.Release
    public void ReleaseObject(GameObject obj)
    {
        _objectPool.Release(obj);
    }
    
    // Return a brand new GameObject instance for our pool to use.
    // We have to specify GameObject.Instantiate because this isn't
    // a Monobehavior.    
    private GameObject CreatePooledObject() {
        GameObject newObject = Object.Instantiate(_prefab);
        return newObject;
    }
    
    // When an object is taken from the pool, activate it.
    private static void OnGetFromPool(GameObject pooledObject) {
        pooledObject.SetActive(true);
    }
    
    // When an object is returned to the pool, deactivate it.
    private static void OnReturnToPool(GameObject pooledObject) {
        pooledObject.SetActive(false);
    }
    
    // When the pool discards an object, destroy the GameObject.
    private static void OnDestroyPooledObject(GameObject pooledObject) {
        Object.Destroy(pooledObject);
    }
}