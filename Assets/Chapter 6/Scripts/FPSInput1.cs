﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput1 : MonoBehaviour {

    public float gravity = -9.8f;
    public float speed = 6.0f;
    public const float base_Speed = 6.0f;
    private CharacterController _charController;


    private void Awake()
    {
        Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    private void OnDestroy()
    {
        Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    private void OnSpeedChanged(float value)
    {
        speed = base_Speed * value;
    }

    // Use this for initialization
    void Start () {
		_charController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {

        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;

        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed); // limit magnitude to movement speed
        movement.y = gravity;
        movement *= Time.deltaTime;                         // scale per frame
        movement = transform.TransformDirection(movement);  // convert from local to global space   
        _charController.Move(movement); // tell character to move by that vector

    }
}