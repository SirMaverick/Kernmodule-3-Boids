using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSBoidShooter : MonoBehaviour {

    public Camera FPScamera;
    public float speed;
    public float maxFOV;
    public float minFOV;
    public Boid boids;
    RaycastHit hit;
    public AudioSource audioS;
    public AudioClip shotSound, reloadSound;
    public float range;
    public Text boidCountText;
    public Text bulletCountText;
    public Text shotsFiredText;
    public GameObject reloadText;
    int boidCount;
    int bulletCount = 6;
    int shotsFired = 0;
    bool firstTimeShoot, firstTimeZoom;
    bool shotReady;

    // Use this for initialization
    void Start() {
        ShowBoidScore();
        reloadText = GameObject.Find("ReloadText");
        reloadText.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            if(bulletCount > 0 && audioS.isPlaying == false) {
                if (firstTimeShoot == false) {
                    GameObject.Find("ShootText").SetActive(false);
                    firstTimeShoot = true;
                }
                audioS.clip = shotSound;
                audioS.Play();
                AddShotAmount();
                ReduceBulletAmount();
                if (Physics.Raycast(FPScamera.transform.position, FPScamera.transform.forward, out hit)) {
                    Debug.DrawRay(transform.position, transform.forward, Color.red);
                    if (hit.transform.tag == "Boid") {
                        boids = hit.transform.gameObject.GetComponent<Boid>();
                        print("hit");
                        GameManager.newBoidList = GameManager.boidList;
                        GameManager.newBoidList.Remove(hit.transform.gameObject);
                        foreach (GameObject b in GameManager.boidList) {
                            b.GetComponent<Boid>().RecheckList();
                        }

                        GameManager.boidList.Remove(hit.transform.gameObject);
                        ShowBoidScore();
                        hit.transform.gameObject.SetActive(false);

                    }
                }
            }
            
        }

        if(bulletCount == 0) {
           reloadText.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            StartCoroutine(Reload());
            if(reloadText.activeSelf) {
                reloadText.SetActive(false);
            }
        }
       

        if (Input.GetKey(KeyCode.Space) && FPScamera.fieldOfView >= maxFOV) {
            if(firstTimeZoom == false) {
                GameObject.Find("ZoomText").SetActive(false);
                firstTimeZoom = true;
            }
            FPScamera.fieldOfView = FPScamera.fieldOfView - speed;

        } else if (Input.GetKey(KeyCode.Space) == false && FPScamera.fieldOfView <= minFOV) {
            FPScamera.fieldOfView = FPScamera.fieldOfView + speed;
        }
    }


    private void AddShotAmount() {
        shotsFired++;
        shotsFiredText.text = "Shots Fired : " + shotsFired;
    }

    private void ShowBoidScore() {
        boidCount = GameManager.boidList.Count;
        boidCountText.text = "Boids Left : " + boidCount;
    }

    private void ReduceBulletAmount() {
        bulletCount--;
        bulletCountText.text = "x" + bulletCount;

    }

    IEnumerator Reload() {
        bulletCount = 6;
        audioS.clip = reloadSound;
        audioS.Play();
        yield return new WaitForSeconds(1.5f);
        bulletCountText.text = "x" + bulletCount;
    }
}
