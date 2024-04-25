using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LoadingButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image loadingCircle;
    public bool isHovering = false;
    public float fillSpeed = 2f; // Speed at which the loading circle fills up (fillAmount per second)
    public float fillAmount = 0f; // Current fill amount of the loading circle

    void Start()
    {
        loadingCircle = GetComponent<Image>();
        loadingCircle.fillAmount = 0f;
    }

    void Update()
    {
        if (isHovering)
        {
            FillLoadingCircle();
        }
        else
        {
            ResetLoadingCircle();
        }
    }

    void FillLoadingCircle()
    {
        fillAmount += Time.deltaTime;
        loadingCircle.fillAmount = Mathf.Clamp01(fillAmount);

        if (fillAmount >= 1f)
        {
            // Perform action when loading is complete
            Debug.Log("Loading Complete!");
            // Call button onClick event or other action here
            // For example, you can trigger a Unity Event:
            //GetComponent<Button>().onClick.Invoke();
            // Reset loading circle
            ResetLoadingCircle();
        }
    }

    void ResetLoadingCircle()
    {
        fillAmount = 0f;
        loadingCircle.fillAmount = 0f;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
    }
}
