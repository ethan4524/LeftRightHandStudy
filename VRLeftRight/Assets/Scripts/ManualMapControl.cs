using UnityEngine;

public class ManualMapControl : MonoBehaviour
{
     
     public GameObject left;
     public GameObject right;
     public GameObject target;
    public float movementSpeed = 1f; // Speed at which the object moves towards the controller

    public void OnLeftTrigger() {
        // Calculate the position and rotation of the controller
        Vector3 controllerPosition = left.transform.position;
        Quaternion controllerRotation = left.transform.rotation;

        // Move the target object towards the position of the controller
        target.transform.position = controllerPosition;//Vector3.MoveTowards(target.transform.position, controllerPosition, movementSpeed * Time.deltaTime);

        // Rotate the target object to align with the rotation of the controller
        //target.transform.rotation = controllerRotation;
        //target.transform.rotation = Quaternion.LookRotation(Vector3.forward);
        // Calculate the direction from this object to the target object
        Vector3 directionToTarget = target.transform.position - left.transform.position;

        // Reverse the direction
        Vector3 oppositeDirection = -directionToTarget;

        // Project the opposite direction onto the x-z plane (eliminate the y component)
        Vector3 perpendicularDirection = Vector3.ProjectOnPlane(oppositeDirection, Vector3.up);

        // Set the rotation to align with the perpendicular direction while keeping it parallel to the x-z plane
        transform.rotation = Quaternion.LookRotation(perpendicularDirection, Vector3.up); 

    }

    public void OnRightTrigger() {
       // Calculate the position and rotation of the controller
        Vector3 controllerPosition = right.transform.position;
        Quaternion controllerRotation = right.transform.rotation;

        // Move the target object towards the position of the controller
        target.transform.position = controllerPosition;//Vector3.MoveTowards(target.transform.position, controllerPosition, movementSpeed * Time.deltaTime);

        // Rotate the target object to align with the rotation of the controller
        //target.transform.rotation = controllerRotation;
        //target.transform.rotation = Quaternion.LookRotation(Vector3.forward);
        // Calculate the direction from this object to the target object
        Vector3 directionToTarget = target.transform.position - right.transform.position;

        // Reverse the direction
        Vector3 oppositeDirection = -directionToTarget;

        // Project the opposite direction onto the x-z plane (eliminate the y component)
        Vector3 perpendicularDirection = Vector3.ProjectOnPlane(oppositeDirection, Vector3.up);

        // Set the rotation to align with the perpendicular direction while keeping it parallel to the x-z plane
        transform.rotation = Quaternion.LookRotation(perpendicularDirection, Vector3.up); 


    }

}
