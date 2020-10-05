using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
     private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.GetComponent<PlayerScript>() != null){
            PlayerScript playerscript = collision.gameObject.GetComponent<PlayerScript>();
            playerscript.PickUpKey();
            Destroy(gameObject);
        }
    }
}
