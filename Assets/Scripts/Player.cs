using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private Rigidbody2D rb;
	public float speed = 10f;
	public float jumpForce  = 100f;
	private bool canJump = false;

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		checkJumping();
		move();
		takeAction();
	}

	private void takeAction(){
		if(Input.GetKeyDown(KeyCode.PageDown)){
			print("Rotating ship counter-clockwise");
		}
		else if( Input.GetKeyDown(KeyCode.PageUp)){
			print("Rotating ship clockwise");
		}
		if(Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return)){
			print("Action button pressed");
		}
	}

	private void checkJumping(){
		float originX = transform.position.x - transform.localScale.x/2;
		float originX2 = transform.position.x + transform.localScale.x/2;
		float originY = transform.position.y - transform.localScale.y/2;
		RaycastHit2D hit1, hit2;
		hit1 = Physics2D.Raycast(new Vector2(originX, originY), Vector2.down, 0.5f);
		hit2 = Physics2D.Raycast(new Vector2(originX2, originY), Vector2.down, 0.5f);

		if(hit1 || hit2){ //raycasts detect ground -> player can jump
			canJump = true;
		}
		else{
			canJump = false;
		}
	}

	private void move(){
		if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)){ //Jump
			if(canJump){
				rb.AddForce(Vector2.up * jumpForce * Time.deltaTime, ForceMode2D.Impulse);
			}
		}
		else if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)){ //Move left
			rb.MovePosition((transform.position + Vector3.left * speed * Time.deltaTime));
		}
		else if(Input.GetKey(KeyCode.S)){
			
		}
		else if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)){ //Move right
			rb.MovePosition((transform.position + Vector3.right  * speed * Time.deltaTime));
		}
	}
}
