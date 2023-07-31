using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManagement.Instance.Load("Level_1");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
