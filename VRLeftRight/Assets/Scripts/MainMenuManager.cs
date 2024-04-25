using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenuManager : MonoBehaviour
{
    public Image exitButton;


    public void ExitGame()
    {
        exitButton.color = Color.yellow;
        // Quit the application
        Application.Quit();
    }

}
