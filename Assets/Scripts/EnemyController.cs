using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
   [SerializeField]
    private float enemyspeed = 1f;
    private Rigidbody2D enemybody;
    private Animator animator;
    private bool moveLeft;

    
    [SerializeField]
    private Transform DownCollision;
    void Awake(){
        enemybody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        moveLeft=true;
    }

    
    void Update()
    {

        EnemyMovement();
        CheckCollision();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.GetComponent<PlayerScript>() != null){
            PlayerScript player= other.gameObject.GetComponent<PlayerScript>();
            PlayerLifeController playerlife = other.gameObject.GetComponent<PlayerLifeController>();
            playerlife.ReduceHealth();
            if(!playerlife.CheckIfAlive()){
                player.KillPlayer();
            }
           
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
    void CheckCollision(){
       if(!Physics2D.Raycast(DownCollision.position,Vector2.down,0.1f)){
           ChangeDirection();
       }
    }

    void ChangeDirection(){
        moveLeft = !moveLeft;
        Vector3 tempScale = transform.localScale;
        if(moveLeft){
            tempScale.x = -Mathf.Abs(tempScale.x);
        }else{
             tempScale.x = Mathf.Abs(-tempScale.x);
        }
        transform.localScale = tempScale;
    }
}
