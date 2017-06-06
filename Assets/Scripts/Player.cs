﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[SerializeField] public GameManager manager;
	private Rigidbody2D rb;

	private Animator animator;
	public float speed = 700f;
	public float jumpForce  = 400f;
	private bool canJump = false;

	// Use this for initialization
	void Start () {
		manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
		rb = gameObject.GetComponent<Rigidbody2D>();
		animator = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		checkJumping();
		move();
		takeAction();
	}

	private void takeAction(){
		if(Input.GetKeyDown(KeyCode.PageDown) || Input.GetKeyDown(KeyCode.Q)){
			print("Rotating ship counter-clockwise");
			manager.rotateShip(false);
		}
		else if( Input.GetKeyDown(KeyCode.PageUp) || Input.GetKeyDown(KeyCode.E)){
			print("Rotating ship clockwise");
			manager.rotateShip(true);
		}
		if(Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return) ||
			Input.GetKeyDown(KeyCode.F)){
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

	public void animate(bool dir){
		if(!dir){
			animator.SetBool("moving", true);
			foreach(Transform child in transform){
				if(child.GetComponent<SpriteRenderer>() != null){
					child.GetComponent<SpriteRenderer>().flipX = true;
				}
				
			}
		}
		else {
			animator.SetBool("moving", true);
			foreach(Transform child in transform){
				if(child.GetComponent<SpriteRenderer>() != null){
					child.GetComponent<SpriteRenderer>().flipX = false;
				}
			}
		}
	}
	private void move(){
		if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)){ //Jump
			if(canJump){
				rb.AddForce(Vector2.up * jumpForce * Time.deltaTime, ForceMode2D.Impulse);
			}
		}
		if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)){ //Move left
			animate(false);
			rb.AddForce(Vector2.left * speed * Time.deltaTime);
			if(rb.velocity.x <= -6f){
				rb.velocity = new Vector2(-6f, rb.velocity.y);
			}
		}
		else if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)){ //Move right
			animate(true);
			rb.AddForce(Vector2.right * speed * Time.deltaTime);
			if(rb.velocity.x >= 6){
				rb.velocity = new Vector2(6f, rb.velocity.y);
			}
		}
		else{
			rb.velocity = new Vector2(0, rb.velocity.y);
			animator.SetBool("moving", false);
		}
		print(rb.velocity.x);
	}
}
