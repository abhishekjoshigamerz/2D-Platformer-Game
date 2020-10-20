using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Sounds{
    ButtonClick,
    PlayerMove,
    PlayerDeath,
    EnemyDeath,
    Music
}
public class SoundManager : MonoBehaviour
{
     private static SoundManager instance;
    public static SoundManager Instance { get { return instance; } }
    private bool IsMute = false;
    public SoundType[] Sounds;

    public AudioSource soundEffect;
    public AudioSource soundMusic;
    private void Awake() {
        if(instance==null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }
    private void Start() {
        PlayMusic(global::Sounds.Music);
    }

    private void Mute(){
        IsMute = true;
    }
    public void PlayMusic(Sounds sound){
        if(IsMute){
            return;
        }
         AudioClip clip = getSoundClip(sound);
        if(clip!= null){
            soundMusic.clip = clip;
            soundMusic.Play();
        }else{
            Debug.LogError("Not able to find the clip for sound type : " + sound);
        }
    }
    public void Play(Sounds sound){
          if(IsMute){
            return;
        }
        AudioClip clip = getSoundClip(sound);
        if(clip!= null){
            soundEffect.PlayOneShot(clip);
        }else{
            Debug.LogError("Not able to find the clip for sound type : " + sound);
        }
    }

    public void PlayPlayerDeathMusic(Sounds sound){
        if(IsMute){
            return;
        }
         AudioClip clip = getSoundClip(sound);
        if(clip!= null){
            soundMusic.clip = clip;
            soundMusic.PlayOneShot(clip);
        }else{
            Debug.LogError("Not able to find the clip for sound type : " + sound);
        }
    }

    private AudioClip getSoundClip(Sounds sound){
       SoundType item = Array.Find(Sounds, i=>i.soundType == sound);
       if (item!=null)
           return item.soundClip;
       
       return null;
    }
}
[Serializable]
public class SoundType{
    public Sounds soundType;

    public AudioClip soundClip;

}
