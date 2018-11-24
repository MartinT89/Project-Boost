using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    [SerializeField]float rcsThrust = 100f;
    [SerializeField] float mainThrust = 35f;
    Rigidbody rocketBody;
    AudioSource audioSource;

	// Use this for initialization
	void Start () {
        rocketBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        ThrustAndSound();
        RotationControls();

	}

    void OnCollisionEnter(Collision collision) {
        switch (collision.gameObject.tag) {
            case "Friendly":
                print("OK");
                break;
            default:
                print("dead");
                break;
        }
    }

    private void ThrustAndSound()
    {
        if (Input.GetKey(KeyCode.Space))
        { //can thrust while rotating 
            rocketBody.AddRelativeForce(Vector3.up * mainThrust);

            if (!audioSource.isPlaying)     //no layering of audio
            {
                audioSource.Play();
            }

        }

        else
        {
            audioSource.Stop();
        }
    }

    private void RotationControls()
    {
        rocketBody.freezeRotation = true; // take manual control of rotation
        float rotationThisFrame = rcsThrust * Time.deltaTime; //independent of the frame 

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

        rocketBody.freezeRotation = false; // resume physics control of rotation 
    }

}
