using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerLifeController : MonoBehaviour
{
    [SerializeField]
    private int health=3;

    [SerializeField]
    private int numofHearts=3;
    
    public Image[] Hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;


    private void Update() {
      CheckHealth();
    }

    public void ReduceHealth(){
        Debug.Log("Player health is reduced");
       // CheckHealth();
        health-=1;
    }


    public bool CheckIfAlive(){
        if(health>0){
            return true;
        }
        return false;
    }

    void CheckHealth(){

        if(health > numofHearts){
            health = numofHearts;
        }
          for(int i = 0; i < Hearts.Length;i++){
            if(i < health){
                Hearts[i].sprite = fullHeart;
            }else{
                Hearts[i].sprite= emptyHeart;
            }
          
          
            if(i < numofHearts){
                Hearts[i].enabled = true;
            }else{
                 Hearts[i].enabled = false;
            }
        }
    }
   
}
