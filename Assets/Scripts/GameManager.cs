using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    Vector2 v1, v2, v3;
    Collider collider;

    public static List<GameObject> boidList = new List<GameObject>();
    public static List<GameObject> newBoidList = new List<GameObject>();
    public int spawnAmount;
    public Object prefab;
    public static GameManager instance;
    Vector3 position;

    private void Awake() {
        instance = this;
        collider = GetComponent<Collider>();
        position = new Vector3(Random.value * collider.bounds.size.x, Random.value * collider.bounds.size.y, 0);
        for (int i = 0; i < spawnAmount; i++) {
            GameObject go = Instantiate(prefab, new Vector3( Random.Range(-3, 3), Random.Range(-3, 3), Random.Range(-3, 3) ), Quaternion.identity) as GameObject;
            go.transform.parent = transform;
            //go.transform.localPosition = position; 
            boidList.Add(go);
            
            
        }
    }

    public void Instantiate() {
        
    }

}
