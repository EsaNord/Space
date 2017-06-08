using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {

	private GameManager manager;

    private int trembleCounter = 0;

	private Quaternion startingRotation;
	private float speed = 15f;

	// Use this for initialization
	void Start () {
		manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
		startingRotation = this.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		if(manager.ShipIsRotating == false){
			//return back to the starting rotation
			if( Input.GetKeyUp( KeyCode.Q)){
				StopAllCoroutines();
				StartCoroutine(Rotate(90));
				manager.ShipIsRotating = true;
			}

			//go to 90 degrees with right arrow
			if( Input.GetKeyDown(KeyCode.E)){
				StopAllCoroutines();
				StartCoroutine(Rotate(-90));
				manager.ShipIsRotating = true;
			}
		}
	}
	
	IEnumerator Rotate(float rotationAmount){
		if(trembleCounter < 3){
				trembleCounter++;
				if(trembleCounter == 3)
				{
					trembleCounter = 0;
					transform.Translate(new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0f));
				}
		}

		Quaternion finalRotation = Quaternion.Euler( 0, 0, rotationAmount ) * startingRotation;

		while(this.transform.rotation != finalRotation){
			this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, finalRotation, speed * Time.deltaTime);
			yield return 0;
		}
		transform.rotation = finalRotation;
		startingRotation = this.transform.rotation;
		manager.ShipIsRotating = false;
	}
}
