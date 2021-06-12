using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform Target;

    private float zoomSpeed = 2f;
    // private Vector3 initialRotation;
    
    
    void Start()
    {
        // initialRotation = transform.rotation.eulerAngles;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        AngleAndLookAtPlayer();
    }

    void Update()
    {
        //ViewObstructed();
    }

    private void LateUpdate()
    {
        AngleAndLookAtPlayer();
        // transform.Rotate(initialRotation);
        // transform.LookAt(Target);
        
        // try to continually set the camera angle (how????)

        // ViewObstructed();
    }
    
    private void AngleAndLookAtPlayer()
    {
        Vector3 cameraPosition = transform.position;
        Vector3 playerPosition = Target.position;
        
        float angleX = Vector3.SignedAngle(cameraPosition, playerPosition, Vector3.right);
        float angleY = Vector3.SignedAngle(cameraPosition, playerPosition, Vector3.up);
        float angleZ = Vector3.SignedAngle(cameraPosition, playerPosition, Vector3.forward);
        
        Debug.Log("angleX: " + angleX);
        Debug.Log("angleY: " + angleY);
        Debug.Log("angleZ: " + angleZ);
        
        transform.LookAt(Target);
        
        Debug.Log("=================");
        
    }

    void ViewObstructed()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Target.position - transform.position, out hit, 4.5f))
        {
            if (hit.collider.gameObject.tag != "Player")
            {
                Target = hit.transform;
                Target.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                
                if(Vector3.Distance(Target.position, transform.position) >= 3f && Vector3.Distance(transform.position, Target.position) >= 1.5f)
                    transform.Translate(Vector3.forward * zoomSpeed * Time.deltaTime);
            }
            else
            {
                Target.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                if (Vector3.Distance(transform.position, Target.position) < 4.5f)
                    transform.Translate(Vector3.back * zoomSpeed * Time.deltaTime);
            }
        }
    }
}