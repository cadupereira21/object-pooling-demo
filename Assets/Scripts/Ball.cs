using UnityEngine;

public class Ball : PooledObject {
    
    [SerializeField]
    private float destroyYPosition = -10f;
    
    private void Update() {
        if (this.transform.position.y < destroyYPosition) {
            this.SpawnManager.DespawnObject(this.IndexAtPooler);
        }
    }
}