using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerScript : MonoBehaviour
{
      private float playerSpeed=5f;


[SerializeField]
    private  Animator Animator;
    
[SerializeField]
    private BoxCollider2D boxCollider;
[SerializeField]
private BoxCollider2D fall;
private bool isDeath;
    private Vector2 OriginalSizeCollider,OriginalColliderOffset;
    private Rigidbody2D Playerbody;
    private float resize;
    private bool IsCrouching=false;
    private bool IsGrounded=true;
    private bool jumped;
    [SerializeField]
    private float jumpdistance;
    [SerializeField]
    private Transform groundCheckPosition;
    [SerializeField]
    private LayerMask groundLayer;
    Scene thisScene;
     
    // Start is called before the first frame update
    void Awake(){
         boxCollider = GetComponent<BoxCollider2D>(); 
         Playerbody = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
      isDeath=false;
       OriginalSizeCollider = boxCollider.size;
       OriginalColliderOffset = boxCollider.offset;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Jump");
        Animator.SetFloat("Speed", Mathf.Abs(horizontal));
        ChangeDirection(horizontal);
       
        Crouch();
        CheckGrounded();
        Jump(vertical);
        
       
        CheckPlayerStatus();
    }
    void CheckPlayerStatus(){
        if(isDeath){
             thisScene = SceneManager.GetActiveScene();
             SceneManager.LoadScene(thisScene.name);
        }
    }

   
    void MovePlayer(float horizontal){
        Vector3 position = transform.position;
        position.x= position.x + horizontal * playerSpeed * Time.deltaTime;
        transform.position=position;
    }

   

    //Packed all the code in one place to change the direction of Player
    void ChangeDirection(float speed){
         Vector3 scale = transform.localScale; 
        if(speed < 0){   
             MovePlayer(speed);
            scale.x = - 1 * Mathf.Abs(scale.x);
           
        } else if(speed > 0){
                 MovePlayer(speed);
                scale.x = Mathf.Abs(scale.x); 
              
        }else{
           // Playerbody.velocity= new Vector2(0,Playerbody.velocity.y);
           // I used Rigidbody to move the player previously
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
        void Jump(float vertical){
         if(IsGrounded){
             if(vertical>0){
                 jumped=true;
                 Playerbody.velocity = new Vector2(Playerbody.velocity.x,jumpdistance);
               
                 Animator.SetBool("IsJump",true);
             }
         }
        }
         
    

   
}
