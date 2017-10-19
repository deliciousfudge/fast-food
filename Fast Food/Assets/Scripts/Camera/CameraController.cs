﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject player;

	private Vector3 offset;

	// Use this for initialization
	void Start () {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 18, player.transform.position.z - 20);
		offset = transform.position - player.transform.position;
        offset.x -= 20;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = player.transform.position + offset;
	}
}
