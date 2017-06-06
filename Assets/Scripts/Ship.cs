using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {

	[SerializeField] public GameManager manager;

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
			while(manager.ShipIsRotating && transform.rotation.z > (transform.rotation.z - angle)){
				transform.RotateAround(transform.GetComponent<Renderer>().bounds.center, Vector3.back , rotation * Time.deltaTime);
				yield return null;	
			}
		}
		else {
			while(manager.ShipIsRotating && transform.rotation.z < (transform.rotation.z + angle)){
				transform.RotateAround(transform.GetComponent<Renderer>().bounds.center, Vector3.forward, rotation * Time.deltaTime);
				yield return null;	
			}
		}
		manager.ShipIsRotating = false;
		yield return true;		
	}
}
