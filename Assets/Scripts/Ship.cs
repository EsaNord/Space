using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {

	private GameManager manager;

	private float targetRotation;

	private float angle = 90f;
	private float rotation = 5f;
	private bool rotationDirection = false;

	// Use this for initialization
	void Start () {
		manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void startRotating(bool dir){
		StartCoroutine(rotate(rotationDirection));
	}
	public IEnumerator rotate(bool dir){
		if(!dir){
			targetRotation = transform.rotation.z - angle;
			while(transform.rotation.z > targetRotation){
				transform.RotateAround(transform.GetComponent<Renderer>().bounds.center, Vector3.back , rotation * Time.deltaTime);
				yield return null;	
			}
		}
		else {
			targetRotation = transform.rotation.z + angle;
			while(transform.rotation.z < targetRotation){
				transform.RotateAround(transform.GetComponent<Renderer>().bounds.center, Vector3.forward, rotation * Time.deltaTime);
				yield return null;	
			}
		}
		manager.ShipIsRotating = false;
		yield return true;		
	}
}
