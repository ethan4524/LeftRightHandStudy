using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class LeftRightGenerator : MonoBehaviour
{
    public GameObject leftHandModel;
    public GameObject rightHandModel;
    public Animator leftHandAnimator;
    public Animator rightHandAnimator;

    Vector2 lastAnimState;


    private void Start() {
        HideHands();
        lastAnimState = new Vector2(-5000,-5000);
    }
    public string GenerateRandomDirection() {
        int r = UnityEngine.Random.Range(0,2);
        // Generate a random rotation around the x-axis
        float randomXRotation = UnityEngine.Random.Range(0f, 360f);
        // Apply the rotation
        
        if (r==0) {
            //left
            leftHandModel.SetActive(true);
            lastAnimState = new Vector2(UnityEngine.Random.Range(-1f, 1f),UnityEngine.Random.Range(-1f, 1f));
            leftHandAnimator.SetFloat("LeftHandBlendX", lastAnimState.x);
            leftHandAnimator.SetFloat("LeftHandBlendY", lastAnimState.y);
            
            leftHandModel.transform.rotation = Quaternion.Euler(randomXRotation, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            return "Left";
        } else {
            //right
            rightHandModel.SetActive(true);
            lastAnimState = new Vector2(UnityEngine.Random.Range(-1f, 1f),UnityEngine.Random.Range(-1f, 1f));
            rightHandAnimator.SetFloat("RightHandBlendX", lastAnimState.x);
            rightHandAnimator.SetFloat("RightHandBlendY", lastAnimState.y);
            
            rightHandModel.transform.rotation = Quaternion.Euler(randomXRotation, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            return "Right";
        }
    }

    public Vector2 GetAnimatorState(string hand) {
        return lastAnimState;
    }

    public void HideHands() {
        leftHandModel.SetActive(false);
        rightHandModel.SetActive(false);
    }
}
