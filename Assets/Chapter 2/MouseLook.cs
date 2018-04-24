using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour {

    public enum RotationAxes
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }

    public float sensitivityHor = 9.0f;
    public float sensitivityVer = 9.0f;

    public float miniimumVert = -45.0f;
    public float maximumVert = 45.0f;

    private float _rotationX = 0;

    public RotationAxes axes = RotationAxes.MouseXAndY;
	
    void Start()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        if(body != null)
        {
            body.freezeRotation = true;
        }
    }

	// Update is called once per frame
	void Update () {
		
        if(axes == RotationAxes.MouseX)
        {
            // horizontal rotation here
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);
        }
        else if(axes == RotationAxes.MouseY)
        {
            // vertical rotation here
            // Instead of using transform.Rotate, it is assigning the new rotation directly after calculating 
            // the new angle and then creating a new vector and then assigning it because we are limiting how far it can rotate vertically.
            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVer;   
            _rotationX = Mathf.Clamp(_rotationX, miniimumVert, maximumVert);    // Limit the rotation between max and min

            float rotationY = transform.localEulerAngles.y;                     // Get the same horizontal position
            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0); // Assign its new rotation directly
        }
        else
        {
            // both horizontal and vertical rotation here

            // Calculating vertical movement here
            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVer;
            _rotationX = Mathf.Clamp(_rotationX, miniimumVert, maximumVert);

            // Calculating the horizontal movement here
            float delta = Input.GetAxis("Mouse X") * sensitivityHor;
            float rotationY = transform.localEulerAngles.y + delta;

            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);



            //transform.Rotate(0, 0, 0);
        }


	}
}
