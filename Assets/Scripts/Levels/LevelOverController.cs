using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelOverController : MonoBehaviour
{
    int nextSceneIndex;

     private void OnTriggerEnter2D(Collider2D collision) {
         if(collision.gameObject.GetComponent<PlayerScript>() !=null){
             LevelManager.Instance.MarkCurrentLevelComplete();
             nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
             SceneManager.LoadScene(nextSceneIndex);
         }
    }
}
