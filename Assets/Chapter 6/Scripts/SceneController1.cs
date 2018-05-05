using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController1 : MonoBehaviour {


    [SerializeField]
    private GameObject enemyPrefab; // serialized variable for linking to the prefab object

    private GameObject _enemy;      // keep track if the instance, if its still there are not

    public const float base_Speed = 3.0f;
    public float speed = 3.0f;


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

    // Update is called once per frame
    void Update () {

		if(_enemy == null)  // If an enemy instance doesn't exist anymore  
        {
            _enemy = Instantiate(enemyPrefab) as GameObject;    // instantiate a new enemy using the prefab, cast as gameobject and store reference to it
            _enemy.transform.position = new Vector3(0, 1, 0);   // spawn it at a location
            float angle = Random.Range(0, 360);                 // Give it a random direction to start moving
            _enemy.transform.Rotate(0, angle, 0);
            _enemy.GetComponent<WanderingAI1>().speed = speed;
        }
	}
}
