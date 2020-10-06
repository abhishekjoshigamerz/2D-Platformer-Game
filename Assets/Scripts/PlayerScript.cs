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
    [SerializeField]
    private ScoreController scoreController;
    
    private IEnumerator coroutine;
    private bool dead=false; 
    // Start is called before the first frame update

    public int SceneIndex;
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
        SceneIndex = SceneManager.GetActiveScene().buildIndex;
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Jump");
        Animator.SetFloat("Speed", Mathf.Abs(horizontal));
        ChangeDirection(horizontal);  
        Crouch();
        CheckGrounded();
        Jump(vertical);
        ReloadGame();
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
                // Playerbody.AddForce(new Vector2(0f,20),ForceMode2D.Force);
               Playerbody.velocity = new Vector2(Playerbody.velocity.x,jumpdistance);
                //Playerbody.AddForce();
                 Animator.SetBool("IsJump",true);
                  
             }
         }
        }
        public void KillPlayer(){
            Animator.SetBool("IsDead",true);
            
            dead=true;
        }  


        public void PickUpKey(){
            Debug.Log("Player picked up the key!");
            scoreController.IncreaseScore(10);
        }

        private void ReloadGame(){
            if(dead){
                Animator.SetBool("IsDead",true);
                coroutine=waitThreeSeconds(2.0f); 
                StartCoroutine(coroutine);
            }
          
        }

        private IEnumerator waitThreeSeconds(float waitTime){
             yield return new WaitForSeconds(waitTime);
             SceneManager.LoadScene(1);
         }
   
}
