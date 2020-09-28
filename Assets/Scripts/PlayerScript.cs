﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
      private float playerSpeed=5f;


[SerializeField]
    private  Animator Animator;
    
[SerializeField]
    private BoxCollider2D boxCollider;

    private Vector2 OriginalSizeCollider,OriginalColliderOffset;
    private Rigidbody2D Playerbody;
    private float resize;
    private bool IsCrouching=false;
    private bool IsGrounded=true;
    private bool jumped;
    [SerializeField]
    private Transform groundCheckPosition;
    [SerializeField]
    private LayerMask groundLayer;
    // Start is called before the first frame update
    void Awake(){
         boxCollider = GetComponent<BoxCollider2D>(); 
         Playerbody = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
       OriginalSizeCollider = boxCollider.size;
       OriginalColliderOffset = boxCollider.offset;
    }

    // Update is called once per frame
    void Update()
    {
        float speed = Input.GetAxisRaw("Horizontal");
        Animator.SetFloat("Speed", Mathf.Abs(speed));
        ChangeDirection(speed);
        Crouch();
        CheckGrounded();
        Jump();
    }

    //Packed all the code in one place to change the direction of Player
    void ChangeDirection(float speed){
         Vector3 scale = transform.localScale; 
        if(speed < 0){   
            Playerbody.velocity = new Vector2(-playerSpeed,Playerbody.velocity.y);
            scale.x = - 1 * Mathf.Abs(scale.x);
           
        } else if(speed > 0){
                Playerbody.velocity = new Vector2(playerSpeed,Playerbody.velocity.y);
                scale.x = Mathf.Abs(scale.x); 
              
        }else{
            Playerbody.velocity= new Vector2(0,Playerbody.velocity.y);
        }
        transform.localScale = scale;
    }

    void Crouch(){
        if(Input.GetKey(KeyCode.LeftControl)){
                IsCrouching= true;
                Animator.SetBool("IsCrouch",true);
              
            }else{
                 IsCrouching= false;
                   
           Animator.SetBool("IsCrouch",false);
                
            }
            SetColliderSize(IsCrouching);
        }

        void SetColliderSize(bool crouchstatus){
            if(IsCrouching==true){
                 boxCollider.size = new Vector2(boxCollider.size.x,1.2f);
             boxCollider.offset = new Vector2(boxCollider.offset.x,.6f);
            }else{
                 boxCollider.size = OriginalSizeCollider; 
                 boxCollider.offset = OriginalColliderOffset;
            }
        }
        void CheckGrounded(){
          IsGrounded = Physics2D.Raycast(groundCheckPosition.position,Vector2.down,0.1f,groundLayer);

          if(IsGrounded){
              if(jumped){
                  jumped = false;
                  Animator.SetBool("IsJump",false);
              }
          }
        }
        void Jump(){
         if(IsGrounded){
             if(Input.GetKey(KeyCode.W)){
                 jumped=true;
                 Animator.SetBool("IsJump",true);
             }
         }
        }
         
    

   
}
