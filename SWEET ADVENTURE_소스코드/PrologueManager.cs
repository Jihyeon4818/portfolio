using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Prologue
{
    //[TextArea]
    //public string prologues;
    public Image cutToonImage;
    public Text text;
}
public class PrologueManager : MonoBehaviour
{
    //public SpriteRenderer cutToon;
    //public Text text_Story;
    //public Image cutToon2;

    int count = 0;

    public Prologue[] prologue;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            NextPrologue();
        }
    }

    public void NextPrologue()
    {
        if (count < prologue.Length)
        {
            //text_Story.text = prologue[count].prologues;
            //cutToon2.sprite = prologue[count].cutToonImage;
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
            SceneManager.LoadScene(1);
        }
    }
    
    public void Skip()
    {
        SceneManager.LoadScene(1);
    }
}
