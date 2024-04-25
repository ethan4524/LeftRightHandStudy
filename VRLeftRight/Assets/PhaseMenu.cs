using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseMenu : MonoBehaviour
{
    public void SelectPhase(string name) {
        if (name=="1") {
            Debug.Log("Phase 1 Selected");
        }
    }
}
