using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {

    public float speed = 10.0f;
    public int damage = 1;
	
	// Update is called once per frame
	void Update () {
        transform.Translate(0, 0, speed * Time.deltaTime);                      // Move forward constantly
	}


    void OnTriggerEnter(Collider other)
    {
        PlayerCharacter player = other.GetComponent<PlayerCharacter>();         // Get what the fireball collided with

        if(player != null)                                                       // Check if the it was the player character
        {
            player.Hurt(1);
        }
        Destroy(this.gameObject);                                               // destroy the gameobject this script is attached to
    }

}
