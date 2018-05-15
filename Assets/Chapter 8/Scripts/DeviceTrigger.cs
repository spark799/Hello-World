using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceTrigger : MonoBehaviour {

    [SerializeField]
    private GameObject[] targets;   // list of gameobjects that you want to activate/ deactivate


    void OnTriggerEnter(Collider other)
    {
        // for each targets do activate function
        foreach(GameObject target in targets)
        {
            target.SendMessage("Activate");
        }
    }


    void OnTriggerExit(Collider other)
    {
        // for each targets do deactivate function
        foreach (GameObject target in targets)
        {
            target.SendMessage("Deactivate");
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
