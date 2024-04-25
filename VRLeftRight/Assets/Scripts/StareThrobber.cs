using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StareThrobber : MonoBehaviour
{
    private RectTransform rectComponent;
    private Image imageComp;

    void Start()
    {
        rectComponent = GetComponent<RectTransform>();
        imageComp = rectComponent.GetComponent<Image>();
        imageComp.fillAmount = 0.0f;
    }

    public void SetFillAmount(float _amount)
    {
        imageComp.fillAmount = _amount;
    }
}
