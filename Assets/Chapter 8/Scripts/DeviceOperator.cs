using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceOperator : MonoBehaviour {


    public float radius = 1.5f;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Fire3"))
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);    // returns a list of nearby objects

            foreach(Collider collider in hitColliders)
            {
                Vector3 direction = collider.transform.position - transform.position;

                if (Vector3.Dot(transform.forward, direction) > .5f)
                {
                    collider.SendMessage("Operate", SendMessageOptions.DontRequireReceiver);    // tries to call the named function, regardless of target's type

                }
            }


        }
        	
	}
}
