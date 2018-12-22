using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour {

    
    [SerializeField] float period = 2f;
    [SerializeField] Vector3 movemnetVector;
    Vector3 startingPos;

    [SerializeField][Range(0,1)] float movementFactor;


	// Use this for initialization
	void Start () {
        startingPos = transform.position;
        
		
	}
	
	// Update is called once per frame
	void Update () {

        float cycles = Time.time / period; //grows continually from 0;

        const float tau = Mathf.PI * 2;
        float rawSin = Mathf.Sin(cycles * tau);

        movementFactor = rawSin / 2f + 0.5f;
        Vector3 offset = movemnetVector * movementFactor;
        transform.position = startingPos + offset;

	}
}
