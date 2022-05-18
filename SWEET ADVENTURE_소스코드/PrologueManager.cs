using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

[Serializable]
public class Prologue
{
    public Image cutToonImage;
    public Text text;
}
public class PrologueManager : MonoBehaviour
{
    int count = 0;

    public Prologue[] prologue;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            NextPrologue();
        }
    }

    void NextPrologue()
    {
        if (count < prologue.Length)
        {
            if(prologue[count].cutToonImage != null)
            {
                prologue[count].cutToonImage.gameObject.SetActive(true);
            }
            prologue[count].text.gameObject.SetActive(true);
            for(int i=0; i<count; i++)
            {
                prologue[i].text.gameObject.SetActive(false);
            }
            count++;
        }
        else
        {
            Skip();
        }
    }
    
    void Skip()
    {
        SceneManager.LoadScene(1);
    }
}
