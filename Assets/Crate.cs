﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour {

    private Rigidbody2D rigid;
    private AudioSource audio;

	// Use this for initialization
	void Start () {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        audio = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if(rigid.velocity.x > 0.1f || rigid.velocity.y > 0.1f)
        {
            if (!audio.isPlaying) {
                audio.Play();
            }
        }
    }
}
