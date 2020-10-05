using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondEnemyController : MonoBehaviour
{
    private float enemyspeed = 1f;
    private Rigidbody2D enemybody;
    private Animator animator;
    private bool moveLeft;



    void Awake(){
        enemybody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        moveLeft=false;
    }

   void Update()
    {

        EnemyMovement();
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.GetComponent<PlayerScript>() != null){
            PlayerScript player= other.gameObject.GetComponent<PlayerScript>();
            player.KillPlayer();
        }

     

       
    }

    void EnemyMovement(){
        if(moveLeft){
            transform.Translate(-1*Time.deltaTime * enemyspeed,0,0);
           // enemybody.velocity = new Vector2(-enemyspeed,enemybody.velocity.y);
        }else{
           transform.Translate(1*Time.deltaTime * enemyspeed,0,0);
        }
    }
   

    
}
