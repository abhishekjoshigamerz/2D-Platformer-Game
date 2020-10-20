using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverController : MonoBehaviour
{
    // Start is called before the first frame update
   public void MainMenu(){
       SceneManager.LoadScene(0);
   }

   public void ReplayMenu(){
       SceneManager.LoadScene(2);
   }
}
