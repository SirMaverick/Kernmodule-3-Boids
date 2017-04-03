using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidMove : MonoBehaviour {

    Vector2 v1, v2, v3;
    
    List<GameObject> boidList = new List<GameObject>();
    public int spawnAmount;
    public Object prefab;

	// Use this for initialization
	void Start () {
        for (int i =0; i < spawnAmount; i++) {
            GameObject go = Instantiate(prefab, new Vector3(Random.Range(-25, 25), Random.Range(-25, 25), 0), Quaternion.identity) as GameObject;
            boidList.Add(go);
        }
         
        /* for each (Boid b)
        v1 = rule1(b);
        v2 = rule2(b);
        v3 = rule3(b);
        
        b.velocity = b.velocity + v1 + v2 + v3;
        b.position = b.position + b.velocity; 
        */
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    
}
