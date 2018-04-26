using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour {


    [SerializeField]
    private GameObject enemyPrefab; // serialized variable for linking to the prefab object

    private GameObject _enemy;      // keep track if the instance, if its still there are not
	
	// Update is called once per frame
	void Update () {

		if(_enemy == null)  // If an enemy instance doesn't exist anymore  
        {
            _enemy = Instantiate(enemyPrefab) as GameObject;    // instantiate a new enemy using the prefab, cast as gameobject and store reference to it
            _enemy.transform.position = new Vector3(0, 1, 0);   // spawn it at a location
            float angle = Random.Range(0, 360);                 // Give it a random direction to start moving
            _enemy.transform.Rotate(0, angle, 0);
        }
	}
}
