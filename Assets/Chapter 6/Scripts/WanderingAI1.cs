﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI1 : MonoBehaviour {

    public float speed = 3.0f;
    public float obstacleRange = 5.0f;
    public bool _alive; // To tell if the character is in alive or dead state
    public const float base_Speed = 3.0f;

    [SerializeField]
    private GameObject fireballPrefab;  // reference to prefab
    private GameObject _fireball;       // instance of prefab


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
        _alive = true; // Initiate character as alive
	}
	
	// Update is called once per frame
	void Update () {

        if (_alive) // only do this if alive
        {
            transform.Translate(0, 0, speed * Time.deltaTime);  // Move forward continuously every frame

            Ray ray = new Ray(transform.position, transform.forward); // transform.postition makes the ray follow the character and transfrom.forward makes it so the ray is always in front of the character even if it turns around
            RaycastHit hit;
            if (Physics.SphereCast(ray, 0.75f, out hit))     // Different than Physics.Raycast, SphereCast will detect how far around the ray with the given radius there is an intersection
            {
                GameObject hitObject = hit.transform.gameObject; ;                                      // Get what was hit by the raycast

                if (hitObject.GetComponent<PlayerCharacter>())                                          // check if it was the player  character
                {
                    if (_fireball == null)
                    {
                        _fireball = Instantiate(fireballPrefab) as GameObject;                          // spawn the prefab
                        _fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);// and spawn in infront of enemy
                        _fireball.transform.rotation = transform.rotation;                              // rotate it to the same direction as enemy
                    }
                }
                else if (hit.distance < obstacleRange)           // If an obstacle (anything that the ray interescts) is in range of the set distance, then turn around
                {
                    float angle = Random.Range(-110, 110); // Create a random angle to turn the character
                    transform.Rotate(0, angle, 0);         // Rotate the character that angle
                }
            }
        }
	}

    // Change the alive state
    public void SetAlive(bool alive)
    {
        _alive = alive;
    }
}
