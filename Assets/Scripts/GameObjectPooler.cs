using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class GameObjectPooler {

    // Where the objects will be parented in the hierarchy
    private readonly GameObject _parent;

    // Object to be instantiated
    private readonly GameObject _object;

    private readonly List<GameObject> _objectPool = new ();
    
    private readonly Queue<int> _inactiveObjects = new ();

    // Maximum size of the pool
    private readonly int _maxSize;
    
    public int CountAllActive => _objectPool.Count - _inactiveObjects.Count;
    
    public int CountAllInactive => _inactiveObjects.Count;

    public GameObjectPooler(GameObject obj, int defaultSize, int maxSize, GameObject parent = null) {
        _object = obj;
        _maxSize = maxSize;
        _parent = parent;
        
        for (int i = 0; i < defaultSize; i++) {
            CreatePooledObject();
        }
    }
    
    public ObjectPoolerDto GetObject(Vector3 position) {
        if (_inactiveObjects.Count == 0) {
            CreatePooledObject();
        }
        
        ObjectPoolerDto dto = GetInactiveObject();
        dto.Obj.transform.position = position;
        dto.Obj.gameObject.SetActive(true);
        return dto;
    }

    public void ReleaseObject(int i, bool resetRigidbody = false) {
        _inactiveObjects.Enqueue(i);
        GameObject pooledObject = _objectPool[i];
        pooledObject.SetActive(false);

        if (resetRigidbody) {
            ResetRigidbody(pooledObject);
        }
    }
    
    private void CreatePooledObject() {
        if (_objectPool.Count >= _maxSize) {
            return;
        }
        
        GameObject newObject = Object.Instantiate(_object, _parent?.transform);
        newObject.gameObject.SetActive(false);
        _objectPool.Add(newObject);
        _inactiveObjects.Enqueue(_objectPool.Count - 1);
    }
        
    private ObjectPoolerDto GetInactiveObject() {
        int index = _inactiveObjects.Dequeue();
        return new ObjectPoolerDto(_objectPool[index], index);
    }
    
    private static void ResetRigidbody(GameObject pooledObject) {
        if (!pooledObject.TryGetComponent(out Rigidbody rb)) return;
        
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}