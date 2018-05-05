using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class RayShooter1 : MonoBehaviour {

    private Camera _camera;

	// Use this for initialization
	void Start () {
        _camera = GetComponent<Camera>();

        //Cursor.lockState = CursorLockMode.Locked; //Lock cursor from moving across the screen
        //Cursor.visible = false;                    // Make it not visible

    }
    

    void OnGUI()
    {
        int size = 12;
        float posX = _camera.pixelWidth / 2 - size / 4;
        float posY = _camera.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), "*");       // Displays a label with text on the screen a coordinates
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);    // Center of screen is half the widith and half the height
            Ray ray = _camera.ScreenPointToRay(point);                                          // Create a ray
            RaycastHit hit;                                                                     // Used to store reference to the object that the ray may hit
            if(Physics.Raycast(ray, out hit))                                                   // Check if and get what the ray hit 
            {
                Debug.Log("Hit " + hit.point);
                
                GameObject hitObject = hit.transform.gameObject; // Retrieve the object the ray hit
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();

                if (target != null) // If it is a enemy then do the enemy reaction function
                {
                    Debug.Log("Target Hit");
                    target.ReactToHit();
                    Messenger.Broadcast(GameEvent.ENEMY_HIT);
                }
                else                // else just show where you raycasted
                {
                    StartCoroutine(SphereIndicator(hit.point));                                     // call coroutine

                }


            }
        }

	}

    private IEnumerator SphereIndicator (Vector3 pos)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);   // create a sphere
        sphere.transform.position = pos;                                        // move the sphere to the raycast intersection

        yield return new WaitForSeconds(1);                                     // pause for 1 second

        Destroy(sphere);                                                        // destroy the sphere
    } 
}
