using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour {


    [SerializeField] private Transform target;  // player reference

    public float rotSpeed = 1.5f;

    private float _rotY;
    private Vector3 _offSet;

	// Use this for initialization
	void Start () {
        _rotY = transform.eulerAngles.y; ;
        _offSet = target.position - transform.position;
	}


    private void LateUpdate()
    {
        float horInput = Input.GetAxis("Horizontal");
        if(horInput != 0)
        {
            _rotY += horInput * rotSpeed;   // rotate using arrow keys
        }
        else
        {
            _rotY += Input.GetAxis("Mouse X") * rotSpeed * 3;   // or rotate using mouse
        }

        Quaternion rotation = Quaternion.Euler(0, _rotY, 0);
        transform.position = target.position - (rotation * _offSet); // offset the camera while following the targets position
        transform.LookAt(target);   // always face the target
    }

    // Update is called once per frame
    void Update () {
		
	}
}
