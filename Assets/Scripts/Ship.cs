using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {

	private GameManager manager;

	private float targetRotation;

	private float angle = 90f;
	private float rotation = 15f;
	private bool rotationDirection = false;
    private int trembleCounter = 0;

	// Use this for initialization
	void Start () {
		manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if(manager.ShipIsRotating){
			rotate(targetRotation);
		}
	}

	public void startRotating(bool dir){
		print("at startRotating()");	
			rotationDirection = dir;
			
			if(!dir){
				targetRotation = transform.eulerAngles.z - angle;
				if(targetRotation < 0){
					targetRotation += 360;
					transform.RotateAround(transform.localPosition, Vector3.forward , 1f * Time.deltaTime);
				}
				//transform.RotateAround(transform.localPosition, Vector3.back , -angle * Time.deltaTime);
			}
			else
			{
				//transform.RotateAround(transform.localPosition, Vector3.back , angle * Time.deltaTime);
				targetRotation = transform.eulerAngles.z + angle;
			}
			print("TargetRotation: " + targetRotation);
			if(manager.ShipIsRotating == false){
				rotate(targetRotation);
				manager.ShipIsRotating = true;
				//StartCoroutine(rotate(dir, targetRotation));	
			}			
	}

	private void rotate(float targetAngle){
		print("current: "+transform.eulerAngles.z);
		print("destination: "+targetAngle);
		print("direction: "+rotationDirection);
		if(!rotationDirection){
				if(transform.eulerAngles.z >= targetAngle){
					transform.RotateAround(transform.localPosition, Vector3.back , rotation * Time.deltaTime);
				}
				else{
					transform.rotation = Quaternion.Euler(0f, 0f, targetAngle);
					manager.ShipIsRotating = false;
				}
		}
		else {
			if(transform.eulerAngles.z <= targetAngle){
				transform.RotateAround(transform.localPosition, Vector3.forward , rotation * Time.deltaTime);
				
				//korjaa kulmaa
			}
			else{
				transform.rotation = Quaternion.Euler(0f, 0f, targetAngle);
				manager.ShipIsRotating = false;
			}
		}
        if(trembleCounter < 3)
        {
            trembleCounter++;
            if(trembleCounter == 3)
            {
                trembleCounter = 0;
                transform.Translate(new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0f));
            }
        }
		print("current: "+transform.eulerAngles.z);
	}
	/*public IEnumerator rotate(bool dir, float target){
		print("at ENum rotate()");
		if(!dir){
			manager.ShipIsRotating = true;			
			while(transform.rotation.z <= target){
				
				transform.RotateAround(transform.localPosition, Vector3.back , -1.0f*rotation * Time.deltaTime);
				yield return null;	
			}
		}
		else {
			manager.ShipIsRotating = true;
			while(transform.rotation.z >= target){
				transform.RotateAround(transform.localPosition, Vector3.forward, rotation * Time.deltaTime);
				yield return null;	
			}
		}
		manager.ShipIsRotating = false;
		yield return true;		
	}*/
}
