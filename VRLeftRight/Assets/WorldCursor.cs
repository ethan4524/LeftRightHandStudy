using UnityEngine;
using UnityEngine.EventSystems;

public class WorldCursor : MonoBehaviour
{
    public LayerMask raycastLayerMask; // Layer mask for the raycast
    public GameObject objectToMove; // Object to move to hit point

    void Update()
    {
        // Cast a ray forward from the GameObject's position
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // Debug raycast to visualize the ray in the scene
        Debug.DrawRay(ray.origin, ray.direction * 10f, Color.blue);

        // Check if the ray hits an object on the specified layer
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, raycastLayerMask))
        {
            objectToMove.SetActive(true);
            // Get the hit point
            Vector3 hitPoint = hit.point;

            // Move the objectToMove to the hit point
            objectToMove.transform.position = hitPoint;

            // Rotate the objectToMove to face the original GameObject
            objectToMove.transform.LookAt(transform);
        }
        else
        {
            objectToMove.SetActive(false); // Hide objectToMove when ray doesn't hit anything
        }
    }
}
