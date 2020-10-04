using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerDeath : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.GetComponent<PlayerScript>() != null){
           PlayerScript player = other.gameObject.GetComponent<PlayerScript>();
           player.KillPlayer();
        }
    }
}
