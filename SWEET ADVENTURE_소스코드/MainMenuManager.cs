using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    public void NewBtn()
    {
        SceneManager.LoadScene("2-0Tutorial");

    }

    public void Exit()
    {
        Application.Quit();
    }

    void GameUIActive()
    {
        GameManager.instance.gameUI.SetActive(true);
    }

}
