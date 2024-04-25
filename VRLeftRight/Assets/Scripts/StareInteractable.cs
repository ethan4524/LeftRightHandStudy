using UnityEngine;
using UnityEngine.Events;

public class StareInteractable : MonoBehaviour
{
    public UnityEvent onStareSelect; // Event to invoke when stared at

    // Method to be called when the object is stared at for a certain duration
    public void OnStareSelect()
    {
        // Invoke assigned method(s) when stared at
        onStareSelect.Invoke();
        Debug.Log("****************************************************");
    }

    // Method to be called when the stare ends (player looks away)
    public void OnStareEnd()
    {
        // Implement any necessary cleanup or reset here
    }
}
