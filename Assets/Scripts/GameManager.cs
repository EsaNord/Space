using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	[SerializeField] public Player player;
	[SerializeField] public Ship ship;
	private GameManager manager;

	private bool gameOver = false;
	private bool shipIsRotating = false;

	public bool GameOver {get {return gameOver;} set{gameOver=value;}}
	public bool ShipIsRotating {get {return shipIsRotating;} set{shipIsRotating = value;}}

	private AudioListener audioListener;
	[SerializeField] private AudioClip sfxShipRotate;


	void Start () {
		manager = this;
		audioListener = GameObject.FindObjectOfType<AudioListener>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void rotateShip(bool dir){
			//ship.startRotating(dir);
	}
}
