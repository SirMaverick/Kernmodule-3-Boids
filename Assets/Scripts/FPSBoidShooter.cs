using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSBoidShooter : MonoBehaviour {

    public Camera FPScamera;
    public float speed;
    public float maxFOV;
    public float minFOV;
    public Boid boids;
    RaycastHit hit;
    public float range;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKey(KeyCode.Mouse0)) {
            if (Physics.Raycast(transform.position, transform.forward, out hit, range)) {
                Debug.DrawRay(transform.position, transform.forward, Color.red);
                if (hit.transform.tag == "Boid") {
                    print("hit");
                    boids.boidList.Remove(hit.transform.gameObject);
                    Destroy(hit.transform.gameObject);

                }
            }
        }
       

        if (Input.GetKey(KeyCode.Space) && FPScamera.fieldOfView >= maxFOV) {
            FPScamera.fieldOfView = FPScamera.fieldOfView - speed;

        } else if (Input.GetKey(KeyCode.Space) == false && FPScamera.fieldOfView <= minFOV) {
            FPScamera.fieldOfView = FPScamera.fieldOfView + speed;
        }
    }


    private void OnMouseDown() {
        
    }
}
