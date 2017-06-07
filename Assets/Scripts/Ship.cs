using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {

	private GameManager manager;

	private float targetRotation;

	private float angle = 90f;
	private float rotation = 15f;
	private bool rotationDirection = false;

	// Use this for initialization
	void Start () {
		manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void startRotating(bool dir){	
			if(!dir){
				targetRotation = transform.rotation.z - angle;
				transform.RotateAround(transform.localPosition, Vector3.back , -angle * Time.deltaTime);
			}
			else
			{
				transform.RotateAround(transform.localPosition, Vector3.back , angle * Time.deltaTime);
				targetRotation = transform.rotation.z + angle;
			}
			//StartCoroutine(rotate(rotationDirection, targetRotation));
			
	}/* 
	public IEnumerator rotate(bool dir, float target){
		if(!dir){			
			while(transform.rotation.z > target){
				manager.ShipIsRotating = true;
				transform.RotateAround(transform.localPosition, Vector3.back , -rotation * Time.deltaTime);
				yield return null;	
			}
		}
		else {
			manager.ShipIsRotating = true;
			targetRotation = transform.rotation.z + angle;
			while(transform.rotation.z < target){
				transform.RotateAround(transform.localPosition, Vector3.forward, rotation * Time.deltaTime);
				yield return null;	
			}
		}
		manager.ShipIsRotating = false;
		yield return true;		
	}*/
}
