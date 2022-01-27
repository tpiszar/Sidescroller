using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject main;
    public GameObject controls;
    public GameObject credits;

    public void LoadLevel(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void Controls()
    {
        main.SetActive(false);
        controls.SetActive(true);
    }

    public void Credits()
    {
        main.SetActive(false);
        credits.SetActive(true);
    }

    public void Back()
    {
        credits.SetActive(false);
        controls.SetActive(false);
        main.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
