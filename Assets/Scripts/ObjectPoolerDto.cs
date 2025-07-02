using UnityEngine;

public class ObjectPoolerDto {
    
    public GameObject Obj { get; private set; }
    public int IndexAtPooler { get; private set; }
        
    public ObjectPoolerDto(GameObject obj, int indexAtPooler) {
        Obj = obj;
        IndexAtPooler = indexAtPooler;
    }
        
}