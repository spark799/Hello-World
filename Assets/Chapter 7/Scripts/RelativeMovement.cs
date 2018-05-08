using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class RelativeMovement : MonoBehaviour {

    [SerializeField] private Transform target;  // the object to remove relative to

    public float rotSpeed = 15.0f;

    public float moveSpeed = 6.0f;

    public float jumpSpeed = 15.0f;
    public float gravity = -9.8f;
    public float terminalVelocity = -10.0f;
    public float minFall = -1.5f;

    private float _vertSpeed;

    private ControllerColliderHit _contact;

    private CharacterController _charController;

    // Use this for initialization
    void Start() {
        _vertSpeed = minFall;
        _charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update() {

        Vector3 movement = Vector3.zero;


        bool hitGround = false;
        RaycastHit hit;

        // check if the raycast hit the ground and player is not jumping
        if(_vertSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            float check = (_charController.height + _charController.radius) / 1.9f;
            hitGround = hit.distance <= check;
        }

        if (hitGround)
        {
            if (Input.GetButton("Jump"))
            {
                _vertSpeed = jumpSpeed;
            }
            else
            {
                _vertSpeed = minFall;   // there needs to be a downward force to go up adn down terrain for physics, thats why not set to 0
            }
        }
        else
        {
            _vertSpeed += gravity * 5 * Time.deltaTime;
            if (_vertSpeed < terminalVelocity)  //make sure it doesnt exceed this velocity, note its < because downward speed is negative
            {
                _vertSpeed = terminalVelocity;
            }

            if (_charController.isGrounded)
            {
                if(Vector3.Dot(movement, _contact.normal) < 0)
                {
                    movement = _contact.normal * moveSpeed;
                }
                else
                {
                    movement += _contact.normal * moveSpeed;
                }
            }
        }

        movement.y = _vertSpeed;

        float horInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");
        if (horInput != 0 || vertInput != 0)
        {
            movement.x = horInput * moveSpeed;
            movement.z = vertInput * moveSpeed;
            movement = Vector3.ClampMagnitude(movement, moveSpeed); // limit diagonal movement to teh same speed as along an axis


            Quaternion tmp = target.rotation;
            target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);
            movement = target.TransformDirection(movement); // transform movement direction from Local to Global directions
            target.rotation = tmp;

            Quaternion direction = Quaternion.LookRotation(movement); // calculates a quaternion facing in that direction
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotSpeed * Time.deltaTime);    // smooth rotation and not instant if just assigned it
        }


        _charController.Move(movement); // move the player after making movement direction to global
    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        _contact = hit;
    }

}
