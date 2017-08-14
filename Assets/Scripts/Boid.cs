using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour {
    public Vector3 v1, v2, v3;

    Rigidbody rb;
    BoidMove b;
    Vector3 c;
    Vector3 pc;
    Vector3 pv;
    Vector3 maxVelVec = new Vector3(2, 2, 2);
    float maxVel = 0.002f;
    float distance = 1.0f;
    float range = 1.0f;
    public List<GameObject> boidList;
    List<GameObject> inRangeBoids;

    // Use this for initialization
    void Start () {
        boidList = GameManager.boidList;
        inRangeBoids = new List<GameObject>();
        rb = gameObject.GetComponent<Rigidbody>();
        foreach (GameObject boid in boidList) {
            if (Mathf.Abs(boid.transform.position.x - transform.position.x) < 15 && Mathf.Abs(boid.transform.position.y - transform.position.y) < 15 && Mathf.Abs(boid.transform.position.z - transform.position.z) < 15) {
                inRangeBoids.Add(boid);
            }
        }
        StartCoroutine(ApplyRules());
    }

    public void RecheckList() {
        boidList = GameManager.newBoidList;
    }

    private Vector3 Rule1(GameObject b) {
        pc = new Vector3(0, 0, 0);
        foreach (GameObject boid in inRangeBoids) {
            if (boid != b) {
                pc += boid.transform.position;
            }
            
        }
        pc /= (inRangeBoids.Count - 1);
        
        return (pc - b.transform.position) / 10;
    }

    private Vector3 Rule2(GameObject b) {
        c = new Vector3(0 ,0 ,0);
        foreach (GameObject boid in inRangeBoids) {
            if (boid != b) {
                if ((Mathf.Abs(boid.transform.position.x - b.transform.position.x) <= distance) && (Mathf.Abs(boid.transform.position.y - b.transform.position.y) <= distance) && (Mathf.Abs(boid.transform.position.z - b.transform.position.z) <= distance )) {
                    c = c - (boid.transform.position - b.transform.position);
                }
                
            }
        }

        return c ;
    }

    private Vector3 Rule3(GameObject b) {
        pv = new Vector3(0, 0, 0);
        foreach (GameObject boid in inRangeBoids) {
            if (boid != b) {
                pv = pv + b.GetComponent<Rigidbody>().velocity;
            }
        }
        pv = pv / (inRangeBoids.Count - 1);
        return (pv - rb.velocity) / 8;
    }

    void SetValue(Vector3 v) {
        if (v.x > maxVel) {
            v.x = maxVel;
        } else if (v.x < -maxVel) {
            v.x = -maxVel;
        }
        if (v.y > maxVel) {
            v.y = maxVel;
        } else if (v1.y < -maxVel) {
            v.y = -maxVel;
        }
        if (v.z > maxVel) {
            v.z = maxVel;
        } else if (v.z < -maxVel) {
            v.z = -maxVel;
        }
    }

    void CheckBoidList() {
        foreach(GameObject boid in inRangeBoids) {
            if (Mathf.Abs(boid.transform.position.x - transform.position.x) > range && Mathf.Abs(boid.transform.position.y - transform.position.y) > range && Mathf.Abs(boid.transform.position.z - transform.position.z) > range) {
                inRangeBoids.Remove(boid);
            }
        }
        foreach(GameObject boid in boidList) {
            if (Mathf.Abs(boid.transform.position.x - transform.position.x) <= range && Mathf.Abs(boid.transform.position.y - transform.position.y) <= range && Mathf.Abs(boid.transform.position.z - transform.position.z) <= range) {
                inRangeBoids.Add(boid);
            }
        }
    }

    IEnumerator ApplyRules() {
        v1 = Rule1(gameObject);
        v2 = Rule2(gameObject);
        v3 = Rule3(gameObject);
        SetValue(v1);
        SetValue(v2);
        SetValue(v3);
        rb.velocity += (v1 + v2 + v3);
        SetValue(rb.velocity);
        yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));
        StartCoroutine("ApplyRules");
    }

}
