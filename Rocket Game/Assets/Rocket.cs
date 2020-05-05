using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    [SerializeField] float rcsThrust = 80f;
    [SerializeField] float mainThrust = 80f;

    Rigidbody rigidBody;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start() {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        Thrust();
        Rotate();
    }

    void OnColissionEnter(Collision collision) {
        switch(collision.gameObject.tag) {
            case "Friendly":
                //do nothing
                break;
            default:
                //kill player
                break;
        }
    }

    private void Thrust() {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow)) {
            rigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if (!audioSource.isPlaying) {
                audioSource.Play();
            }
        }
        else {
            audioSource.Stop();
        }
    }

    private void Rotate() {
        rigidBody.freezeRotation = true;
        float rotationThisFrame = Time.deltaTime * rcsThrust;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }
        rigidBody.freezeRotation = false;
    }

}
