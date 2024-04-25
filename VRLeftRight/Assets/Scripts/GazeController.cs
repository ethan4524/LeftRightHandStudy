using System;
using UnityEngine;

public class GazeController : MonoBehaviour
{
    public float stareDurationThreshold = 0.5f; // Default stare duration threshold
    public float lineLength = 10f; // Length of the line renderer

    private GameObject currentStareObject;
    public GameObject cursor;
    private float stareTimer = 0f;
    private bool isStaring = false;

    private LineRenderer lineRenderer;

    public Transform starePosition;
    public LayerMask interactableLayer;

    void Start()
    {
        // Create a new LineRenderer component
        //lineRenderer = gameObject.AddComponent<LineRenderer>();
        // Set line color to yellow
        //lineRenderer.startColor = Color.yellow;
        //lineRenderer.endColor = Color.yellow;
        // Set line width
        //lineRenderer.startWidth = 0.02f;
        //lineRenderer.endWidth = 0.02f;
        // Ignore line renderer in raycast
        //lineRenderer.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
    }

    void Update()
    {
        RaycastHit hit;
        
        // Cast a ray from the center of the camera's view
        if (Physics.Raycast(starePosition.position, starePosition.forward, out hit, Mathf.Infinity))
        {
            // Update line renderer positions to visualize the gaze direction
            //lineRenderer.SetPosition(0, starePosition.position);
            //lineRenderer.SetPosition(1, hit.point);

            // Check if the hit object is stare-interactable
            cursor.SetActive(true);
            cursor.transform.position = hit.point;
            

            GameObject hitObject = hit.collider.gameObject;
            if (hitObject.CompareTag("StareInteractable"))
            {
                Debug.Log("object detected");
                if (currentStareObject != hitObject)
                {
                    // Reset timer if staring at a new object
                    Debug.Log("object detected");
                    currentStareObject?.GetComponent<StareInteractable>()?.OnStareEnd();
                    currentStareObject = hitObject;
                    stareTimer = 0f;
                    isStaring = true;
                    
                }
                else
                {
                    
                    stareTimer += Time.deltaTime;
                    if (stareTimer >= stareDurationThreshold)
                    {
                        // Notify the interactable object of selection
                        currentStareObject.GetComponent<StareInteractable>()?.OnStareSelect();
                    }
                }
            }
            else
            {
                // Reset timer if not staring at an interactable
                currentStareObject?.GetComponent<StareInteractable>()?.OnStareEnd();
                currentStareObject = null;
                stareTimer = 0f;
                isStaring = false;
            }



        }
        else
        {
            // Reset timer if not hitting anything
            currentStareObject?.GetComponent<StareInteractable>()?.OnStareEnd();
            currentStareObject = null;
            stareTimer = 0f;
            isStaring = false;
            //cursor.SetActive(false);
                    //cursor.transform.position = hit.point;
        }
    }
}
