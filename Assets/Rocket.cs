using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    Rigidbody rocketBody;
    AudioSource audioSource;

	// Use this for initialization
	void Start () {
        rocketBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        ProcessInput();
	}

    private void ProcessInput()
    {
        ThrustAndSound();
        RotationControls();
    }

    private void RotationControls()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward);
        }
    }

    private void ThrustAndSound()
    {
        if (Input.GetKey(KeyCode.Space))
        { //can thrust while rotating 

            rocketBody.AddRelativeForce(Vector3.up);

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
}
