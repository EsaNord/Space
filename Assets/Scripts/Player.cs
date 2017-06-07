using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private GameManager manager;
	private Rigidbody2D rb;

	private Animator animator;
	public float speed = 700f;
	public float jumpForce  = 200f;
	private bool canJump = false;
	private bool facingRight = true;

	private GameObject[] bodyparts;

	// Use this for initialization
	void Start () {
		manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
		rb = gameObject.GetComponent<Rigidbody2D>();
		//bodyparts = GameObject.FindGameObjectsWithTag("bodyparts");
		animator = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		checkJumping();
		checkFalling();
		checkFacing();
		move();
		takeAction();
	}

	private void takeAction(){
		if(Input.GetKeyDown(KeyCode.PageUp) || Input.GetKeyDown(KeyCode.Q)){
			if(manager.ShipIsRotating == false){
				print("Rotating ship counter-clockwise");
				manager.rotateShip(false);
			}
		}
		else if( Input.GetKeyDown(KeyCode.PageDown) || Input.GetKeyDown(KeyCode.E)){
			if(manager.ShipIsRotating == false){
				print("Rotating ship clockwise");
				manager.rotateShip(true);
			}
		}
		if(Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return) ||
			Input.GetKeyDown(KeyCode.F)){
			print("Action button pressed");
		}
	}

	private void checkFalling(){
		if(rb.velocity.y < -0.1f){
			animator.SetBool("falling", true);
		}
		else {
			animator.SetBool("falling", false);
		}
	}

	private void checkJumping(){
		float originX = transform.localPosition.x - transform.localScale.x/5.5f;
		float originX2 = transform.localPosition.x + transform.localScale.x/5;
		float originX3 = transform.localPosition.x;
		float originY = transform.localPosition.y - transform.localScale.y/2;
		float originY2 = transform.localPosition.y;
		RaycastHit2D hit1, hit2, hit3, hitBox;
		hit1 = Physics2D.Raycast(new Vector2(originX, originY), Vector2.down, 0.55f, 1);
		hit2 = Physics2D.Raycast(new Vector2(originX2, originY), Vector2.down, 0.55f, 1);
		hit3 = Physics2D.Raycast(new Vector2(originX3, originY2), Vector2.down, 0.55f, 1);
		hitBox = Physics2D.BoxCast(new Vector2(originX3, originY), new Vector2(0.5f,0.5f), 0, Vector2.down, 0.2f, 1);
		

		if(hitBox){ //raycasts detect ground -> player can jump
			Debug.DrawLine(new Vector2(originX, originY), new Vector2(originX, originY-0.2f), Color.green);
			Debug.DrawLine(new Vector2(originX2, originY), new Vector2(originX2, originY-0.2f), Color.green);
			canJump = true;
			animator.SetBool("falling", false);
		}
		else{
			canJump = false;
		}
	}

	public void setFacing(bool dir){
		if(!dir){
			if(facingRight){
				facingRight = false;
				Vector3 theScale = transform.localScale;
				theScale.x *= -1f;
				transform.localScale = theScale;
			}
		}
		else {
			if(!facingRight){
				facingRight = true;
				Vector3 theScale = transform.localScale;
				theScale.x *= -1f;
				transform.localScale = theScale;
			}
		}
	}
	public void checkFacing(){
		if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)){
			setFacing(false);
		}
		else if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)){
			setFacing(true);
		}
	}

	private void move(){
		if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)){ //Jump
			if(canJump){
				rb.AddForce(Vector2.up * jumpForce * Time.deltaTime, ForceMode2D.Impulse);
			}
		}
		if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)){ //Move left
			rb.AddForce(Vector2.left * speed * Time.deltaTime);
			if(rb.velocity.x <= -6f){
				rb.velocity = new Vector2(-6f, rb.velocity.y);
			}
			animator.SetBool("moving", true);
		}
		else if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)){ //Move right
			rb.AddForce(Vector2.right * speed * Time.deltaTime);
			if(rb.velocity.x >= 6){
				rb.velocity = new Vector2(6f, rb.velocity.y);
			}
			animator.SetBool("moving", true);
		}
		else{
			rb.velocity = new Vector2(0, rb.velocity.y);
			animator.SetBool("moving", false);
		}
		//print(rb.velocity.x);
	}
}
