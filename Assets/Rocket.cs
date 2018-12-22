using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

    [SerializeField]float rcsThrust = 100f;
    [SerializeField] float mainThrust = 35f;
    [SerializeField] AudioClip mainEngine, explosion, loading;
    [SerializeField] ParticleSystem mainEngineP, explosionP, loadingP;
    [SerializeField] int nextLevel;
    Rigidbody rocketBody;
    AudioSource audioSource;

    enum State {Alive, Dying, Transcending };
    State state = State.Alive;

	// Use this for initialization
	void Start () {
        rocketBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {

        if (state == State.Alive)
        {

            ThrustAndSound();
            RotationControls();

        }

	}

    void OnCollisionEnter(Collision collision)
    {

        if (state != State.Alive)
        {
            return;
        }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                SoundOnCollision(loading);
                loadingP.Play();
                state = State.Transcending;
                Invoke("LoadNextScene", 1f);
                break;
            default:
                SoundOnCollision(explosion);
                explosionP.Play();
                state = State.Dying;
                Invoke("DyingReload", 1f);
                break;
        }
    }

    private void SoundOnCollision(AudioClip clip)
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        audioSource.PlayOneShot(clip);
    }

    private void DyingReload()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(nextLevel);
    }

    private void ThrustAndSound()
    {
        if (Input.GetKey(KeyCode.Space))
        { //can thrust while rotating 
            rocketBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

            if (!audioSource.isPlaying)     //no layering of audio
            {
                audioSource.PlayOneShot(mainEngine);
                mainEngineP.Play();
            }

        }

        else
        {
            audioSource.Stop();
            mainEngineP.Stop();
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
