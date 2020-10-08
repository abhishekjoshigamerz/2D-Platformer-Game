using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenuController : MonoBehaviour
{
    public Button buttonPlay;
    public GameObject LevelSelection;

    public void PlayButton(){
        //SceneManager.LoadScene(2);
        LevelSelection.SetActive(true);  
    }

    public void ExitButton(){
        Application.Quit();
    }
}
