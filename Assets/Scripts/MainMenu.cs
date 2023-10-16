using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject StartButton;
    public GameObject ExitButton;

    public void StartMenuButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("BoardGame");
    }

    public void ExitMenuButton()
    {
        Application.Quit();
    }
}
