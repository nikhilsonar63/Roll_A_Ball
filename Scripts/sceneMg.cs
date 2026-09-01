using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneMg : MonoBehaviour
{
    public TMP_Text cointx;
    // Start is called before the first frame update
    void Start()
    {
        cointx.text = PlayerPrefs.GetInt("Coine").ToString();
    }

    // Update is called once per frame
    public void play_button()
    {
        SceneManager.LoadScene(1);
    }
    public void level()
    {
        SceneManager.LoadScene(6);
    }
    public void Quit()
    {
        Application.Quit();
    }

 
}
